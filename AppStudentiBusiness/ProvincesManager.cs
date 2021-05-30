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
using AppStudentiData;
using Microsoft.EntityFrameworkCore;

namespace AppStudentiBusiness
{
    public class ProvincesManager : IProvincesManager
    {
        private AppSettings appsettings;
        private appstudentiContext db;

        public ProvincesManager(IOptions<AppSettings> options, appstudentiContext context)
        {
            appsettings = options.Value;
            db = context;
        }

        public List<ProvincesViewModel> GetAllProvince()
        {
            List<ProvincesViewModel> list;

            var query = from p in db.Province.AsNoTracking()
                        select new ProvincesViewModel()
                        {
                            ProvinceId = p.ProvinceId,
                            ProvinceName = p.ProvinceName
                        };

            list = query.ToList();


            return list;
        }
    }
}
