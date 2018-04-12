using System;
using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    [DataContract]
    public class CreateProjectSubstanceParams
    {
        public CreateProjectSubstanceParams(string projectCode, string rdSubsPc, string stillOfInterest,
            string lead, string favourite, string sTD, string fldCandidate, string rdFormulationSid)
        {
            ProjectCode = projectCode ?? throw new ArgumentNullException(nameof(projectCode));
            SubstanceCsn = rdSubsPc ?? throw new ArgumentNullException(nameof(rdSubsPc));
            StillOfInterest = stillOfInterest;
            Lead = lead;
            Favourite = favourite;
            STD = sTD;
            FldCandidate = fldCandidate;
            RdFormulationSid = rdFormulationSid;
        }

        [DataMember]
        public string ProjectCode { get; private set; }
        [DataMember]
        public string SubstanceCsn { get; private set; }
        [DataMember]
        public string StillOfInterest { get; private set; }
        [DataMember]
        public string Lead { get; private set; }
        [DataMember]
        public string Favourite { get; private set; }
        [DataMember]
        public string STD { get; private set; }
        [DataMember]
        public string RdFormulationSid { get; private set; }
        [DataMember]
        public string FldCandidate { get; private set; }
    }
}
