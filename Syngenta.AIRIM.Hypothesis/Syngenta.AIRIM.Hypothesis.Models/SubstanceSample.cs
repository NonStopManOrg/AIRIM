using System.Runtime.Serialization;

namespace Syngenta.AIRIM.Hypothesis.Models
{
    public class SubstanceSample
    {
        public string ISN { get; set; }
        public string IVN { get; set; }
        public string VesselStatusLabel { get; set; }
        public string Sub_vesselNumber { get; set; }
        public double? Sub_vesselWeight { get; set; }
        public double? Sub_vesselVolume { get; set; }
        public double? Sub_vesselConcentration { get; set; }
    }
}