using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AllinOneStock.Models.UniversalModel;

namespace AllInOneStockMarket.Models
{
    public class ScripModel
    {
        public string ScripName { get; set; }
        public Int16 Token { get; set; }
        public Exchange Exchange { get; set; }
        public double Ltp { get; set; }
        public double Close { get; set; }
        public long TotalBids { get; set; }
        public long TotalAsks { get; set; }
        public DepthModel Bid1 { get; set; } = new DepthModel(); 
        public DepthModel Bid2 { get; set; } = new DepthModel(); 
        public DepthModel Bid3 { get; set; } = new DepthModel(); 
        public DepthModel Bid4 { get; set; } = new DepthModel(); 
        public DepthModel Bid5 { get; set; } = new DepthModel(); 
        public DepthModel Ask1 { get; set; } = new DepthModel(); 
        public DepthModel Ask2 { get; set; } = new DepthModel(); 
        public DepthModel Ask3 { get; set; } = new DepthModel(); 
        public DepthModel Ask4 { get; set; } = new DepthModel(); 
        public DepthModel Ask5 { get; set; } = new DepthModel();
    }

    public class DepthModel
    {
        public int qty { get; set; }
        public int order { get; set; }
        public decimal price { get; set; }
    }
}
