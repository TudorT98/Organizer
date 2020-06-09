using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Organizer.Model
{
    public class Calendar
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [NotMapped]
        public User user { get; set; }
        public DateTime day { get; set; }
    }
}
