using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class PrekiuDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrekiuID { get; set; }
        public string PuslapioURL { get; set; }
        public string ImgURL { get; set; }
        public string ParduotuvesVardas { get; set; }
        public string PrekesVardas { get; set; }
        public string PrekesKaina { get; set; }
        public string RaktinisZodis { get; set; }
    }
}
