using System;
using System.IO;

namespace BharatSetu.Services
{
    public static class Constants
    {
        public const string DatabaseFilename = "BharatSetuSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

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
        
        public const string Download = "registration/certificate/public/download?beneficiary_reference_id={0}";
    }
}
