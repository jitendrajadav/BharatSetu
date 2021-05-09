﻿using System.Collections.Generic;

namespace BharatSetu.Models
{
   public class CalenderByPin
    {
        public List<Center> Centers { get; set; }
    }

    public class VaccineFee
    {
        public string Vaccine { get; set; }
        public string Fee { get; set; }
    }

    public class CalenderByPinSession
    {
        public string Session_id { get; set; }
        public string Date { get; set; }
        public int Available_capacity { get; set; }
        public int Min_age_limit { get; set; }
        public string Vaccine { get; set; }
        public List<string> Slots { get; set; }
    }

    public class Center
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
        public List<VaccineFee> Vaccine_fees { get; set; }
        public List<CalenderByPinSession> Sessions { get; set; }
    }
}
