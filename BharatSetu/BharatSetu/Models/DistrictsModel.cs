using System.Collections.Generic;

namespace BharatSetu.Models
{
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
}
