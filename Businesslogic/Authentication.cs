﻿using AllinOneStock.Models;
using AllInOneStockMarket;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static AllInOneStockMarket.Models.Enums;

namespace AllinOneStock.Businesslogic
{
    public class Authentication
    {
        public string authentication(CredentialModel credential)
        {
            try
            {
                SqlController sql = new SqlController();
                
                Dictionary<string, string> valuePairs = new Dictionary<string, string>();

                valuePairs.Add("[client_id]", credential.UserName);

                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add("[password]", credential.Password);
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs.Add("[verification__code]", credential.VerificationCode.ToString());
                        SqlDataReader verificationDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                        if (verificationDetails != null && verificationDetails.HasRows)
                        {
                            verificationDetails.Close();
                            valuePairs.Add("[type]", ((Int16)credential.type).ToString());
                            SqlDataReader typeDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                            if (typeDetails != null && typeDetails.HasRows)
                            {
                                return GenerateToken(credential);
                            }
                            else
                            {
                                return "Wrong Type";
                            }
                        }
                        else
                        {
                            return "Wrong Verification Code";
                        }
                    }
                    else
                    {
                        return "wrong password";
                    }
                }
                else
                {
                    return "Username Unavailable";
                }
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string changePassword(ChangePasswordModel model)
        {
            try
            {
                SqlController sql = new SqlController();

                Dictionary<string, string> valuePairs = new Dictionary<string, string>();

                valuePairs.Add("[client_id]", model.UserName);
                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add("[password]", model.Password);
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs.Add("[password]", model.NewPassword);
                        sql.insertOrUpdateQuery(SqlDatabaseTable.user_details, valuePairs, "[client_id]", model.UserName);
                        return "Successfully Changed Password";
                    }
                    else
                    {
                        return "Wrong Password";
                    }
                }
                else
                {
                    return "Wrong UserName";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string changeVerifiationCode(ChangeVerificationModel model)
        {
            try
            {
                SqlController sql = new SqlController();

                Dictionary<string, string> valuePairs = new Dictionary<string, string>();

                valuePairs.Add("[client_id]", model.UserName);
                SqlDataReader userDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);
                if (userDetails != null && userDetails.HasRows)
                {
                    userDetails.Close();
                    valuePairs.Add("[verification__code]", model.VerificationCode.ToString());
                    SqlDataReader passwordDetails = sql.selectConditionQuery(null, SqlDatabaseTable.user_details, valuePairs);

                    if (passwordDetails != null && passwordDetails.HasRows)
                    {
                        passwordDetails.Close();
                        valuePairs.Add("[verification__code]", model.NewVerificationCode.ToString());
                        sql.insertOrUpdateQuery(SqlDatabaseTable.user_details, valuePairs, "[client_id]", model.UserName);
                        return "Successfully Changed Verification Code";
                    }
                    else
                    {
                        return "Wrong Verification Code";
                    }
                }
                else
                {
                    return "Wrong UserName";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string GenerateToken(CredentialModel credential)
        {
            var securityKey =   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Jwtkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                        {
                new Claim(ClaimTypes.NameIdentifier,credential.UserName),
                new Claim(ClaimTypes.Role,credential.type.ToString()),
            };
            var token = new JwtSecurityToken(Startup.JwtIssuer,
                Startup.JwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
