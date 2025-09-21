using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public abstract record ProductCodeForManipulationDto
    {
        [Required(ErrorMessage = "CompanyId is a required field.")]
        public int CompanyId;

        [Required(ErrorMessage = "FirstLevelCode is a required field.")]
        public int FirstLevelCode;


        [Required(ErrorMessage = "FirstLevelName is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
        public string? FirstLevelName;

        [Required(ErrorMessage = "SecondLevelCode is a required field.")]
        public int SecondLevelCode;


        [Required(ErrorMessage = "SecondLevelName is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
        public string? SecondLevelName;


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
        public string? ThirdLevelName { get; init; }


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
        public string? FourthLevelName { get; init; }


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
        public string? FifthLevelName { get; init; }


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
        public string? SixthLevelName { get; init; }


        [MaxLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
        public string? SeventhLevelName { get; init; }

    }
}
