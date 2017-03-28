using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerLibrary
{
    public class Case
    {
        public int CaseId { get; set; }
        public string Category { get; set; }
        public DateTime OpenDate { get; set; }
        public string CaseStatusId { get; set; }
        public string Summary { get; set; }
        public DateTime CloseDate { get; set; }
        public int ClientId { get; set; }
        
    }
}
