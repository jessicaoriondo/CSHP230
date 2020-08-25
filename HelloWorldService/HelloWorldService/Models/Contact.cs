using Newtonsoft.Json;
using System;


namespace HelloWorldService.Models
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class Contact
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }
        public Phone[] Phones { get; set; }

        //[JsonIgnore] //can get value but wont get the value out in the JSON output
        //public string Password { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Number { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("phone_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Nil = 0,
        Home = 1,
        Mobile = 2,
    }
}