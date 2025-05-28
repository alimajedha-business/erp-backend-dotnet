using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.DTOs
{
    public record LedgerForCreationDto(short Code, string Name, string Name2, bool IsLeading, string? Description,
        IEnumerable<AccountSetForCreationDto> AccountSets);

}
