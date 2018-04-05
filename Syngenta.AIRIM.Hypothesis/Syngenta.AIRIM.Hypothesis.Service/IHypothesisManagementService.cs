using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Syngenta.AIRIM.Hypothesis.Models;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHypothesisManagementService" in both code and config file together.


    [ServiceContract]
    public interface IHypothesisManagementService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectCodes", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<string> GetProjectCodes();

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectDetails/{code}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        ProjectDetail GetProjectDetails(string code);

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectSubstance/{code}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<ProjectSubstanceModel> GetProjectSubstance(string code);


        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectMembers/{code}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<ProjectMember> GetProjectMembers(string code);


        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectCategories/{code}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<string> GetProjectCategories(string code);

        [OperationContract]
        [WebGet(UriTemplate = "/GetCategorySubstanceCSN/{categoryid}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<string> GetCategorySubstanceCSN(string categoryid);

        [OperationContract]
        [WebGet(UriTemplate = "/GetSubstanceSamples/{substancecsn}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        List<SubstanceSample> GetSubstanceSamples(string substancecsn);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateProjectCategory", Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        void CreateProjectCategory(CreateProjectParams createProjectParams);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateCategorySubstance", Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        void CreateCategorySubstance(CreateCategorySubstanceParams createCategorySubstanceParams);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateProjectSubstance", Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        void CreateProjectSubstance(CreateProjectSubstanceParams createProjectSubstanceParams);
        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateProjectSubstance", Method = "POST", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        void UpdateProjectSubstance(UpdateProjectSubstanceParams createProjectSubstanceParams);
    }
}
