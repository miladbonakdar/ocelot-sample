using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Faker<User> _testUsers;

        public UserController()
        {
            int ids = 0;
            _testUsers = new Faker<User>()
                .StrictMode(true)
                .RuleFor(o => o.Id, f => ids++)
                .RuleFor(o => o.Lastname, f => f.Name.LastName())
                .RuleFor(o => o.Name, f => f.Name.FirstName());
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new[]
            {
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
                _testUsers.Generate(),
            };
        }

        [HttpGet("{Id}")]
        public User Get(int id)
        {
            var item = _testUsers.Generate();
            item.Id = id;
            return item;
        }
    }

    public class User
    {
        public User()
        {
        }

        public User(int id, string name, string lastname)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set;}
    }
}