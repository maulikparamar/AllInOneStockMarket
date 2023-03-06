using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using System;
using System.Collections.Generic;
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
            sql.selectQuery(SqlDatabaseTable.orders, "clientId", clientId);
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

        public int marketOrder()
        {
            try
            {
                SqlController sql = new SqlController();
                Dictionary<string, string> valuePairs = new();
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
