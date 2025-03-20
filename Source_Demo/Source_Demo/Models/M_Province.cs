using System;
using System.ComponentModel.DataAnnotations;

namespace Source_Demo.Models
{
    public class M_Province : M_BaseModel.BaseCustom
    {
        public int id { get; set; }
        public double totalArea { get; set; }             // Diện tích tỉnh (km² hoặc ha, tùy đơn vị)
        public double cultivatedArea { get; set; }        // Diện tích canh tác (km² hoặc ha)
        public double elevation { get; set; }             // Độ cao so với mực nước biển (mét)
        public string rainfallInfo { get; set; }          // Thông tin lượng mưa (có thể là chuỗi mô tả hoặc số liệu)
        public double production { get; set; }            // Sản lượng (tấn, kg, tùy đơn vị)
        public bool? organicModel { get; set; }           // Mô hình hữu cơ (tuỳ chọn, nullable bool)
    }
    public class EM_Province : M_BaseModel.BaseCustom
    {
    }
}