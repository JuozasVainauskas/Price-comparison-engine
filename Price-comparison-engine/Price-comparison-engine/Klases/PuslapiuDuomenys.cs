using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class PuslapiuDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageID { get; set; }
        public string PageURL { get; set; }
        public string ImgURL { get; set; }
    }
}
