using AllinOneStock.Businesslogic;
using AllinOneStock.Models;
using AllInOneStockMarket.Businesslogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace AllinOneStock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllInOneStockController : ControllerBase
    {
        private readonly ILogger<AllInOneStockController> _logger;

        public AllInOneStockController(ILogger<AllInOneStockController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Authentication")]
        public ActionResult<string> postAuthentication(CredentialModel credential)
        {
            Authentication authentication = new Authentication();
            string result =  authentication.authentication(credential);
            return result;
        }

        [HttpPost("ChangePassword")]
        public ActionResult<string> postChangePassword(ChangePasswordModel credential)
        {
            Authentication authentication = new Authentication();
            string result = authentication.changePassword(credential);
            return result;
        }

        [HttpPost("ChangeVerificationCode")]
        public ActionResult<string> postChangeVerificationCode(ChangeVerificationModel credential)
        {
            Authentication authentication = new Authentication();
            string result = authentication.changeVerifiationCode(credential);
            return result;
        }

        [HttpPost("GetAllPriceView")]
        [Authorize]
        public ActionResult<List<PriceViewsList>> postPriceView()
        {
            string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            PriceView priceView = new PriceView();
            List<PriceViewsList> result = priceView.getPriceViewAll(name);
            return result;
        }

        [HttpPut("UppdatePriceView")]
        [Authorize]
        public ActionResult updatePiceView(PriceViewsList priceView)
        {
            string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            PriceView priceView1 = new();
            priceView1.updatePriceView(name, priceView);
            return Ok();
        }

        [HttpPost("DeletePriceView")]
        public ActionResult deletepriceview(string priceviewname)
        {
            string name = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            PriceView priceview1 = new();
            priceview1.deletePriceView(name, priceviewname);
            return Ok();
        }

        [HttpPost("GetScripDetails")]
        public ActionResult getScripDetails(string tokenId)
        {
            PriceView priceview = new();
            ItemScrip item = priceview.getScriDetails(tokenId);
            return Ok(item);
        }

        public ActionResult getAllOrders()
        {
            return null;
        }

        public ActionResult deleteOrder()
        {
            return null;
        }

        public ActionResult marketOrder()
        {
            return null;
        }
    }
}
