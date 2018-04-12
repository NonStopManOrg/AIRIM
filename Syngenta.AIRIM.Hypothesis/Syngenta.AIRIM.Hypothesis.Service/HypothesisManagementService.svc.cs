using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using Syngenta.AIRIM.Hypothesis.Models;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HypothesisManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HypothesisManagementService.svc or HypothesisManagementService.svc.cs at the Solution Explorer and start debugging.
    public class HypothesisManagementService : IHypothesisManagementService
    {
        public List<string> GetProjectCodes()
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            List<string> projectsCodes = new List<string>();
            string commText = "APR_PROJECT_HYPOTHESIS_K.GET_PROJECT_CODES_P";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectsCodesOracleParameter = cmd.Parameters.Add("prc", OracleDbType.RefCursor, ParameterDirection.Output);

            connection.Open();

            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectsCodesOracleParameter.Value).GetDataReader();
            while (myReader.Read())
            {
                projectsCodes.Add(myReader[0].ToString());
            }
            connection.Close();
            return projectsCodes;
        }
        public ProjectDetail GetProjectDetails(string code)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            ProjectDetail projectDetails = null;
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_DETAILS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectDetailsOracleParameter = cmd.Parameters.Add("PROJECT_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("CODE", OracleDbType.Varchar2, ParameterDirection.Input);
            projectCodeOracleParameter.Value = code;
            connection.Open();
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectDetailsOracleParameter.Value).GetDataReader();
            while (myReader.Read())
            {
                projectDetails = new ProjectDetail()
                {
                    PROJECT_NAME = myReader[0].ToString(),
                    PROJECT_STATUS = myReader[1].ToString(),
                    PROJECT_INDICATIONS_CODE = myReader[2].ToString(),
                    INITIATING_PROJECTCODE = myReader[3].ToString(),
                    PROJECT_TYPE_LABEL = myReader[4].ToString()
                };
            }
            connection.Close();
            return projectDetails;
        }
        public List<ProjectSubstanceModel> GetProjectSubstance(string code)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            List<ProjectSubstanceModel> projectSubstances = null;
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_SUBSTANCECNS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            OracleParameter projectSubstanceOracleParameter = cmd.Parameters.Add("PROJECT_SUBSTANCE", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("CODE", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            projectCodeOracleParameter.Value = code;
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectSubstanceOracleParameter.Value).GetDataReader();
            projectSubstances = new List<ProjectSubstanceModel>();
            while (myReader.Read())
            {
                projectSubstances.Add(new ProjectSubstanceModel()
                {
                    SubstanceCSN = myReader[0].ToString(),
                    STILL_OF_INTEREST = myReader[1].ToString() == "Y" ? "True" : "False"
                });
            }
            connection.Close();
            return projectSubstances;
        }
        public List<string> GetProjectCategories(string code)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_CATEGORIES_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCategoriesOracleParameter = cmd.Parameters.Add("PROJECT_CATEGORIES", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("CODE", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            projectCodeOracleParameter.Value = code;
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectCategoriesOracleParameter.Value).GetDataReader();
            var projectCategoriesList = new List<string>();
            while (myReader.Read())
            {
                projectCategoriesList.Add(myReader[0].ToString());
            }
            connection.Dispose();
            return projectCategoriesList;
        }
        public List<string> GetCategorySubstanceCSN(string categoryid)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_SUBSTANCECNS_CSN_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCategoriesOracleParameter = cmd.Parameters.Add("SUBSTANCES_CSN", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("CATEGORYID", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            projectCodeOracleParameter.Value = categoryid;
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectCategoriesOracleParameter.Value).GetDataReader();
            var substanceCsnList = new List<string>();
            while (myReader.Read())
            {
                substanceCsnList.Add(myReader[0].ToString());
            }
            connection.Dispose();
            return substanceCsnList;
        }
        public List<ProjectMember> GetProjectMembers(string code)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_MEMBERS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCategoriesOracleParameter = cmd.Parameters.Add("PROJECT_MEMBERS", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("CODE", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            projectCodeOracleParameter.Value = code;
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectCategoriesOracleParameter.Value).GetDataReader();
            var ProjectMemberList = new List<ProjectMember>();
            while (myReader.Read())
            {
                ProjectMemberList.Add(new ProjectMember()
                {
                    UserName = myReader[0].ToString(),
                    Role = myReader[1].ToString()
                });
            }
            connection.Dispose();
            return ProjectMemberList;
        }
        public List<SubstanceSample> GetSubstanceSamples(string substancecsn)
        {
            var connection = new OracleConnection {ConnectionString = Constants.ConnectionString};
            //List<ProjectSubstanceModel> projectSubstances = null;
            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_SUBSTANCECN_SAMPLES_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCategoriesOracleParameter = cmd.Parameters.Add("SUBSTANCE_SAMPLES", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter substanceCSNOracleParameter = cmd.Parameters.Add("SUBSTANCECSN", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            substanceCSNOracleParameter.Value = substancecsn;
            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)projectCategoriesOracleParameter.Value).GetDataReader();
            var substanceSamplesList = new List<SubstanceSample>();
            double? subVesselWeight = null;
            double? subVesselConcentration = null;
            double? subVesselVolume = null;
            while (myReader.Read())
            {
                if (!string.IsNullOrEmpty(myReader[4].ToString()))
                    subVesselWeight = double.Parse(myReader[4].ToString());
                if (!string.IsNullOrEmpty(myReader[5].ToString()))
                    subVesselConcentration = double.Parse(myReader[5].ToString());
                if (!string.IsNullOrEmpty(myReader[6].ToString()))
                    subVesselVolume = double.Parse(myReader[6].ToString());

                substanceSamplesList.Add(new SubstanceSample()
                {
                    ISN = myReader[0].ToString(),
                    IVN = myReader[1].ToString(),
                    VesselStatusLabel = myReader[2].ToString(),
                    Sub_vesselNumber = myReader[3].ToString(),
                    Sub_vesselWeight = subVesselWeight,
                    Sub_vesselConcentration = subVesselConcentration,
                    Sub_vesselVolume = subVesselVolume
                });
            }
            connection.Dispose();
            return substanceSamplesList;
        }
        public string CreateProjectCategory(CreateProjectParams createProjectParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJECT_SUBS_CATEGORY_K.INSERT_PROJECT_SUBS_CATEGORY_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            OracleParameter subCategoryIdOracleParameter = cmd.Parameters.Add("p_SUBS_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.Input);
            subCategoryIdOracleParameter.Value = createProjectParams.Categoryid;
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
            projectCodeOracleParameter.Value = createProjectParams.Projectcode;
            OracleParameter subCategoryNameOracleParameter = cmd.Parameters.Add("p_SUBS_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input);
            subCategoryNameOracleParameter.Value = createProjectParams.Categoryname;
            OracleParameter resultOracleParameter = cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);
            var myReader = ((OracleRefCursor)resultOracleParameter.Value).GetDataReader();

            connection.Open();
            cmd.ExecuteNonQuery();
            if (!myReader.Read()) return null;
            connection.Dispose();
            return myReader[0].ToString();
        }
        public void CreateCategorySubstance(CreateCategorySubstanceParams createCategorySubstanceParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJ_SUBS_CATEGORY_SUBS_K.insert_proj_subs_cat_subs_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter categoryIdOracleParameter = cmd.Parameters.Add("p_subs_category_id", OracleDbType.Varchar2, ParameterDirection.Input);
            categoryIdOracleParameter.Value = createCategorySubstanceParams.Categoryid;
            OracleParameter substanceIdOracleParameter = cmd.Parameters.Add("p_subs_id", OracleDbType.Varchar2, ParameterDirection.Input);
            substanceIdOracleParameter.Value = createCategorySubstanceParams.Substancecsn;
             cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Dispose();
        }
        public void CreateProjectSubstance(CreateProjectSubstanceParams createProjectSubstanceParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJECT_SUBS_K.insert_PROJECT_SUBS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
            projectCodeOracleParameter.Value = createProjectSubstanceParams.ProjectCode;
            OracleParameter rdSubsOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
            rdSubsOracleParameter.Value = createProjectSubstanceParams.SubstanceCsn;
            OracleParameter stillOfInterestOracleParameter = cmd.Parameters.Add("p_STILL_OF_INTEREST", OracleDbType.Varchar2, ParameterDirection.Input);
            stillOfInterestOracleParameter.Value = createProjectSubstanceParams.StillOfInterest;
            OracleParameter leadOracleParameter = cmd.Parameters.Add("p_LEAD", OracleDbType.Varchar2, ParameterDirection.Input);
            leadOracleParameter.Value = createProjectSubstanceParams.StillOfInterest;
            OracleParameter favouriteOracleParameter = cmd.Parameters.Add("p_FAVOURITE", OracleDbType.Varchar2, ParameterDirection.Input);
            favouriteOracleParameter.Value = createProjectSubstanceParams.Favourite;
            OracleParameter stdOracleParameter = cmd.Parameters.Add("p_STD", OracleDbType.Varchar2, ParameterDirection.Input);
            stdOracleParameter.Value = createProjectSubstanceParams.STD;
            OracleParameter rdFormulationSidOracleParameter = cmd.Parameters.Add("p_RD_FORMULATION_SID", OracleDbType.Varchar2, ParameterDirection.Input);
            rdFormulationSidOracleParameter.Value = createProjectSubstanceParams.RdFormulationSid;
            OracleParameter vFldCandidateOracleParameter = cmd.Parameters.Add("p_FLD_CANDIDATE", OracleDbType.Varchar2, ParameterDirection.Input);
            vFldCandidateOracleParameter.Value = createProjectSubstanceParams.FldCandidate;
             cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Dispose();
        }
        public void UpdateProjectSubstance(UpdateProjectSubstanceParams updateProjectSubstanceParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "SCO_APR.APR_PROJECT_SUBS_K.update_PROJECT_SUBS_p";
            OracleCommand query = new OracleCommand
            {
                Connection = connection,
                CommandText =
                    "SELECT CHANGE_SEQ_NUM, LEAD, FAVOURITE, STD, RD_FORMULATION_SID, FLD_CANDIDATE, CURRENT_OWNING_INSTANCE_CODE FROM APR_PROJECT_SUBS WHERE" +
                    $" (PROJECT_CODE = '{updateProjectSubstanceParams.ProjectCode}') AND (RD_SUBS_PC = '{updateProjectSubstanceParams.RdSubsPc}')",
                CommandType = CommandType.Text
            };
            connection.Open();
            OracleDataReader dr = query.ExecuteReader();
            if (dr.Read())
            {
                updateProjectSubstanceParams.ChangeSeqNum = dr["CHANGE_SEQ_NUM"].ToString();
                updateProjectSubstanceParams.Lead = dr["LEAD"].ToString();
                updateProjectSubstanceParams.Favourite = dr["FAVOURITE"].ToString();
                updateProjectSubstanceParams.STD = dr["STD"].ToString();
                updateProjectSubstanceParams.RdFormulationSid = dr["RD_FORMULATION_SID"].ToString();
                updateProjectSubstanceParams.FldCandidate = dr["FLD_CANDIDATE"].ToString();
                updateProjectSubstanceParams.CurrentOwningInstance = dr["CURRENT_OWNING_INSTANCE_CODE"].ToString();
            }
            else
            {
                connection.Dispose();
                throw new Exception("there is no substance with this data");
            }

            using (OracleCommand cmd = new OracleCommand(commText, connection) { CommandType = CommandType.StoredProcedure })
            {
                OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
                projectCodeOracleParameter.Value = updateProjectSubstanceParams.ProjectCode;
                OracleParameter rdSubsOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
                rdSubsOracleParameter.Value = updateProjectSubstanceParams.RdSubsPc;
                OracleParameter stillOfInterestOracleParameter = cmd.Parameters.Add("p_STILL_OF_INTEREST", OracleDbType.Varchar2, ParameterDirection.Input);
                stillOfInterestOracleParameter.Value = updateProjectSubstanceParams.StillOfInterest;
                OracleParameter leadOracleParameter = cmd.Parameters.Add("p_LEAD", OracleDbType.Varchar2, ParameterDirection.Input);
                leadOracleParameter.Value = updateProjectSubstanceParams.Lead;
                OracleParameter favouriteOracleParameter = cmd.Parameters.Add("p_FAVOURITE", OracleDbType.Varchar2, ParameterDirection.Input);
                favouriteOracleParameter.Value = updateProjectSubstanceParams.Favourite;
                OracleParameter stdOracleParameter = cmd.Parameters.Add("p_STD", OracleDbType.Varchar2, ParameterDirection.Input);
                stdOracleParameter.Value = updateProjectSubstanceParams.STD;
                OracleParameter rdFormulationSidOracleParameter = cmd.Parameters.Add("p_RD_FORMULATION_SID", OracleDbType.Varchar2, ParameterDirection.Input);
                rdFormulationSidOracleParameter.Value = updateProjectSubstanceParams.RdFormulationSid;
                OracleParameter pChangeSeqNumOracleParameter = cmd.Parameters.Add("p_CHANGE_SEQ_NUM", OracleDbType.Varchar2, ParameterDirection.Input);
                pChangeSeqNumOracleParameter.Value = updateProjectSubstanceParams.ChangeSeqNum;
                OracleParameter pCurrentOwningInstanceCodeOracleParameter = cmd.Parameters.Add("p_CURRENT_OWNING_INSTANCE_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
                pCurrentOwningInstanceCodeOracleParameter.Value = updateProjectSubstanceParams.CurrentOwningInstance;
                OracleParameter vFldCandidateOracleParameter = cmd.Parameters.Add("p_FLD_CANDIDATE", OracleDbType.Varchar2, ParameterDirection.Input);
                vFldCandidateOracleParameter.Value = updateProjectSubstanceParams.FldCandidate;
                cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.ExecuteNonQuery();
                connection.Dispose();
            }
        }
    }
}
