using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using AllInOneStockMarket.Models;
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
                valuePairs.Add(PriceViewDetailsColumnName.client_id, clientId);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.priceView, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        PriceViewsList priceViews = new();
                        priceViews.PriceViewName = dataReader["priceViewName"].ToString();
                        priceViews.ScripsList = getListItemScrips(dataReader["scrip_list"].ToString().Split(",").ToList());
                        list.Add(priceViews);
                    }
                    return list;
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
                valuePairs.Add(PriceViewDetailsColumnName.client_id, userName);
                valuePairs.Add(PriceViewDetailsColumnName.priceViewName, priceView);
                controller.deleteQuery(SqlDatabaseTable.priceView, valuePairs);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int updatePriceView(string userName, UpdatePriceViewsList priceview)
        {
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(PriceViewDetailsColumnName.client_id, userName);
                valuePairs.Add(PriceViewDetailsColumnName.priceViewName, priceview.PriceViewName);
                valuePairs.Add(PriceViewDetailsColumnName.scripList, String.Join("," ,priceview.ScripsList));
                controller.insertOrUpdateQuery(SqlDatabaseTable.priceView, valuePairs, PriceViewDetailsColumnName.priceViewName,priceview.PriceViewName);
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        public List<ItemScrip> GetScripList(string token)
        {
            List<ItemScrip> list = new();
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(ScripDetailsColumnName.token, token);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.scrip_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        list.Add(new ItemScrip(dataReader));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }

        public ScripModel getScriDetails(string token)
        {
            try
            {
                SqlController controller = new SqlController();
                //string exchange = new String(tokenId.Where(Char.IsLetter).ToArray());
                //string token = new String(tokenId.Where(Char.IsDigit).ToArray());
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(ScripDetailsColumnName.token, token);
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.priceView, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        return new ScripModel(dataReader);
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
                //foreach(var token in listTokens)
                //{
                //    valuePairs.Add(ScripDetailsColumnName.token, token);
                //}
                valuePairs.Add(ScripDetailsColumnName.token, String.Join(",", listTokens));
                SqlDataReader dataReader = controller.selectCondition_multiplevalue_In_Query(null, SqlDatabaseTable.scrip_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        items.Add(new ItemScrip(dataReader));
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
