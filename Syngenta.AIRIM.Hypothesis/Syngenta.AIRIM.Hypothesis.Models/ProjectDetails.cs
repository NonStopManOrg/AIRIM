using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    public partial class ProjectDetail
    {
        public string PROJECT_NAME { get; set; }
        public string PROJECT_STATUS { get; set; }
        public string INITIATING_PROJECTCODE { get; set; }
        public string PROJECT_TYPE_LABEL { get; set; }
        public string PROJECT_INDICATIONS_CODE { get; set; }
    }
}
