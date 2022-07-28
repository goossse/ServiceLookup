using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace ServiceLookup.BL.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }
        public string? Image { get; set; }
        public double? AverageRate { get; set; }
        [Required]
        public DateTime DateOfCreating { get; set; }
        public int PriceId { get; set; }
        public PriceDTO? Price { get; set; }
        [Required]
        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public int? ServiceTypeId { get; set; }
        [Required]
        public ServiceTypeDTO? ServiceType { get; set; }
        public List<RequestDTO>? Requests { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}
