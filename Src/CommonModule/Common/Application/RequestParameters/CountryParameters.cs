using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.RequestParameters
{
    public class CountryParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
    }
}
