using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Gateway.Models
{
    public class UserOrderAggregate
    {
        public Order Order { get; set; }
        public User User { get; set; }

        public StringContent ToJsonStringContent()
        {
            var content = JsonSerializer.Serialize(this);
            return new StringContent(content)
            {
                Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
            };
        }
    }
}