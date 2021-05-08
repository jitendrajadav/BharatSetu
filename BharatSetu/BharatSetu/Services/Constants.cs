namespace BharatSetu.Services
{
    public static class Constants
    {
        public const string StageApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public const string TestApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public const string ProdApiUrl = "https://cdn-api.co-vin.in/api/v2/";
        public static string BaseUrl = TestApiUrl;

        public const string PostAuthentication = "auth/public/generateOTP";
        public const string PostConfirmAuthentication = "auth/public/confirmOTP";

        public const string GetStatesIndia = "admin/location/states/";
        public const string GetDistrictsByStatesId = "admin/location/districts/{0}";

        public const string GetCertificateByBeneficiaryReferenceId = "registration/certificate/public/download{0}";

        public const string GetPlannedVaccinationSessions = "appointment/sessions/public/findByPin?pincode={0}&date={1}";
        public const string GetPlannedVaccinationSessionsByDistrict = "appointment/sessions/public/findByDistrict/?{0}&{1}";
        public const string GetPlannedVaccinationSessionsFor7Days = "appointment/sessions/public/calendarByPin?{0}&{1}";
        public const string GetPlannedVaccinationSessionsFor7DaysDistricts = "appointment/sessions/public/calendarByDistrict?{0}&{1}";
    }
}
