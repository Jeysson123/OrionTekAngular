using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class HttpStatusDto
    {
        public string CurrentMsg { get; set; }
        public bool CurrentStatus { get; set; }
        public string CurrentCode { get; set; }
    }
}
