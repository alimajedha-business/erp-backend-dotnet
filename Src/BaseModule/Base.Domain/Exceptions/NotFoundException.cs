using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public string LocalizationKey { get; }
        public object[] Arguments { get; }

        protected NotFoundException(string localizationKey, params object[] args)
            : base(localizationKey) // base message = key, for backward compatibility
        {
            LocalizationKey = localizationKey;
            Arguments = args;
        }
    }
}
