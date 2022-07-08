using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MDriverRegister
    {
        public string DriverId { get; set; }

        public string DriverName { get; set; }

        public string DriverCardNo { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Place { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCode { get; set; }
    }
}
