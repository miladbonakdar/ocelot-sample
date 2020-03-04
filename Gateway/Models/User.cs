using System.Text.Json;

namespace Gateway.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static User Deserialize(string content)
            => JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
    }
}