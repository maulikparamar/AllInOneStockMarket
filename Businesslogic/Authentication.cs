using AllinOneStock.Models;
using AllInOneStockMarket;
using AllInOneStockMarket.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static AllInOneStockMarket.Models.Enums;

namespace AllinOneStock.Businesslogic
{
    public class Authentication
    {
        public CredentialResponseModel authentication(CredentialModel credential)
        {
            try
            {
                SqlController sql = new SqlController();
                
                Dictionary<string, string> valuePairs = new Dictionary<string, string>();
                
                valuePairs.Add(ClientDetailsColumnName.client_id, credential.UserId);

                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add(ClientDetailsColumnName.client_password, credential.Password);
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs.Add(ClientDetailsColumnName.client_verification_code, credential.VerificationCode.ToString());
                        SqlDataReader verificationDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                        if (verificationDetails != null && verificationDetails.HasRows)
                        {
                            verificationDetails.Close();
                            valuePairs.Add(ClientDetailsColumnName.client_type, ((Int16)credential.type).ToString());
                            SqlDataReader typeDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                            if (typeDetails != null && typeDetails.HasRows)
                            {
                                return new CredentialResponseModel() { msg = GenerateToken(credential) , goodorbadresponse = true};
                            }
                            else
                            {
                                return new CredentialResponseModel() { msg = "Wrong Type" ,goodorbadresponse = false};
                            }
                        }
                        else
                        {
                            return new CredentialResponseModel() { msg = "Wrong Verification Code", goodorbadresponse = false };
                        }
                    }
                    else
                    {
                        return new CredentialResponseModel() { msg = "wrong password", goodorbadresponse = false };
                    }
                }
                else
                {
                    return new CredentialResponseModel() { msg = "Username Unavailable", goodorbadresponse = false };
                }
            }catch(Exception ex)
            {
                return new CredentialResponseModel() { msg = ex.Message ,goodorbadresponse = false};
            }
        }

        public CredentialResponseModel changePassword(ChangePasswordModel model)
        {
            try
            {
                SqlController sql = new SqlController();

                Dictionary<string, string> valuePairs = new Dictionary<string, string>();

                valuePairs.Add(ClientDetailsColumnName.client_id, model.UserName);
                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add(ClientDetailsColumnName.client_password, model.Password);
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs[ClientDetailsColumnName.client_password] = model.NewPassword;
                        sql.insertOrUpdateQuery(SqlDatabaseTable.user_details, valuePairs, ClientDetailsColumnName.client_id, model.UserName);
                        return new CredentialResponseModel() { msg = "Successfully Changed Password", goodorbadresponse = true };
                    }
                    else
                    {
                        return new CredentialResponseModel() { msg = "Wrong Password", goodorbadresponse = false };
                    }
                }
                else
                {
                    return new CredentialResponseModel() { msg = "Wrong UserName", goodorbadresponse = false };
                }
            }
            catch (Exception ex)
            {
                return new CredentialResponseModel() { msg = ex.Message, goodorbadresponse = false };
            }
        }
        public CredentialResponseModel changeVerifiationCode(ChangeVerificationModel model)
        {
            try
            {
                SqlController sql = new SqlController();

                Dictionary<string, string> valuePairs = new Dictionary<string, string>();

                valuePairs.Add(ClientDetailsColumnName.client_id, model.UserName);
                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add(ClientDetailsColumnName.client_verification_code, model.VerificationCode.ToString());
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs[ClientDetailsColumnName.client_verification_code] = model.NewVerificationCode.ToString();
                        sql.insertOrUpdateQuery(SqlDatabaseTable.user_details, valuePairs, ClientDetailsColumnName.client_id, model.UserName);
                        return new CredentialResponseModel() { msg = "Successfully Changed Verification Code", goodorbadresponse = true };
                    }
                    else
                    {
                        return new CredentialResponseModel() { msg = "Wrong Verification Code", goodorbadresponse = false };
                    }
                }
                else
                {
                    return new CredentialResponseModel() { msg = "Wrong UserName", goodorbadresponse = false };
                }
            }
            catch (Exception ex)
            {
                return new CredentialResponseModel() { msg = ex.Message, goodorbadresponse = false };
            }
        }

        public List<ClientDetails> getClientDetails(userType type)
        {
            List<ClientDetails> list = new();
            try
            {
                SqlController controller = new SqlController();
                Dictionary<string, string> valuePairs = new();
                valuePairs.Add(ClientDetailsColumnName.client_type, ((int)type).ToString());
                SqlDataReader dataReader = controller.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        ClientDetails clientDetails = new();
                        clientDetails.client_id = dataReader["clientId"].ToString();
                        clientDetails.client_name = dataReader["clientName"].ToString();
                        clientDetails.client_phoneno = Convert.ToInt64(dataReader["clientPhoneNo"].ToString());
                        list.Add(clientDetails);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new();
        }


        private string GenerateToken(CredentialModel credential)
        {
            var securityKey =   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Jwtkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                        {
                new Claim(ClaimTypes.NameIdentifier,credential.UserId),
                new Claim(ClaimTypes.Role,credential.type.ToString()),
            };
            var token = new JwtSecurityToken(null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
