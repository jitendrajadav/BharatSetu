using System.Collections.Generic;

namespace BharatSetu.Models
{
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
}
