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

namespace AppStudentiWeb.Controllers
{
    public class ProvincesController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;
        private IProvincesManager pm;

        public ProvincesController(IOptions<AppSettings> options, IHostingEnvironment server, IProvincesManager provincesmanager)
        {
            appsettings = options.Value;
            host = server;
            pm = provincesmanager;
        }

        public List<ProvincesViewModel> GetAllProvince()
        {
            List<ProvincesViewModel> list = pm.GetAllProvince();
            return list;
        }
    }
}
