using AllInOneStockMarket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AllinOneStock.Businesslogic
{
    public class SqlController
    {
        private SqlConnection SqlConnection;
        public SqlController()
        {
            SqlConnection = new SqlConnection();
            SqlConnection.ConnectionString = Startup.sqlConnectionString;
        }
        public SqlDataReader selectQuery(string tableName,string key,string value)
        {
            SqlConnection.Open();
            SqlDataReader dataReader;
            string query = "select * from " + tableName + " where " + key + "=" + value + "";
            try
            {
                SqlCommand sc = new SqlCommand(query, SqlConnection);
                dataReader = sc.ExecuteReader(CommandBehavior.CloseConnection);
              //  sc.Dispose();
                return dataReader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    SqlConnection.Close();
            //}
            return null;
        }
        public SqlDataReader selectConditionQuery(List<string>? fileds,string tableName,Dictionary<string, string> valuePairs)
        {
            if(SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open();
            }
            SqlDataReader dataReader;
            string filed = "";
            if (fileds != null)
            {
                string.Join(",", fileds);
            }
            else
            {
                filed = "*";
            }
            string condition = "";
            var i = 0;
            foreach (KeyValuePair<string, string> d in valuePairs)
            {
                if(i == 0)
                {
                    condition += d.Key + "= '" + d.Value + "'";
                }
                else
                {
                    condition += " and " + d.Key + "= '"+ d.Value+"'";
                }
                i++;
            }
            string query = "select " + filed + " from " + tableName + " where " + condition + "";
            try
            {
                SqlCommand command = new SqlCommand(query, SqlConnection);
                dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                // command.Dispose();
                return dataReader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
              
            //  //  SqlConnection.Close();
            //}
            return null;
        }
        public Int32 insertQuery(string tableName, Dictionary<string, string> valuePairs)
        {
            SqlConnection.Open();
            var columns = string.Join(",", valuePairs.Keys);
            var values = string.Join(",", valuePairs.Values);
            var query = "insert into " + tableName + " (" + columns + ") values " + values + "";
            try
            {
                SqlCommand command = new SqlCommand(query, SqlConnection);
                Int32 y = command.ExecuteNonQuery();
               // command.Dispose();
                return y;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    SqlConnection.Close();
            //}
            return -1;
        }
        public Int32 insertOrUpdateQuery(string tableName, Dictionary<string, string> valuePairs, string columnName, string valueCondition)
        {
            SqlConnection.Open();
            var query = "select * from " + tableName + " where " + columnName + "='" + valueCondition + "'";

            try
            {
                SqlDataAdapter sqlData = new SqlDataAdapter(query, SqlConnection);
                DataSet data = new DataSet();
                sqlData.Fill(data);

                if (data.Tables[0].Rows.Count > 0)
                {
                    string condition = "";
                    var i = 0;
                    foreach (KeyValuePair<string, string> d in valuePairs)
                    {
                        if (i == 0)
                        {
                            condition += d.Key + "= '" + d.Value + "'";
                        }
                        else
                        {
                            condition += "," + d.Key + "= '" + d.Value + "'";
                        }
                        i++;
                    }

                    var updateQuery = "update set " + condition + " where " + columnName + "='" + valueCondition + "' ";
                    SqlCommand command = new SqlCommand(updateQuery, SqlConnection);
                    command.Dispose();
                    return command.ExecuteNonQuery();
                }
                else
                {
                    var columns = string.Join(",", valuePairs.Keys);
                    var values = string.Join(",", valuePairs.Values);
                    var insertQuery = "insert into " + tableName + " (" + columns + ") values " + values + "";
                    SqlCommand command = new SqlCommand(insertQuery, SqlConnection);
                    command.Dispose();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SqlConnection.Close();
            }
            return -1;
        }
        public void deleteQuery(string tableName, Dictionary<string,string> valuePairs)
        {
            SqlConnection.Open();
            string condition = "";
            var i = 0;
            foreach (KeyValuePair<string, string> d in valuePairs)
            {
                if (i == 0)
                {
                    condition += d.Key + "= '" + d.Value + "'";
                }
                else
                {
                    condition += "," + d.Key + "= '" + d.Value + "'";
                }
                i++;
            }

            var query = "delete " + tableName + " where " + condition + "";
            try
            {
                SqlCommand command = new SqlCommand(query, SqlConnection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SqlConnection.Close();
            }
        }
    }
}
