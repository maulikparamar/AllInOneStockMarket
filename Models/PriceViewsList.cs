using System;
using System.Collections.Generic;
using static AllinOneStock.Models.UniversalModel;

namespace AllinOneStock.Models
{
    public class PriceViewsList
    {
        public int client_id { get; set; }
        public string PriceViewName { get; set; }
        public List<ItemScrip> ScripsList { get; set; } = new List<ItemScrip>();
    }

    public class ItemScrip
    {
        public string ScripName { get; set; }

        public Int16 Token { get; set; }
        public Exchange Exchange { get; set; }
        public double Ltp { get; set; }
        public double Close { get; set; }
    }
}
