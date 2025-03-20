using Source_Demo.Lib;
using Source_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source_Demo.Services
{
    public interface IS_District
    {
        Task<ResponseData<List<M_District>>> getListDistrictParameter();
        Task<ResponseData<M_District>> getDistrict(int id);
    }

    public class S_District : IS_District
    {
        private readonly List<M_District> _mockDistricts;

        public S_District(ICallApi callApi)
        {
            _mockDistricts = new List<M_District>
            {
                new M_District
                {
                    id = 1,
                    production = 5000.0,
                    cropAreaByYear = new Dictionary<int, Dictionary<string, double>>
                    {
                        { 2023, new Dictionary<string, double> { { "lúa", 300.0 }, { "ngô", 150.0 } } },
                        { 2024, new Dictionary<string, double> { { "lúa", 320.5 } } }
                    },
                    totalFarmers = 1200,
                    harvestTime = "Tháng 9 - Tháng 10"
                },
                new M_District
                {
                    id = 2,
                    production = 3000.0,
                    cropAreaByYear = new Dictionary<int, Dictionary<string, double>>
                    {
                        { 2023, new Dictionary<string, double> { { "cà phê", 200.0 }, { "tiêu", 100.0 } } },
                        { 2024, new Dictionary<string, double> { { "cà phê", 220.0 } } }
                    },
                    totalFarmers = 800,
                    harvestTime = "Tháng 11 - Tháng 12"
                },
                new M_District
                {
                    id = 3,
                    production = 4500.0,
                    cropAreaByYear = new Dictionary<int, Dictionary<string, double>>
                    {
                        { 2023, new Dictionary<string, double> { { "lúa", 400.0 }, { "khoai", 120.0 } } },
                        { 2024, new Dictionary<string, double> { { "lúa", 410.0 } } }
                    },
                    totalFarmers = 1500,
                    harvestTime = "Tháng 8 - Tháng 9"
                },
                new M_District
                {
                    id = 4,
                    production = 3500.0,
                    cropAreaByYear = new Dictionary<int, Dictionary<string, double>>
                    {
                        { 2023, new Dictionary<string, double> { { "trà", 180.0 }, { "rau", 90.0 } } },
                        { 2024, new Dictionary<string, double> { { "trà", 200.0 } } }
                    },
                    totalFarmers = 900,
                    harvestTime = "Tháng 10 - Tháng 11"
                },
                new M_District
                {
                    id = 5,
                    production = 6000.0,
                    cropAreaByYear = new Dictionary<int, Dictionary<string, double>>
                    {
                        { 2023, new Dictionary<string, double> { { "lúa", 500.0 }, { "đậu", 200.0 } } },
                        { 2024, new Dictionary<string, double> { { "lúa", 520.0 } } }
                    },
                    totalFarmers = 2000,
                    harvestTime = "Tháng 7 - Tháng 8"
                }
            };
        }

        public async Task<ResponseData<List<M_District>>> getListDistrictParameter()
        {
            await Task.Delay(500); // Giả lập thời gian chờ

            var response = new ResponseData<List<M_District>>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = true,
                dataDescription = "Danh sách các quận/huyện",
                data = _mockDistricts,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };

            return response;
        }

        public async Task<ResponseData<M_District>> getDistrict(int id)
        {
            await Task.Delay(300); // Giả lập thời gian chờ

            var district = _mockDistricts.FirstOrDefault(d => d.id == id);
            if (district == null)
            {
                return new ResponseData<M_District>
                {
                    time = Utilities.CurrentTimeSeconds(),
                    isListData = false,
                    dataDescription = string.Empty,
                    data = default(M_District),
                    data2nd = null,
                    error = new error { code = 404, message = $"Không tìm thấy quận/huyện với id = {id}" }
                };
            }

            return new ResponseData<M_District>
            {
                time = Utilities.CurrentTimeSeconds(),
                isListData = false,
                dataDescription = $"Thông tin quận/huyện với id = {id}",
                data = district,
                data2nd = null,
                error = new error { code = 0, message = "Thành công" }
            };
        }
    }
}