using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Restap.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
