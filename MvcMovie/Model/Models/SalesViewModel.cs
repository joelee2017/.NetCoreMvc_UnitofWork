using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class SalesViewModel
    {
        /// <summary>編號</summary>
        public int Id { get; set; }

        /// <summary>電影編號</summary>
        public int MovieId { get; set; }

        /// <summary>售票數</summary>
        [Display(Name = "售票數")]
        public int Ticket { get; set; }

        /// <summary>總銷金額</summary>
        [Display(Name = "總銷金額")]
        public int TotalSales { get; set; }
    }
}
