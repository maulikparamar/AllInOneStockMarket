using AllInOneStockMarket.Models;
using static AllinOneStock.Models.UniversalModel;

namespace AllinOneStock.Models
{
    public class OrderModel
    {
      // public int id { get; set; }
       public string clientId { get; set; }
       public string brokerId { get; set; }
        public int scripId { get; set; }
       public int qty { get; set; }
       public decimal price { get; set; }

        public string buyorsell { get; set; }
    }

    public class getOrderModel
    {
         public int id { get; set; }
        public string clientId { get; set; }
        public ClientDetails brokerModel { get; set; }
        public ScripModel scripModel { get; set; }
        public int qty { get; set; }
        public double price { get; set; }

        public string status { get; set; }

        public string buyorsell { get; set; }
    }

    public enum OrderStatus{
        Pending = 0,
        Accept = 1
    }
}
