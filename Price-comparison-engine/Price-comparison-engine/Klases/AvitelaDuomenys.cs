using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class AvitelaDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvitelosID { get; set; }
        public int AvitelosBalsuSuma { get; set; }
        public int AvitelosBalsavusiuSkaicius { get; set; }
    }
}
