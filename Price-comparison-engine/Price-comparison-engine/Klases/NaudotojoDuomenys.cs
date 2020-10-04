using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class NaudotojoDuomenys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NaudotojoID { get; set; }
        public string Email { get; set; }
        public string SlaptazodzioHash { get; set; }
        public string SlaptazodzioSalt { get; set; }
        public string ArBalsavo { get; set; }
        public int Role { get; set; }
    }
}
