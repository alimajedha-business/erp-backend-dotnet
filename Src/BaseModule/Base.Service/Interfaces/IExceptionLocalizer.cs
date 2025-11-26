// Ignore Spelling: Localizer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.Base.Service.Interfaces
{

    public interface IExceptionLocalizer
    {
        string Localize(Exception ex);
    }

    public interface IExceptionLocalizer<TEntityResource>  :IExceptionLocalizer
    {  
    }
}
