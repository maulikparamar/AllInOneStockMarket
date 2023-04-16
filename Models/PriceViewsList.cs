using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static AllinOneStock.Models.UniversalModel;

namespace AllinOneStock.Models
{
    public class PriceViewsList
    {
        public string client_id { get; set; }
        public string PriceViewName { get; set; }
        public List<ItemScrip> ScripsList { get; set; } = new List<ItemScrip>();
    }

    public class UpdatePriceViewsList
    {
        public String client_id { get; set; }
        public string PriceViewName { get; set; }
        public List<Int32> ScripsList { get; set; } = new List<int>();
    }

    public class ItemScrip
    {
        public ItemScrip() { }
        public ItemScrip(SqlDataReader dataReader)
        {
            ScripName = dataReader["ScripName"].ToString();
            Exchange = (Exchange)Convert.ToInt16(dataReader["Exchange"].ToString());
            Close = Convert.ToDouble(dataReader["scrip_close"].ToString());
            Ltp = Convert.ToDouble(dataReader["scrip_ltp"].ToString()); 
            Token = Convert.ToInt16(dataReader["Token"].ToString());
        }
        public string ScripName { get; set; }

        public Int16 Token { get; set; }
        public Exchange Exchange { get; set; }
        public double Ltp { get; set; }
        public double Close { get; set; }
    }
}
