using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTest.Models.Entity
{
    [Table("Province")]
    public class Province
    {
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
}
