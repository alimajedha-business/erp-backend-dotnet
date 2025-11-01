using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.DTOs
{
    public abstract record DepartmentForManipulationDto
    {
        [MaxLength(10,ErrorMessage = "MaxLength")]
        [Display(Name ="Code")]
        public string? Code { get; set; }

        [Required(ErrorMessage ="Required")]
        [MaxLength(200, ErrorMessage = "MaxLength")]
        [Display(Name = "Name")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public DateTime? StatusChangeDate { get; set; }
    }
}
