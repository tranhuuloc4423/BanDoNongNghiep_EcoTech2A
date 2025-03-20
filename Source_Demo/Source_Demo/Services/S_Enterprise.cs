using Source_Demo.Lib;
using Source_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_Enterprise
    {
        Task<ResponseData<List<M_Enterprise>>> getListEnterpriseParameter();
        Task<ResponseData<M_Enterprise>> getEnterprise(int id);
    }

    public class S_Enterprise : IS_Enterprise
    {
        private readonly List<M_Enterprise> _mockEnterprises;

        public S_Enterprise(ICallApi callApi)
        {
            _mockEnterprises = new List<M_Enterprise>
            {
                new M_Enterprise
                {
                    id = 1,
                    name = "Công ty TNHH ABC",
                    englishName = "ABC Co., Ltd",
                    shortName = "ABC",
                    address = "123 Đường Láng, Hà Nội",
                    phone = "0901234567",
                    email = "abc@example.com",
                    director = "Nguyễn Văn A",
                    location = "Khu công nghiệp Bắc",
                    website = "www.abc.com",
                    type = "TNHH"
                },
                new M_Enterprise
                {
                    id = 2,
                    name = "Công ty CP XYZ",
                    englishName = "XYZ Corporation",
                    shortName = "XYZ",
                    address = "456 Lê Lợi, TP.HCM",
                    phone = "0912345678",
                    email = "xyz@example.com",
                    director = "Trần Thị B",
                    location = "Khu trung tâm",
                    website = "www.xyzcorp.com",
                    type = "Cổ phần"
                },
                new M_Enterprise
                {
                    id = 3,
                    name = "Doanh nghiệp Tư nhân DEF",
                    englishName = "DEF Private Enterprise",
                    shortName = "DEF",
                    address = "789 Nguyễn Huệ, Đà Nẵng",
                    phone = "0933456789",
                    email = "def@example.com",
                    director = "Lê Văn C",
                    location = "Khu ven biển",
                    website = "www.def.vn",
                    type = "Tư nhân"
                },
                new M_Enterprise
                {
                    id = 4,
                    name = "Công ty TNHH GHI",
                    englishName = "GHI Co., Ltd",
                    shortName = "GHI",
                    address = "101 Trần Phú, Nha Trang",
                    phone = "0944567890",
                    email = "ghi@example.com",
                    director = "Phạm Thị D",
                    location = "Khu du lịch",
                    website = "www.ghi.com",
                    type = "TNHH"
                },
                new M_Enterprise
                {
                    id = 5,
                    name = "Công ty CP JKL",
                    englishName = "JKL Corporation",
                    shortName = "JKL",
                    address = "202 Hùng Vương, Cần Thơ",
                    phone = "0955678901",
                    email = "jkl@example.com",
                    director = "Hoàng Văn E",
                    location = "Khu đô thị",
                    website = "www.jklcorp.com",
                    type = "Cổ phần"
                }
            };
        }

        public async Task<ResponseData<List<M_Enterprise>>> getListEnterpriseParameter()
        {
            await Task.Delay(500); // Giả lập thời gian chờ

            var response = new ResponseData<List<M_Enterprise>>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = true,
                dataDescription = "Danh sách các doanh nghiệp",
                data = _mockEnterprises,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };

            return response;
        }

        public async Task<ResponseData<M_Enterprise>> getEnterprise(int id)
        {
            await Task.Delay(300); // Giả lập thời gian chờ

            var enterprise = _mockEnterprises.FirstOrDefault(e => e.id == id);
            if (enterprise == null)
            {
                return new ResponseData<M_Enterprise>
                {
                    time = Utilities.CurrentTimeSeconds(),
                    isListData = false,
                    dataDescription = string.Empty,
                    data = default(M_Enterprise),
                    data2nd = null,
                    error = new error { code = 404, message = $"Không tìm thấy doanh nghiệp với id = {id}" }
                };
            }

            return new ResponseData<M_Enterprise>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = false,
                dataDescription = $"Thông tin doanh nghiệp với id = {id}",
                data = enterprise,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };
        }
    }
}