using Scriptingo.FastApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class ApiRequest
    {
        /// <summary>
        /// Name of requested process name.
        /// </summary>
        public bool WithData { get; set; }

        public List<data> Parameters { get; set; }
    }
}
