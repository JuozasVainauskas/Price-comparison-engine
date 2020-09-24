using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class DuomenuStruktura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NaudotojoID { get; set; }
        public string NaudotojoEmail { get; set; }
        public string NaudotojoSlaptazodis { get; set; }
    }
}
