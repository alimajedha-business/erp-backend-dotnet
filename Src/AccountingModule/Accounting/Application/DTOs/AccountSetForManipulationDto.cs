using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.DTOs
{
    public abstract record AccountSetForManipulationDto
    {
        [Required(ErrorMessage = "Title is a required field.")]
        public string? Title;
    }
}
