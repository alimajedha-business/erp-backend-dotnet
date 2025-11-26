using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.DTOs
{
    public record class CountryDto(int Id,string Name,int Code, int? TaxCode, string? Currency);
   
}
