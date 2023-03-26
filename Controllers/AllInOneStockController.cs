using AllinOneStock.Businesslogic;
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

        [HttpPost("Authentication")]
        public ActionResult<string> postAuthentication(CredentialModel credential)
        {
            try
            {
                string result = authentication.authentication(credential);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public ActionResult<string> postChangePassword(ChangePasswordModel credential)
        {
            try
            {
                string result = authentication.changePassword(credential);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("ChangeVerificationCode")]
        public ActionResult<string> postChangeVerificationCode(ChangeVerificationModel credential)
        {
            try
            {
                string result = authentication.changeVerifiationCode(credential);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllPriceView")]
        [Authorize]
        public ActionResult<List<PriceViewsList>> postPriceView()
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                List<PriceViewsList> result = priceView.getPriceViewAll(name);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UppdatePriceView")]
        [Authorize]
        public ActionResult updatePiceView(PriceViewsList priceViewList)
        {
            try
            {
                string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                priceView.updatePriceView(name, priceViewList);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("DeletePriceView")]
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

        public ActionResult getAllOrders(string clientId)
        {
            try
            {
                List<OrderModel> orderModels = orders.getAllOrders(clientId);
                return Ok(orderModels);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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

        [HttpGet("LoginPage")]
        public ActionResult getLoginPage()
        {
            return View("~/Views/Login_view.cshtml");
        }
    }
}
