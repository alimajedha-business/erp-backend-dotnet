using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Interfaces.Services
{
    internal interface IServiceManager
    {
        ICountryService CountryService{ get; }
        //IAccountSetService AccountSetService { get; }
    }
}
