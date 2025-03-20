using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Source_Demo.ExtensionMethods;
using Source_Demo.Lib;
using Source_Demo.Models;
using Source_Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class MapController : BaseController<MapController>
    {
        private readonly IS_Enterprise _s_Enterprise;
        private readonly IS_Province _s_Province;
        private readonly IS_District _s_District;

        public MapController(IS_Enterprise enterprise, IS_Province province, IS_District district)
        {
            _s_Enterprise = enterprise;
            _s_Province = province;
            _s_District = district;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
