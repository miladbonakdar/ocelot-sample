using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new[]
            {
                new User(1, "milad", "bonakdar"),
                new User(2, "omid", "naghipoor"),
                new User(3, "rasoool", "ghana")
            };
        }

        [HttpGet("{Id}")]
        public User Get(int id)
        {
            return new User(id, "milad", "bonakdar");
        }
    }

    public class User
    {
        public User(int id, string name, string lastname)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
        }

        public int Id { get; }
        public string Name { get; }
        public string Lastname { get; }
    }
}