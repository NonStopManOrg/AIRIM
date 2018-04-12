using System;
using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    [DataContract]
    public class UpdateProjectSubstanceParams
    {
        public UpdateProjectSubstanceParams(string projectCode, string rdSubsPc, string stillOfInterest, string lead,
            string favourite, string std, string rdFormulationSid, string changeSeqNum, string currentOwningInstance, string fldCandidate)
        {
            ProjectCode = projectCode ?? throw new ArgumentNullException(nameof(projectCode));
            RdSubsPc = rdSubsPc ?? throw new ArgumentNullException(nameof(rdSubsPc));
            StillOfInterest = stillOfInterest;
            Lead = lead;
            Favourite = favourite;
            STD = std;
            RdFormulationSid = rdFormulationSid;
            ChangeSeqNum = changeSeqNum;
            CurrentOwningInstance = currentOwningInstance;
            FldCandidate = fldCandidate;
        }

        [DataMember]
        public string ProjectCode { get; private set; }
        [DataMember]
        public string RdSubsPc { get; private set; }
        [DataMember]
        public string StillOfInterest { get; private set; }
        [DataMember]
        public string Lead { get; set; }
        [DataMember]
        public string Favourite { get; set; }
        [DataMember]
        public string STD { get; set; }
        [DataMember]
        public string RdFormulationSid { get; set; }
        [DataMember]
        public string ChangeSeqNum { get; set; }
        [DataMember]
        public string CurrentOwningInstance { get;  set; }
        [DataMember]
        public string FldCandidate { get; set; }
    }
}
