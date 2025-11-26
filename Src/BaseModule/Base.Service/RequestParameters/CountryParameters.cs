using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.Base.Service.RequestParameters
{
    public class CountryParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
        public CountryParameters() => OrderBy = "name";
    }
}
