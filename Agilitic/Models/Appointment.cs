using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Agilitic.Models
{
    [DataContract]
    internal class Appointment : IComparable<Appointment>
    {
        [DataMember]
        [JsonProperty("appointment_start_date_time")]
        internal DateTime StartTime;

        [DataMember]
        [JsonProperty("appointment_end_date_time")]
        internal DateTime EndTime;

        [DataMember]
        [JsonProperty("appointment_id")]
        internal string ID;

        public int CompareTo(Appointment apt)
        {
            return this.StartTime.CompareTo(apt.StartTime);
        }
    }
}
