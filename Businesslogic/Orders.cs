using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static AllInOneStockMarket.Models.Enums;

namespace AllInOneStockMarket.Businesslogic
{
    public class Orders
    {
        public List<OrderModel> getAllOrders(string clientId)
        {
            SqlController sql = new SqlController();
           SqlDataReader dataReader = sql.selectQuery(SqlDatabaseTable.orders, "clientId", clientId);

            //if (dataReader != null && dataReader.HasRows)
            //{
            //    while (dataReader.Read())
            //    {
            //        PriceViewsList priceViews = new();
            //        priceViews.PriceViewName = dataReader["priceview_name"].ToString();
            //        priceViews.ScripsList = getListItemScrips(dataReader["priceview_list"].ToString().Split(",").ToList());
            //        list.Add(priceViews);
            //    }
            //}

            //List<OrderModel> models =
            return null;
        }

        public void deleteOrder(string clientId,int orderId)
        {
            try
            {
                SqlController sql = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("clientId", clientId);
                valuePairs.Add("orderId", orderId.ToString());
                sql.deleteQuery(SqlDatabaseTable.orders, valuePairs);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int createOrder(OrderModel model)
        {
            try
            {
                SqlController sql = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("clientId", model.clientId.ToString());
                valuePairs.Add("brokerId",model.brokerId.ToString());
                valuePairs.Add("exchange",model.scripId.ToString());
                valuePairs.Add("price",model.price.ToString());
                valuePairs.Add("qty", model.qty.ToString());
                int result = sql.insertQuery(SqlDatabaseTable.orders, valuePairs);
                return result;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return -1;
        }
    }
}
