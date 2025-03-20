using Source_Demo.Lib;
using Source_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_Province
    {
        Task<ResponseData<List<M_Province>>> getListProvinceParameter();
        Task<ResponseData<M_Province>> getProvince(int id);
    }

    public class S_Province : IS_Province
    {
        private readonly List<M_Province> _mockProvinces;

        public S_Province(ICallApi callApi)
        {
            // Dữ liệu mẫu được khởi tạo trong constructor
            _mockProvinces = new List<M_Province>
            {
                new M_Province
                {
                    id = 1,
                    totalArea = 10500.5,
                    cultivatedArea = 4500.0,
                    elevation = 120.5,
                    rainfallInfo = "1500 mm/năm",
                    production = 20000.0,
                    organicModel = true
                },
                new M_Province
                {
                    id = 2,
                    totalArea = 8500.0,
                    cultivatedArea = 3200.5,
                    elevation = 80.0,
                    rainfallInfo = "1200 mm/năm",
                    production = 15000.0,
                    organicModel = false
                },
                new M_Province
                {
                    id = 3,
                    totalArea = 12000.0,
                    cultivatedArea = 5000.0,
                    elevation = 150.0,
                    rainfallInfo = "1800 mm/năm",
                    production = 25000.0,
                    organicModel = null
                },
                new M_Province
                {
                    id = 4,
                    totalArea = 9500.5,
                    cultivatedArea = 4000.0,
                    elevation = 200.0,
                    rainfallInfo = "1400 mm/năm",
                    production = 18000.0,
                    organicModel = true
                },
                new M_Province
                {
                    id = 5,
                    totalArea = 11000.0,
                    cultivatedArea = 4700.0,
                    elevation = 90.5,
                    rainfallInfo = "1600 mm/năm",
                    production = 22000.0,
                    organicModel = false
                }
            };
        }

        public async Task<ResponseData<List<M_Province>>> getListProvinceParameter()
        {
            await Task.Delay(500);

            var response = new ResponseData<List<M_Province>>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = true, // Vì trả về danh sách
                dataDescription = "Danh sách các tỉnh",
                data = _mockProvinces,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };

            return response;
        }

        public async Task<ResponseData<M_Province>> getProvince(int id)
        {
            await Task.Delay(300); // Giả lập thời gian chờ giống như gọi API (300ms)

            var province = _mockProvinces.FirstOrDefault(p => p.id == id);
            if (province == null)
            {
                return new ResponseData<M_Province>
                {
                    time = Utilities.CurrentTimeSeconds(),
                    isListData = false,
                    dataDescription = string.Empty,
                    data = default(M_Province),
                    data2nd = null,
                    error = new error { code = 404, message = $"Không tìm thấy tỉnh với id = {id}" }
                };
            }

            return new ResponseData<M_Province>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = false,
                dataDescription = $"Thông tin tỉnh với id = {id}",
                data = province,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };
        }
    }
}