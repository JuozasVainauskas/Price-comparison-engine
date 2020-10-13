using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Klases
{
    public class CommentsTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string Email { get; set; }
        public int ShopId { get; set; }
        public string Date { get; set; }
        public int ServiceRating { get; set; }
        public int ProductsQualityRating { get; set; }
        public int DeliveryRating { get; set; }
        public string Comment { get; set; }
    }
}
