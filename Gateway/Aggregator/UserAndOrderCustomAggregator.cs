using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Gateway.Models;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace Gateway.Aggregator
{
    public class UserAndOrderCustomAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            var userTask = responses.First(r => r.DownstreamReRoute.Key == "User").DownstreamResponse.Content
                .ReadAsStringAsync();
            var orderTask = responses.First(r => r.DownstreamReRoute.Key == "Order").DownstreamResponse.Content
                .ReadAsStringAsync();

            var res = await Task.WhenAll(userTask, orderTask);
            var userOrderAggregate = new UserOrderAggregate
            {
                Order = Order.Deserialize(res[1]),
                User = User.Deserialize(res[0])
            };

            return new DownstreamResponse(userOrderAggregate.ToJsonStringContent(), HttpStatusCode.OK,
                new List<KeyValuePair<string, IEnumerable<string>>>()
                , "OK");
        }
    }
}