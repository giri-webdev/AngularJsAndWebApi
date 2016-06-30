using Newtonsoft.Json;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication1.CustomAttributes;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
   [AuthorizeUser]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new RegisterBindingModel());
        }

        [HttpPost]
        public ActionResult AddUser(RegisterBindingModel model)
        {
       
            HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Account/Register",model ).Result;
            var responseCreate = responseMessage.Content.ReadAsStringAsync().Result;
            var message = JsonConvert.DeserializeObject<string>(responseCreate);
            TempData["message"] = message;
            return View();
        }

        
        [HttpGet]
        public ActionResult ListEmployee()
        {
            HttpResponseMessage responseMessage = client.GetAsync("api/Values/Get").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                var employees = JsonConvert.DeserializeObject<List<string>>(result);
                return View(employees);
            }

            return View();
        }


        [HttpGet]
        public ActionResult GetClaims()
        {
           // HttpResponseMessage response = client.GetAsync("api/Account/AddClaim?value=Giri").Result;
            //if (response.IsSuccessStatusCode)
            //{
                HttpResponseMessage responseMessage = client.GetAsync("api/Values/GetClaims").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    var claims = JsonConvert.DeserializeObject<List<ExternalLoginViewModel>>(result);
                    return View(claims);
                }
            //}

            return View();
        }
    }
}