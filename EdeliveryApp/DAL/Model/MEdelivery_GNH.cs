using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MEdelivery_GNH
    {

        public int ID { get; set; }

        public string SONumber { get; set; }

        public string SOItems { get; set; }

        public string MADONVICUNGCAP { get; set; }

        public string TENDONVICUNGCAP { get; set; }

        public string MAKHACHANG { get; set; }

        public string TENKHACHHANG { get; set; }

        public string MAHANGHOA { get; set; }

        public string TENHANGHOA { get; set; }

        public decimal? SOBC { get; set; }

        public decimal? SOCAY { get; set; }

        public decimal? SOCAYLE { get; set; }

        public decimal? TRONGLUONG { get; set; }

        public string SOLUONGBEBO { get; set; }

        public decimal? DAKHAIBAO { get; set; }

        public decimal? CONLAI { get; set; }

        public string SONumberBPM { get; set; }
    }
}
