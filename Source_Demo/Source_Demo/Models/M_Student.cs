using System;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_Student : M_BaseModel.BaseCustom
    {

        public int? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string lastNameSlug { get; set; }
        public string firstNameSlug { get; set; }
        public DateTime? birthday { get; set; }
        public int? gender { get; set; }
        public int? address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string remark { get; set; }
    }
    public class EM_Student : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên đệm")]
        [StringLength(20, ErrorMessage = "Họ và tên đệm có độ dài tối đa 20 ký tự")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(10, ErrorMessage = "Tên có độ dài tối đa 10 ký tự")]
        public string lastName { get; set; }
        public DateTime? birthday { get; set; }
        public int gender { get; set; }
        public int? address { get; set; }
        [StringLength(10, ErrorMessage = "Điện thoại có độ dài tối đa 20 ký tự")]
        public string phoneNumber { get; set; }
        [StringLength(50, ErrorMessage = "Email có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú có độ dài tối đa 150 ký tự")]
        public string remark { get; set; }
    }
}