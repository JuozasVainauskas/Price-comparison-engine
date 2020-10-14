using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class ItemsTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string PageUrl { get; set; }
        public string ImgUrl { get; set; }
        public string ShopName { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
        public string Keyword { get; set; }
    }
}
