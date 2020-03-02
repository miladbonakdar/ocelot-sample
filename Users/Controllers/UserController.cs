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
                new User("milad", "bonakdar"),
                new User("omid", "naghipoor"),
                new User("rasoool", "ghana")
            };
        }
    }

    public class User
    {
        public User(string name, string lastname)
        {
            Name = name;
            Lastname = lastname;
        }

        public string Name { get; }
        public string Lastname { get; }
    }
}