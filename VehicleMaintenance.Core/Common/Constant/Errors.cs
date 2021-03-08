using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Core.Common.Constant
{
    public static class Errors
    {
        public static readonly object SystemError = new { isSuccess = false, errorCode = "C000", message = "Sistem hatası oluştu. Sistem yöneticisine bildiriniz." };
        public static readonly object ActionTypeNameRequired = new { isSuccess = false, errorCode = "C001", message = "Aksiyon Tip adı boş geçilemez." };
        public static readonly object VehicleTypeNameRequired = new { isSuccess = false, errorCode = "C002", message = "Araç Tip adı boş geçilemez." };
        public static readonly object StatusNameRequired = new { isSuccess = false, errorCode = "C003", message = "Durum alanı boş geçilemez." };
        public static readonly object PictureImageRequired = new { isSuccess = false, errorCode = "C004", message = "Resim alanı boş geçilemez." };


        public static readonly object VehicleNameRequired = new { isSuccess = false, errorCode = "C005", message = "Araç adı zorunludur." };
        public static readonly object PlateNoRequired = new { isSuccess = false, errorCode = "C006", message = "Plaka kodu zorunludur." };
        public static readonly object UserIDRequired = new { isSuccess = false, errorCode = "C007", message = "Kullanıcı ID zorunludur." };
        public static readonly object VehicleTypeID = new { isSuccess = false, errorCode = "C008", message = "Araç tipi ID alanı zorunludur." };
        public static readonly object MaintenanceDescriptionRequired = new { isSuccess = false, errorCode = "C009", message = "Bakım ile ilgili açıklama zorunludur." };
        public static readonly object LocationLatitudeRequired = new { isSuccess = false, errorCode = "C010", message = "Enlem zorunludur." };
        public static readonly object LocationLongitudeRequired = new { isSuccess = false, errorCode = "C011", message = "Boylam zorunludur." };
        public static readonly object ExpectedTimeToFix = new { isSuccess = false, errorCode = "C012", message = "Düzeltmek için beklenen süre girilmelidir." };
        public static readonly object VehicleIDRequired = new { isSuccess = false, errorCode = "C013", message = "Araç Id'si zorunludur." };
        public static readonly object ActionTypeIDRequired = new { isSuccess = false, errorCode = "C014", message = "Aksiyon tipi Id alanı zorunludur." };
        public static readonly object ResponsibleUserID = new { isSuccess = false, errorCode = "C015", message = "Sorumlu kişinin Id'si zorunludur." };
        public static readonly object StatusID = new { isSuccess = false, errorCode = "C016", message = "Durum Id'si zorunludur." };
        public static readonly object PictureGroupID = new { isSuccess = false, errorCode = "C017", message = "Resim Id alanı zorunludur." };
        public static readonly object TextRequired = new { isSuccess = false, errorCode = "C018", message = "Metin alanı boş bırakılamaz." };
        public static readonly object MaintenanceIDRequired = new { isSuccess = false, errorCode = "C020", message = "Bakım Id'si zorunludur." };
    }
}
