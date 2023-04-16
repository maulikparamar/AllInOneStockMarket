using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static AllinOneStock.Models.UniversalModel;

namespace AllInOneStockMarket.Models
{
    public class ScripModel
    {
        public ScripModel() { }
        public ScripModel(SqlDataReader dataReader)
        {
            ScripName = dataReader["ScripName"].ToString();
            Token = Convert.ToInt32(dataReader["Token"].ToString());
            Exchange = (Exchange)Convert.ToInt16(dataReader["Exchange"].ToString());
            Ltp = Convert.ToDouble(dataReader["scrip_ltp"].ToString());
            Open = Convert.ToDouble(dataReader["scrip_open"].ToString());
            High = Convert.ToDouble(dataReader["scrip_high"].ToString());
            Low = Convert.ToDouble(dataReader["scrip_low"].ToString());
            Close = Convert.ToDouble(dataReader["scrip_close"].ToString());
            TotalBids = Convert.ToInt64(dataReader["total_bids"].ToString());
            TotalAsks = Convert.ToInt64(dataReader["total_asks"].ToString());
            Bid1 = new DepthModel() { qty = Convert.ToInt32(dataReader["bid1_qty"].ToString()), order = Convert.ToInt32(dataReader["bid1_orderno"].ToString()), price = Convert.ToDouble(dataReader["bid1_price"].ToString()) };
            Bid2 = new DepthModel() { qty = Convert.ToInt32(dataReader["bid2_qty"].ToString()), order = Convert.ToInt32(dataReader["bid2_orderno"].ToString()), price = Convert.ToDouble(dataReader["bid2_price"].ToString()) };
            Bid3 = new DepthModel() { qty = Convert.ToInt32(dataReader["bid3_qty"].ToString()), order = Convert.ToInt32(dataReader["bid3_orderno"].ToString()), price = Convert.ToDouble(dataReader["bid3_price"].ToString()) };
            Bid4 = new DepthModel() { qty = Convert.ToInt32(dataReader["bid4_qty"].ToString()), order = Convert.ToInt32(dataReader["bid4_orderno"].ToString()), price = Convert.ToDouble(dataReader["bid4_price"].ToString()) };
            Bid5 = new DepthModel() { qty = Convert.ToInt32(dataReader["bid5_qty"].ToString()), order = Convert.ToInt32(dataReader["bid5_orderno"].ToString()), price = Convert.ToDouble(dataReader["bid5_price"].ToString()) };

            Ask1 = new DepthModel() { qty = Convert.ToInt32(dataReader["ask1_qty"].ToString()), order = Convert.ToInt32(dataReader["ask1_orderno"].ToString()), price = Convert.ToDouble(dataReader["ask1_price"].ToString()) };
            Ask2 = new DepthModel() { qty = Convert.ToInt32(dataReader["ask2_qty"].ToString()), order = Convert.ToInt32(dataReader["ask2_orderno"].ToString()), price = Convert.ToDouble(dataReader["ask2_price"].ToString()) };
            Ask3 = new DepthModel() { qty = Convert.ToInt32(dataReader["ask3_qty"].ToString()), order = Convert.ToInt32(dataReader["ask3_orderno"].ToString()), price = Convert.ToDouble(dataReader["ask3_price"].ToString()) };
            Ask4 = new DepthModel() { qty = Convert.ToInt32(dataReader["ask4_qty"].ToString()), order = Convert.ToInt32(dataReader["ask4_orderno"].ToString()), price = Convert.ToDouble(dataReader["ask4_price"].ToString()) };
            Ask5 = new DepthModel() { qty = Convert.ToInt32(dataReader["ask5_qty"].ToString()), order = Convert.ToInt32(dataReader["ask5_orderno"].ToString()), price = Convert.ToDouble(dataReader["ask5_price"].ToString()) };
                      
        }
        public string ScripName { get; set; }
        public Int32 Token { get; set; }
        public Exchange Exchange { get; set; }
        public double Ltp { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
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
        public double price { get; set; }
    }
}
