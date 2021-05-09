namespace BharatSetu.Services
{
    public static class Constants
    {
        public const string StageApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public const string TestApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public const string ProdApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public static string BaseUrl = TestApiUrl;

        public const string GenerateOTP = "auth/public/generateOTP";
        public const string ConfirmOTP = "auth/public/confirmOTP";

        public const string States = "admin/location/states/";
        public const string Districts = "admin/location/districts/{0}";

        public const string GetCertificateByBeneficiaryReferenceId = "registration/certificate/public/download{0}";

        public const string FindByPin = "appointment/sessions/public/findByPin?pincode={0}&date={1}";
        public const string FindByDistrict = "appointment/sessions/public/findByDistrict/?district_id={0}&date={1}";
        public const string CalanderByPin = "appointment/sessions/public/calendarByPin?pincode={0}&date={1}";
        public const string CalendarByDistrict = "appointment/sessions/public/calendarByDistrict?district_id={0}&date={1}";
    }
}
