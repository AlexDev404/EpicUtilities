using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnbr.Json
{
    internal class error
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public int numericErrorCode { get; set; }
        public string originatingService { get; set; }
        public string intent { get; set; }
    }
}
