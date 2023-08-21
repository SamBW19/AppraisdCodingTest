using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class AlternativeUser
    {

        public bool bPrivate;
        public string Email { get; set; }
        public int FirmID { get; set; }
        public int UserID { get; set; }
        public AdminLevel AdminLevel { get; set; }
    }
}
