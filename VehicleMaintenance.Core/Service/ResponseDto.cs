using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Core.Service
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorCode { get; set; }
        public string Message { get; set; } = "İşlem başarılıdır.";
        public object Data { get; set; }
    }
}
