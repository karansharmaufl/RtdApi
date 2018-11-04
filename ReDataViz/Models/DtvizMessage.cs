using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReDataViz.Models
{
    public class DtvizMessage
    {
        public  string Id { get; set; }
        public string Owner { get; set; }
        public string OwnerEmailID { get; set; }
        public string Text { get; set; }
        public string Topic { get; set; }
        public string Color { get; set; }
    }
}
