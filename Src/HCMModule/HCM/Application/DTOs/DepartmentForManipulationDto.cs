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
        [MaxLength(10,ErrorMessage = "MaxLengthCode")]
        public string? Code { get; set; }

        [Required(ErrorMessage ="NameIsRequired")]
        [MaxLength(200, ErrorMessage = "MaxLengthName")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        public DateTime? StatusChangeDate { get; set; }
    }
}
