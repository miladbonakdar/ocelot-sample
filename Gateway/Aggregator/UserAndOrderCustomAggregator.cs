using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
            var user = res[0];
            var order = res[1];

            var contentBuilder = new StringBuilder();
            contentBuilder.Append("{\"User\":");
            contentBuilder.Append(user);
            contentBuilder.Append(",\"Order\":");
            contentBuilder.Append(order);
            contentBuilder.Append("}");

            var stringContent = new StringContent(contentBuilder.ToString())
            {
                Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK,
                new List<KeyValuePair<string, IEnumerable<string>>>()
                , "OK");
        }
    }
}