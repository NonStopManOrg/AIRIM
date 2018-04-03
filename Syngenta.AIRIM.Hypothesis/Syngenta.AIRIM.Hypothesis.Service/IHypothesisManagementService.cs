using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Syngenta.AIRIM.Hypothesis.Data;
using Syngenta.AIRIM.Hypothesis.Models;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHypothesisManagementService" in both code and config file together.
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
    [DataContract]
    public class CreateProjectSubstanceParams
    {
        public CreateProjectSubstanceParams(string projectcode, string substancecsn, bool stillofinterest)
        {
            Projectcode = projectcode;
            Substancecsn = substancecsn;
            Stillofinterest = stillofinterest;
        }

        [DataMember]
        public string Projectcode { get; private set; }
        [DataMember]
        public string Substancecsn { get; private set; }
        [DataMember]
        public bool Stillofinterest { get; private set; }
    }
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

    [ServiceContract]
    public interface IHypothesisManagementService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectCodes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetProjectCodes();

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectDetails/{code}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        ProjectDetail GetProjectDetails(string code);

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectSubstance/{code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<ProjectSubstanceModel> GetProjectSubstance(string code);


        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectMembers/{code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ProjectDetail GetProjectMembers(string code);


        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectCategories/{code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetProjectCategories(string code);

        [OperationContract]
        [WebGet(UriTemplate = "/GetCategorySubstanceCSN/{categoryid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetCategorySubstanceCSN(string categoryid);


        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateProjectCategory", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateProjectCategory(CreateProjectParams createProjectParams);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateCategorySubstance", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void CreateCategorySubstance(CreateCategorySubstanceParams createCategorySubstanceParams);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateProjectSubstance", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateProjectSubstance(CreateProjectSubstanceParams createProjectSubstanceParams);
    }
}
