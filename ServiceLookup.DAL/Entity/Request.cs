using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
{
    public class Request 
    {
        [Required]
        public int Id { get; set; }
        [StringLength(2000, MinimumLength = 2)]
        public string? Description { get; set; }

        [Required]
        public DateTime DateTimeOfCreating { get; set; }
        [Required]
        public DateTime StartOfBooking { get; set; }

        public DateTime EndOfBooking { get; set; }

        public int PriceId { get; set; }
        public Price Price { get; set; }

        public int ConditionId { get; set; }
        public Condition? Condition { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

    }
}
