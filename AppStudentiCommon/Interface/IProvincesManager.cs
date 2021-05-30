using AppStudentiCommon.Model;
using AppStudentiCommon.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Interface
{
    public interface IProvincesManager
    {
        List<ProvincesViewModel> GetAllProvince();
    }
}
