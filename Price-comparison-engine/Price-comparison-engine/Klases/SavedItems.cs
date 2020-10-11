using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class SavedItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SavedItemID { get; set; }
        public string Email { get; set; }
        public string PageURL { get; set; }
        public string ImgURL { get; set; }
        public string ShopName { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
    }
}
