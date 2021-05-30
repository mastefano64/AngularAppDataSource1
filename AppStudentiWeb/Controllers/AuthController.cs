using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using AppStudentiCommon;
using AppStudentiCommon.Model;
using AppStudentiCommon.Interface;
using AppStudentiWeb.Helper;
using AppStudentiCommon.Model.ViewModel;
using AppStudentiCommon.Model.Search;

namespace AppStudentiWeb.Controllers
{
    public class AuthController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;

        public AuthController(IOptions<AppSettings> options, IHostingEnvironment server)
        {
            appsettings = options.Value;
            host = server;
        }

        [HttpPost]
        public bool Login([FromBody]AuthUser user)
        {
            if (user.Username == "username1" && user.Password == "password1")
                return true;
            else return false;
        }

        [HttpGet]
        public bool Logout()
        {
            return false;
        }

        [HttpGet]
        public IActionResult Error1()
        {
            Exception ex = new Exception("Error1");
            return StatusCode(403);
        }

        [HttpGet]
        public IActionResult Error2()
        {
            Exception ex = new Exception("Error2");
            throw ex;
        }
    }
}
