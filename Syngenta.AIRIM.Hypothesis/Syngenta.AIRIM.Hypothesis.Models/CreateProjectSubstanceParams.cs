using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    [DataContract]
    public class CreateProjectSubstanceParams
    {
        public CreateProjectSubstanceParams(string projectcode, string substancecsn, bool stillofinterest)
        {
            ProjectCode = projectcode;
            SubstanceCSN = substancecsn;
            StillOfInterest = stillofinterest;
        }
        [DataMember]
        public string ProjectCode { get; private set; }
        [DataMember]
        public string SubstanceCSN { get; private set; }
        [DataMember]
        public bool StillOfInterest { get; private set; }
    }
}
