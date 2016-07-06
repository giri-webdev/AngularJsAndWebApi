using Newtonsoft.Json;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using WebApplication1.CustomAttributes;
using WebApplication1.Models;

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

        //Odata queries
        public ActionResult Products()
        {
            //HttpResponseMessage responseMessage = client.GetAsync("api/Values/GetProducts?$filter=Id+gt+3").Result;
            //HttpResponseMessage responseMessage = client.GetAsync("api/Values/GetProducts?$top=3").Result;
            //HttpResponseMessage responseMessage = client.GetAsync("api/Values/GetProducts?$orderby=Id+desc").Result;
            HttpResponseMessage responseMessage = client.GetAsync("api/Values/GetProducts?$select=Id+,+Name").Result;
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(response);

            return View(products);
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