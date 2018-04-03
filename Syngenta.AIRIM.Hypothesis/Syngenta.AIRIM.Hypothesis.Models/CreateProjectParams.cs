using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    [DataContract]
    public class CreateProjectParams
    {
        public CreateProjectParams(string projectcode, string categoryname, string categoryid)
        {
            Projectcode = projectcode;
            Categoryname = categoryname;
            Categoryid = categoryid;
        }
        [DataMember]
        public string Projectcode { get; set; }
        [DataMember]
        public string Categoryname { get; set; }
        [DataMember]
        public string Categoryid { get; set; }
    }
}
