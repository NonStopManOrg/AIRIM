using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    [DataContract]
    public class CreateCategorySubstanceParams
    {
        public CreateCategorySubstanceParams(string categoryid, string substancecsn)
        {
            Categoryid = categoryid;
            Substancecsn = substancecsn;
        }
        [DataMember]
        public string Categoryid { get; private set; }
        [DataMember]
        public string Substancecsn { get; private set; }
    }
}
