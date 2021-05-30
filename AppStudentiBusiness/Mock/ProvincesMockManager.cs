using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using AppStudentiCommon;
using AppStudentiCommon.Model;
using AppStudentiCommon.Interface;
using AppStudentiCommon.Model.ViewModel;
using AppStudentiCommon.Model.Search;

namespace AppStudentiBusiness.Mock
{
    public class ProvincesMockManager : IProvincesManager
    {
        private AppSettings appsettings;

        public ProvincesMockManager(IOptions<AppSettings> options)
        {
            appsettings = options.Value;
        }

        public List<ProvincesViewModel> GetAllProvince()
        {
            List<ProvincesViewModel> list = new List<ProvincesViewModel>();

            list.Add(new ProvincesViewModel()
            {
                ProvinceId = "at",
                ProvinceName = "Asti"
            });
            list.Add(new ProvincesViewModel() {
                ProvinceId = "ge",
                ProvinceName = "Genova"
            });
            list.Add(new ProvincesViewModel()
            {
                ProvinceId = "li",
                ProvinceName = "Livorno"
            });
            list.Add(new ProvincesViewModel()
            {
                ProvinceId = "mi",
                ProvinceName = "Milano"
            });
            list.Add(new ProvincesViewModel()
            {
                ProvinceId = "to",
                ProvinceName = "Torino"
            });

            return list;
        }
    }
}
