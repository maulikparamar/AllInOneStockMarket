using static AllinOneStock.Models.UniversalModel;

namespace AllinOneStock.Models
{
    public class OrderModel
    {
       public int id { get; set; }
       public int clientId { get; set; }
       public int brokerId { get; set; }
        public int scripId { get; set; }
       public int qty { get; set; }
       public decimal price { get; set; }
    }
}
