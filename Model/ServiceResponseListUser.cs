using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Volunteer_API.Model
{
    public class ServiceResponseListUser<T>
    {
        public List<User>? users { get; set; }

    }
}