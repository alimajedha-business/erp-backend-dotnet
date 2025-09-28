using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.DTOs
{
    public abstract record ProductHierarchyForManipulationDto
    {
        [Required(ErrorMessage = "CompanyId is a required field.")]
        public int CompanyId;

        [Required(ErrorMessage = "FirstLevelSize is a required field.")]
        public int FirstLevelSize;


        [Required(ErrorMessage = "FirstLevelType is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Name is 10 characters.")]
        public string? FirstLevelType;

        [Required(ErrorMessage = "SecondLevelSize is a required field.")]
        public int SecondLevelSize;


        [Required(ErrorMessage = "SecondLevelType is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Name is 10 characters.")]
        public string? SecondLevelType;


        [MaxLength(10, ErrorMessage = "Maximum length for the Description is 10 characters.")]
        public string? ThirdLevelType { get; init; }


        [MaxLength(10, ErrorMessage = "Maximum length for the Description is 10 characters.")]
        public string? FourthLevelType { get; init; }


        [MaxLength(10, ErrorMessage = "Maximum length for the Description is 10 characters.")]
        public string? FifthLevelType { get; init; }


        [MaxLength(10, ErrorMessage = "Maximum length for the Description is 10 characters.")]
        public string? SixthLevelType { get; init; }


        [MaxLength(10, ErrorMessage = "Maximum length for the Description is 10 characters.")]
        public string? SeventhLevelType { get; init; }

    }
}
