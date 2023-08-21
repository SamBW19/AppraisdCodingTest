using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class Appraisal
    {
        public int AppraisalID { get; set; }
        public DateTime? dDue { get; set; }
        public DateTime? dCreated { get; set; }
        public int UserID { get; set; }
        public int? AppraiserID { get; set; }
        public int? ProcessstepID { get; set; }
        public int? UseGroupID { get; set; }
        public string Title { get; set; }
        public int? SignOffID { get; set; }
        public bool? bSignedOff { get; set; }
        public DateTime? dCycleDate { get; set; }
        public bool bDeleted { get; set; }  
        public bool bHiddenFromAppraisee { get; set; }
        public bool bAutoClosed { get; set; }
        public DateTime? dAutoClosed { get; set; }
    }
}
