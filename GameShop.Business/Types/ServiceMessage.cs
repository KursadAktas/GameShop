using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }
}
