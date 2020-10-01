using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Classes
{
    public class ShopRatingData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public double VotesCount { get; set; }
        public double VotersNumber { get; set; }
    }
}
