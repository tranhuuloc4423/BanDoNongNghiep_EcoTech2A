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
    public class StudentManageController : BaseController<StudentManageController>
    {
        private readonly IS_Student _s_Student;

        public StudentManageController(IS_Student student)
        {
            _s_Student = student;
        }

        //View chính
        public IActionResult Index()
        {
            return View();
        }

        //Get ListStudent dưới dạng Json 
        [HttpGet]
        public async Task<JsonResult> GetList(int? status)
        {
            var res = await _s_Student.getListStudentParameter(accessToken, status);
            return Json(new M_JResult(res));
        }

        //View Add Student
        [HttpGet]
        public IActionResult P_Add()
        {
            return PartialView();
        }

        //Validate Form Add Student
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> P_Add(EM_Student model)
        {
            M_JResult jResult = new M_JResult();
            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            var res = await _s_Student.Create(accessToken, model, userId);
            return Json(jResult.MapData(res));
        }

        [HttpGet]
        public IActionResult P_Edit()
        {
            return PartialView();

        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> P_Edit(EM_Student model)
        {
            M_JResult jResult = new M_JResult();
            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            var res = await _s_Student.Update(accessToken, model, userId);
            return Json(jResult.MapData(res));
        }

        //[HttpGet]
        //public async Task<IActionResult> P_View(int id)
        //{
        //    var res = await _s_Student.getStudent<M_Student>(accessToken, id);
        //    return res.result == 1 && res.data != null ? PartialView(res.data) : Json(new M_JResult(res));
        //}
    }
}
