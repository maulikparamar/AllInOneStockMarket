﻿using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using AllInOneStockMarket.Businesslogic;
using AllInOneStockMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace AllinOneStock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllInOneStockController : Controller
    {
        private readonly Authentication authentication = new Authentication();
        private readonly PriceView priceView = new PriceView();
        private readonly Orders orders = new Orders();
        private readonly ILogger<AllInOneStockController> _logger;

        public AllInOneStockController(ILogger<AllInOneStockController> logger)
        {
            _logger = logger;
        }

        #region client-side
        [HttpPost("Authentication")]
        public ActionResult<string> postAuthentication([FromBody] CredentialModel credential)
        {
            try
            {
                CredentialResponseModel result = authentication.authentication(credential);
                if (result.goodorbadresponse)
                {
                    return result.msg;
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> postChangePassword(ChangePasswordModel credential)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                if (name != credential.UserName) return Unauthorized();
                CredentialResponseModel result = authentication.changePassword(credential);
                if (result.goodorbadresponse)
                {
                    return result.msg;
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("ChangeVerificationCode")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> postChangeVerificationCode(ChangeVerificationModel credential)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                if (name != credential.UserName) return Unauthorized();
                CredentialResponseModel result = authentication.changeVerifiationCode(credential);
                if (result.goodorbadresponse)
                {
                    return result.msg;
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllBrokers")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<ClientDetails>> postAllBrokers(string clientType)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                if (name == "") return Unauthorized();
                userType user;
                if(!Enum.TryParse(clientType,out user))
                {
                    return StatusCode(StatusCodes.Status404NotFound,"Not Found");
                }


                List<ClientDetails> result = authentication.getClientDetails(user);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllPriceView")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<PriceViewsList>> postAllPriceView(string clientId)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                if (name != clientId) return Unauthorized();
                List<PriceViewsList> result = priceView.getPriceViewAll(name);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdatePriceView")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult updatePiceView(UpdatePriceViewsList priceViewList)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                int result = priceView.updatePriceView(name, priceViewList);
                if(result == -1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("DeletePriceView")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult deletepriceview(string priceviewname)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                priceView.deletePriceView(name, priceviewname);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetScripDetails")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult getScripDetails(string tokenId)
        {
            try
            {
                ScripModel item = priceView.getScriDetails(tokenId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("SearchScripDetails")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult getSearchScripDetails(string scripName)
        {
            try
            {
                List<ItemScrip> item = priceView.getSearchScrip(scripName).Take(50).ToList();
                return Ok(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllOrders")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult getAllOrders(string clientId)
        {
            try
            {
                List<getOrderModel> orderModels = orders.getAllOrders(clientId);
                return Ok(orderModels);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("DeleteOrder")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult deleteOrder(string clientId, int orderId)
        {
            try
            {
                orders.deleteOrder(clientId, orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateOrder")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult createOrder(OrderModel model)
        {
            try
            {
                orders.createOrder(model);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetClientDetails")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult getClientDetails(string clientId)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                if (name != clientId) return Unauthorized();
                var i = authentication.getFullClientDetails(clientId);
                return Ok(i);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion


        #region Server-side
        //[HttpPost("GetAllBrokers")]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("GetAllClient")]
        public ActionResult<List<FullClientDetails>> postAllClientList()
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
               // string type = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

               // if (name == "" && type == userType.Admin.ToString()) return Unauthorized();

                List<FullClientDetails> result = authentication.getFullClientDetails(userType.Client);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdateClient")]
        public ActionResult postUpdateClient(FullClientDetails clientDetails)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
               // string type = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                //&& type == userType.Admin.ToString()
                if (name == "" ) return Unauthorized();

                int result = authentication.createOrUpdateClient(clientDetails);
                result = authentication.createPriceViewNewUser(clientDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllOrdersDetails")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<getOrderModel>> postAllOrdersDetails()
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                // string type = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

                // if (name == "" && type == userType.Admin.ToString()) return Unauthorized();

                List<getOrderModel> result = orders.getBrokerAllOrders(name);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdateOrder")]
        public ActionResult postUpdateOrder(orderModelRequest order)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                // string type = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                //&& type == userType.Admin.ToString()
                if (name == "") return Unauthorized();

                OrderStatus o;

                if(!Enum.TryParse(order.status, out o))
                {
                    return NoContent();
                }

                int result = orders.updateStatus(o, order.id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("DeleteOrders")]
        public ActionResult postDeleteOrders(deleteOrderModelRequest modelRequest)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                // string type = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                //&& type == userType.Admin.ToString()
                if (name == "") return Unauthorized();

                 orders.deleteOrder(modelRequest.client, modelRequest.id);

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("LoginPage")]
        public ActionResult getLoginPage()
        {
            return View("~/Views/Login_view.cshtml");
        }

        [HttpGet("HomePage")]
        public ActionResult getHomePage()
        {
            return View("~/Views/HomePage.cshtml");
        }
        [HttpGet("ClientsList")]
        public ActionResult getClientList()
        {
            return View("~/Views/ClientsList_view.cshtml");
        }
        [HttpGet("OrdersList")]
        public ActionResult getOrdersList()
        {
            return View("~/Views/OrdersList_view.cshtml");
        }
        #endregion
    }
}
