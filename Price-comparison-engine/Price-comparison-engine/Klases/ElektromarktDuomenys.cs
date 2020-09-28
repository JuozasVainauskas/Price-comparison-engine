using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class ElektromarktDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ElektromarktID { get; set; }
        public int ElektromarktBalsuSuma { get; set; }
        public int ElektromarktBalsavusiuSkaicius { get; set; }
    }
}
