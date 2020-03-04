using System.Net.Mime;
using System.Text.Json;

namespace Gateway.Models
{
    public class Order
    {
        public decimal FinalPrice { get; set; }
        public long Id { get; set; }

        public static Order Deserialize(string content)
            => JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
    }
}