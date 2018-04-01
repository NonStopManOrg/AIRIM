using System;
using System.Collections.Generic;
using System.Linq;
using Syngenta.AIRIM.Hypothesis.Data;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HypothesisManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HypothesisManagementService.svc or HypothesisManagementService.svc.cs at the Solution Explorer and start debugging.
    public class HypothesisManagementService : IHypothesisManagementService
    {
        public List<string> GetProjectCodes()
        {
            var context = new HypothesisEntities();
            return context.APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_CODES_P().Select(c => c.PROJECT_CODE).ToList();
        }

        public APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_DETAILS_P_Result GetProjectDetails(string code)
        {
            var context = new HypothesisEntities();
            var x = context.APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_DETAILS_P(code);//.FirstOrDefault();
            return new APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_DETAILS_P_Result();
        }
    }
}
