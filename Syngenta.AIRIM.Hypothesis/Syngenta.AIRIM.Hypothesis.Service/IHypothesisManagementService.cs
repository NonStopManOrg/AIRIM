using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Syngenta.AIRIM.Hypothesis.Data;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHypothesisManagementService" in both code and config file together.
    [ServiceContract]
    public interface IHypothesisManagementService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectCodes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetProjectCodes();

        [OperationContract]
        [WebGet(UriTemplate = "/GetProjectDetails/{code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GET_PROJECT_DETAILS_RESULT GetProjectDetails(string code);

        //[OperationContract]
        //CompositeType GetProjectDetails(string projectCode);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
