using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_District : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public double production { get; set; }                    // Sản lượng (tấn, kg, tùy đơn vị)
        public Dictionary<int, Dictionary<string, double>> cropAreaByYear { get; set; } // Diện tích các loại cây trồng theo từng năm
        public int totalFarmers { get; set; }                     // Tổng số nông dân canh tác
        public string harvestTime { get; set; }                   // Thời gian thu hoạch (chuỗi mô tả)
    }
    public class EM_District : M_BaseModel.BaseCustom
    {
    }
}