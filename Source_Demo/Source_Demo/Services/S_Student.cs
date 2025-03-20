using Source_Demo.Lib;
using Source_Demo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_Student
    {
        Task<ResponseData<List<M_Student>>> getListStudentParameter(string accessToken, int? status);
        Task<ResponseData<M_Student>> getStudent(string accessToken, int id);
        Task<ResponseData<M_Student>> Create(string accessToken, EM_Student model, string createdBy);
        Task<ResponseData<M_Student>> Update(string accessToken, EM_Student model, string updatedBy);
        Task<ResponseData<M_Student>> Delete(string accessToken, int id);
        Task<ResponseData<M_Student>> UpdateStatus(string accessToken, int id, int status, DateTime timer, string createdBy);
    }
    public class S_Student : IS_Student
    {
        private readonly ICallApi _callApi;
        public S_Student(ICallApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Student>>> getListStudentParameter(string accessToken, int? status)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Student>>(GlobalVariables.url_api + "Student/getListStudentParameter", dictPars);
        }
        public async Task<ResponseData<M_Student>> getStudent(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id}
            };
            return await _callApi.GetResponseDataAsync<M_Student>(GlobalVariables.url_api + "Student/getStudent", dictPars);
        }
        public async Task<ResponseData<M_Student>> Create(string accessToken, EM_Student model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"firstName", model.firstName},
                {"lastName", model.lastName},
                {"birthday", model.birthday?.ToString("yyyy-MM-dd")},
                {"gender", model.gender},
                {"address", model.address.HasValue ? model.address : 0},
                {"phoneNumber", model.phoneNumber},
                {"email", model.email},
                {"remark", model.remark},
                {"createdBy", model.createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Student>(GlobalVariables.url_api + "Student/Create", dictPars);
        }
        public async Task<ResponseData<M_Student>> Update(string accessToken, EM_Student model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"firstName", model.firstName},
                {"lastName", model.lastName},
                {"birthday", model.birthday?.ToString("yyyy-MM-dd")},
                {"gender", model.gender},
                {"address", model.address.HasValue ? model.address : 0},
                {"phoneNumber", model.phoneNumber},
                {"email", model.email},
                {"remark", model.remark},
                {"status", model.status},
                {"updatedBy", updatedBy},
                {"timer", model.timer?.ToString("O")},
            };
            return await _callApi.PostResponseDataAsync<M_Student>(GlobalVariables.url_api + "Student/Update", dictPars);
        }
        public async Task<ResponseData<M_Student>> Delete(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.PostResponseDataAsync<M_Student>(GlobalVariables.url_api + "Student/Delete", dictPars);
        }
        public async Task<ResponseData<M_Student>> UpdateStatus(string accessToken, int id, int status, DateTime timer, string createdBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"createdBy", createdBy},
                {"timer", timer.ToString("O")},
            };
            return await _callApi.PostResponseDataAsync<M_Student>(GlobalVariables.url_api + "Student/UpdateStatus", dictPars);
        }
    }
}
