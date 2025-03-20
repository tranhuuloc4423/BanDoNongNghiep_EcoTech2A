using System;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_Enterprise : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public string name { get; set; }              // Tên
        public string englishName { get; set; }       // Tên tiếng Anh
        public string shortName { get; set; }         // Tên viết tắt
        public string address { get; set; }           // Địa chỉ
        public string phone { get; set; }             // Điện thoại
        public string email { get; set; }             // Email
        public string director { get; set; }          // Giám đốc
        public string location { get; set; }  // Vị trí doanh nghiệp theo địa phương
        public string website { get; set; }           // Website
        public string type { get; set; }    // Loại hình
    }
    public class EM_Enterprise : M_BaseModel.BaseCustom
    {
        
    }
}