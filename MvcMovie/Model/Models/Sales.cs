namespace Model.Models
{
    public class Sales
    {
        /// <summary>編號</summary>
        public int Id { get; set; }

        /// <summary>電影編號</summary>
        public int MovieId { get; set; }

        /// <summary>售票數</summary>
        public int Ticket { get; set; }

        /// <summary>總銷金額</summary>
        public int TotalSales { get; set; }
    }
}
