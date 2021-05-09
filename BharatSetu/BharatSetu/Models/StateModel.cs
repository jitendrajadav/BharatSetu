using System.Collections.Generic;

namespace BharatSetu.Models
{
    public class AuthConfirm
    {
        public string token { get; set; }
    }
    public class ConfirmAuthentication
    {
        public string otp { get; set; }
        public string txnId { get; set; }
    }

    public class Mobile
    {
        public string mobile { get; set; }
    }

    public class Authenticate
    {
        public string txnId { get; set; }
    }

    public class State
    {
        public int State_id { get; set; }
        public string State_name { get; set; }
    }

    public class StateModel 
    {
        public List<State> States { get; set; }
        public int Ttl { get; set; }
    }

    public class Districts
    {
        public int State_id { get; set; }
        public int District_id { get; set; }
        public string District_name { get; set; }
        public string District_name_l { get; set; }
    }

    public class DistrictsModel
    {
        public List<Districts> Districts { get; set; }
        public int Ttl { get; set; }
    }

    public class Session
    {
        public int Center_id { get; set; }
        public string Name { get; set; }
        public string Name_l { get; set; }
        public string Address { get; set; }
        public string Address_l { get; set; }
        public string State_name { get; set; }
        public string State_name_l { get; set; }
        public string District_name { get; set; }
        public string District_name_l { get; set; }
        public string Block_name { get; set; }
        public string Block_name_l { get; set; }
        public string Pincode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Fee_type { get; set; }
        public string Fee { get; set; }
        public string Session_id { get; set; }
        public string Date { get; set; }
        public int Available_capacity { get; set; }
        public int Min_age_limit { get; set; }
        public string Vaccine { get; set; }
        public List<string> Slots { get; set; }
    }

    public class VaccinationSessions
    {
        public List<Session> Sessions { get; set; }
    }

}
