using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.ApplicationUser
{
    public class CurrentUser
    {
        public CurrentUser(string id, string email, IEnumerable<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles;
        }

        public string Id { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
