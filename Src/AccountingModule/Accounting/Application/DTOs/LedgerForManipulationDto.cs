using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.DTOs
{
    public abstract record LedgerForManipulationDto
    {
        [Required(ErrorMessage = "Code is a required field.")]
        public short Code { get; init; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Name2 is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name2 is 100 characters.")]
        public string? Name2 { get; init; }


        [Required(ErrorMessage = "IsLeading is a required field.")]
        public bool IsLeading { get; init; }


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 1000 characters.")]
        public string? Description { get; init; }
        public IEnumerable<AccountSetForCreationDto>? AccountSets { get; init; }
    }
}
