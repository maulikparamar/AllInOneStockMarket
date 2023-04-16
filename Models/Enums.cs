namespace AllInOneStockMarket.Models
{
    public class Enums
    {
        public static class SqlDatabaseTable
        {
            public static string blogger = "[AllInOneMarket].[dbo].[blogger]";
            public static string index = "[AllInOneMarket].[dbo].[index]";
            public static string priceView = "[AllInOneMarket].[dbo].[PriceViewDetails]";
            public static string user_details = "[AllInOneMarket].[dbo].[ClientDetails]";
            public static string scrip_details = "[AllInOneMarket].[dbo].[ScripDetails]";
            public static string orders = "[AllInOneMarket].[dbo].[OrderDetails]";
        }

        public static class PriceViewDetailsColumnName
        {
            public static string priceViewName = "[priceViewName]";
            public static string client_id = "[client_id]";
            public static string scripList = "[scrip_list]";
        }
        public static class ClientDetailsColumnName
        {
            public static string client_id = "[clientId]";
            public static string client_name = "[clientName]";
            public static string client_type = "[clientType]";
            public static string client_Fund = "[clientFund]";
            public static string client_phoneNo = "[clientPhoneNo]";
            public static string client_pan = "[clientPan]";
            public static string client_address = "[clientAddress]";
            public static string client_password = "[clientPassword]";
            public static string client_verification_code = "[clientVerificationCode]";
        }
        public static class OrderDetailsColumnName
        {
            public static string id = "[id]";
            public static string client_id = "[client_id]";
            public static string broker_id = "[broker_id]";
            public static string scrip_id = "[scrip_id]";
            public static string qty = "[qty]";
            public static string price = "[price]";
            public static string status = "[status]";
            public static string buyorsell = "[buyorsell]";
        }
        public static class ScripDetailsColumnName
        {
            public static string scrip_name = "[ScripName]";
            public static string token = "[Token]";
            public static string exchange = "[Exchange]";
            public static string scrip_ltp = "[scrip_ltp]";
            public static string scrip_close = "[scrip_close]";
            public static string total_bids = "[total_bids]";
            public static string total_asks = "[total_asks]";
            public static string bid1_qty = "[bid1_qty]";
            public static string bid1_orderno = "[bid1_orderno]";
            public static string bid1_price = "[bid1_price]";

            public static string bid2_qty = "[bid2_qty]";
            public static string bid2_orderno = "[bid2_orderno]";
            public static string bid2_price = "[bid2_price]";

            public static string bid3_qty = "[bid3_qty]";
            public static string bid3_orderno = "[bid3_orderno]";
            public static string bid3_price = "[bid3_price]";

            public static string bid4_qty = "[bid4_qty]";
            public static string bid4_orderno = "[bid4_orderno]";
            public static string bid4_price = "[bid4_price]";

            public static string bid5_qty = "[bid5_qty]";
            public static string bid5_orderno = "[bid5_orderno]";
            public static string bid5_price = "[bid5_price]";

            public static string ask1_qty = "[ask1_qty]";
            public static string ask1_orderno = "[ask1_orderno]";
            public static string ask1_price = "[ask1_price]";

            public static string ask2_qty = "[ask2_qty]";
            public static string ask2_orderno = "[ask2_orderno]";
            public static string ask2_price = "[ask2_price]";

            public static string ask3_qty = "[ask3_qty]";
            public static string ask3_orderno = "[ask3_orderno]";
            public static string ask3_price = "[ask3_price]";

            public static string ask4_qty = "[ask4_qty]";
            public static string ask4_orderno = "[ask4_orderno]";
            public static string ask4_price = "[ask4_price]";

            public static string ask5_qty = "[ask5_qty]";
            public static string ask5_orderno = "[ask5_orderno]";
            public static string ask5_price = "[ask5_price]";
        }
    }
}
