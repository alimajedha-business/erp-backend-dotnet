using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NGErp.Base.Domain.ErrorModel
{
    public  record ErrorDetails
    {
        public string? Title { get; set; }
        public string? TraceId { get; set; }
        public IDictionary<string, string[]?>? Details { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
