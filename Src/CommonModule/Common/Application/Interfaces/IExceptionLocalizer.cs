// Ignore Spelling: Localizer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Interfaces
{
    public interface IExceptionLocalizer
    {
        string Localize(Exception ex);
    }
}
