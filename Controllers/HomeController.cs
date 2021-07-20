using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZarinpalGateway.Models;
using ZarinpalGateway.Models.Enums;
using ZarinpalSandbox;

namespace ZarinpalGateway.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static List<PaymentModel> _payments = new List<PaymentModel>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(_payments);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(int amount)
        {
            var payId = CreateNewPayment(amount);
            var callBackUrl = $"https://localhost:44396/Home/Return/{payId}";
            var description = $"پرداخت مبلغ {amount}";
            var email = "samanazadi1996@gmail.com";
            var phoneNumber = "09304241296";
            var payment = new Payment(amount);
            var result = await payment.PaymentRequest(description, callBackUrl, email, phoneNumber);
            SetAuthorityPayment(payId, result.Authority);
            if (result.Status == 100)
            {
                return Redirect(result.Link);
            }
            return RedirectToAction("Index", "Home", new { message = $"خطا {result.Status}" });
        }

        public async Task<IActionResult> Return(Guid id)
        {
            string status = Request.Query["Status"];
            string authority = Request.Query["Authority"];
            if (!string.IsNullOrEmpty(authority) && !string.IsNullOrEmpty(status))
            {
                var pay = _payments.Single(p => p.Id.Equals(id) && p.Authority.Equals(authority));
                if (status.ToLower().Equals("ok"))
                {
                    var payment = new Payment(pay.Amount);
                    var result = await payment.Verification(authority);
                    if (result.Status == 100)
                    {
                        pay.State = States.Succeed;
                        return RedirectToAction("Index", "Home", new { message = $"مبلغ {pay.Amount.ToString("#,##")} پرداخت شد" });
                    }
                }
                pay.State = States.Failed;
                return RedirectToAction("Index", "Home", new { message = "پرداخت لغو شد" });
            }
            return RedirectToAction("Index", "Home", new { message = "درخواست جعلی" });
        }
        private Guid CreateNewPayment(int amount)
        {
            var pay = new PaymentModel(amount);
            _payments.Add(pay);
            return pay.Id;
        }
        private void SetAuthorityPayment(Guid id, string authority)
        {
            _payments.Single(p => p.Id.Equals(id)).Authority = authority;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
