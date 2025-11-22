// Ignore Spelling: Localizer

using Common.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Service.Interfaces
{

    public interface IExceptionLocalizer
    {
        string Localize(Exception ex);
    }

    public interface IExceptionLocalizer<TEntityResource>  :IExceptionLocalizer
    {  
    }
}
