using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class ParduotuviuDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParduotuvesID { get; set; }
        public string ParduotuvesPavadinimas { get; set; }
        public int BalsuSuma { get; set; }
        public int BalsavusiuSkaicius { get; set; }
    }
}
