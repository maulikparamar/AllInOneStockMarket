using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using static AllinOneStock.Models.UniversalModel;
using static AllInOneStockMarket.Models.Enums;

namespace AllInOneStockMarket.Businesslogic
{
    public class PriceView
    {
        public List<PriceViewsList> getPriceViewAll(string clientId)
        {
            List<PriceViewsList> list = new();
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("[client_id]", clientId);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.priceView, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PriceViewsList priceViews = new();
                        priceViews.PriceViewName = dataReader["priceview_name"].ToString();
                        priceViews.ScripsList = getListItemScrips(dataReader["priceview_list"].ToString().Split(",").ToList());
                        list.Add(priceViews);
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        public void deletePriceView(string userName,string priceView)
        {
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("[client_id]", userName);
                valuePairs.Add("[priceview_name]", priceView);
                controller.deleteQuery(SqlDatabaseTable.priceView, valuePairs);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int updatePriceView(string userName, PriceViewsList priceview)
        {
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("[client_id]", userName);
                valuePairs.Add("[priceview_name]", priceview.PriceViewName);
                valuePairs.Add("[ScripName]", String.Join("," ,priceview.ScripsList));
                controller.insertOrUpdateQuery(SqlDatabaseTable.priceView, valuePairs, "[priceview_name]",priceview.PriceViewName);
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        public List<ItemScrip> GetScripList(string scripName)
        {
            List<ItemScrip> list = new();
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add("[ScripName]", scripName);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.scrip_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        list.Add(new ItemScrip()
                        {
                            ScripName = dataReader["ScripName"].ToString(),
                            Exchange = (Exchange)Convert.ToInt16(dataReader["Exchange"].ToString()),
                            Close = Convert.ToDouble(dataReader["scrip_close"].ToString()),
                            Ltp = Convert.ToDouble(dataReader["scrip_ltp"].ToString()),
                            Token = Convert.ToInt16(dataReader["ScripCode"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        // Pending
        public ItemScrip getScriDetails(string tokenId)
        {
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                //valuePairs.Add("[client_id]", clientId);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.priceView, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
         ////////sdfjsdhfkjsdhfjk
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        private ItemScrip getItemScrip(string tokeId)
        {
            return null;
        }

        private List<ItemScrip> getListItemScrips(List<string> listTokens)
        {
            List<ItemScrip> items = new();

            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                foreach(var i in listTokens)
                {
                    string exchange = new String(i.Where(Char.IsLetter).ToArray());
                    string token = new String(i.Where(Char.IsDigit).ToArray());
                    valuePairs.Add("[Exchange]",exchange);
                    valuePairs.Add("[ScripCode]", token);
                }
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.scrip_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        items.Add(new ItemScrip() { 
                            ScripName = dataReader["ScripName"].ToString(),
                            Exchange = (Exchange)Convert.ToInt16(dataReader["Exchange"].ToString()),
                            Close = Convert.ToDouble(dataReader["scrip_close"].ToString()),
                            Ltp = Convert.ToDouble(dataReader["scrip_ltp"].ToString()),
                            Token = Convert.ToInt16(dataReader["ScripCode"].ToString()) 
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return items;
        }
    }
}
