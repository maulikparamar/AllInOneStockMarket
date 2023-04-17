using AllinOneStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllInOneStockMarket.Models
{
    public class ClientDetails
    {
        public string client_id { get; set; }
        public string client_name { get; set; }
        public Int64 client_phoneno { get; set; }
    }

    public class FullClientDetails
    {
        public string client_id { get; set; }
        public string client_name { get; set; }
        public Int64 client_funds { get; set; }
        public Int64 client_phoneno { get; set; }

        public string client_pan { get; set; }

        public string client_address { get; set; }
        public string client_password { get; set; }
        public int client_verification_code { get; set; }
    }

}
