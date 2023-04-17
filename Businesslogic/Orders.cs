using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using AllInOneStockMarket.Models;
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
        public List<getOrderModel> getAllOrders(string clientId)
        {
            try { 
            SqlController sql = new SqlController();
           SqlDataReader dataReader = sql.selectQuery(SqlDatabaseTable.orders, OrderDetailsColumnName.client_id, clientId);


            List<getOrderModel> orders = new();
            if (dataReader != null && dataReader.HasRows)
            {
                    PriceView priceView = new PriceView();
                while (dataReader.Read())
                {
                        getOrderModel orderModel = new();
                        orderModel.id = Convert.ToInt32(dataReader["id"].ToString());
                        orderModel.clientId = dataReader["client_id"].ToString();
                        orderModel.brokerModel = GetClienDetails(dataReader["broker_id"].ToString());
                        orderModel.scripModel = priceView.getScriDetails(dataReader["scrip_id"].ToString());
                        orderModel.qty = Convert.ToInt32(dataReader["qty"].ToString());
                        orderModel.price = Convert.ToDouble(dataReader["price"].ToString());
                        orderModel.status = dataReader["status"].ToString();
                        orderModel.buyorsell = dataReader["buyorsell"].ToString();
                        orders.Add(orderModel);
                }
                    return orders;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        public List<getOrderModel> getBrokerAllOrders(string brokerId)
        {
            try
            {
                SqlController sql = new SqlController();
                SqlDataReader dataReader = sql.selectQuery(SqlDatabaseTable.orders, OrderDetailsColumnName.broker_id, brokerId);


                List<getOrderModel> orders = new();
                if (dataReader != null && dataReader.HasRows)
                {
                    PriceView priceView = new PriceView();
                    while (dataReader.Read())
                    {
                        getOrderModel orderModel = new();
                        orderModel.id = Convert.ToInt32(dataReader["id"].ToString());
                        orderModel.clientId = dataReader["client_id"].ToString();
                        orderModel.brokerModel = GetClienDetails(dataReader["broker_id"].ToString());
                        orderModel.scripModel = priceView.getScriDetails(dataReader["scrip_id"].ToString());
                        orderModel.qty = Convert.ToInt32(dataReader["qty"].ToString());
                        orderModel.price = Convert.ToDouble(dataReader["price"].ToString());
                        orderModel.status = dataReader["status"].ToString();
                        orderModel.buyorsell = dataReader["buyorsell"].ToString();
                        orders.Add(orderModel);
                    }
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        private ClientDetails GetClienDetails(string clientId)
        {
            ClientDetails clientDetails = new();
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(ClientDetailsColumnName.client_id, clientId);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        clientDetails.client_id = dataReader["clientId"].ToString();
                        clientDetails.client_name = dataReader["clientName"].ToString();
                        clientDetails.client_phoneno = Convert.ToInt64(dataReader["clientPhoneNo"].ToString());   
                    }
                    return clientDetails;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return clientDetails;
        }

        public void deleteOrder(string clientId,int orderId)
        {
            try
            {
                SqlController sql = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(OrderDetailsColumnName.client_id, clientId);
                valuePairs.Add(OrderDetailsColumnName.id, orderId.ToString());
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
                valuePairs.Add(OrderDetailsColumnName.client_id, model.clientId.ToString());
                valuePairs.Add(OrderDetailsColumnName.broker_id,model.brokerId.ToString());
                valuePairs.Add(OrderDetailsColumnName.scrip_id,model.scripId.ToString());
                valuePairs.Add(OrderDetailsColumnName.price,model.price.ToString());
                valuePairs.Add(OrderDetailsColumnName.qty, model.qty.ToString());
                valuePairs.Add(OrderDetailsColumnName.status, OrderStatus.Pending.ToString());
                valuePairs.Add(OrderDetailsColumnName.buyorsell, model.buyorsell);
                int result = sql.insertQuery(SqlDatabaseTable.orders, valuePairs);
                return result;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        public int updateStatus(OrderStatus status,int id)
        {
            try
            {
                SqlController sql = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(OrderDetailsColumnName.status, status.ToString());
                int result = sql.updateQuery(SqlDatabaseTable.orders, valuePairs, OrderDetailsColumnName.id, id.ToString());
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return -1;
        }
    }
}
