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
            //APR_COM_HELPER_K.GET_REFDATA_USER_SID_F
            //var context = new HypothesisEntities();
            //return context.APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_CODES_P().Select(c => c.PROJECT_CODE).ToList();

            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
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
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
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
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
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
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
            //List<ProjectSubstanceModel> projectSubstances = null;
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
            connection.Close();
            return projectCategoriesList;
        }
        public List<string> GetCategorySubstanceCSN(string categoryid)
        {
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
            //List<ProjectSubstanceModel> projectSubstances = null;
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
            connection.Close();
            return substanceCsnList;
        }

        public List<ProjectMember> GetProjectMembers(string code)
        {
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
            //List<ProjectSubstanceModel> projectSubstances = null;
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
            connection.Close();
            return ProjectMemberList;
        }
        public List<SubstanceSample> GetSubstanceSamples(string substancecsn)
        {
            var connection = new OracleConnection();
            connection.ConnectionString = Constants.ConnectionString;
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
            double Sub_vesselWeight;
            double Sub_vesselConcentration;
            double Sub_vesselVolume;


            double? hamda = double.TryParse(myReader[4].ToString(), out Sub_vesselWeight) ? double.Parse(myReader[4].ToString()) : null;
            while (myReader.Read())
            {
                substanceSamplesList.Add(new SubstanceSample()
                {
                    ISN = myReader[0].ToString(),
                    IVN = myReader[1].ToString(),
                    VesselStatusLabel = myReader[2].ToString(),
                    Sub_vesselNumber = myReader[3].ToString(),
                    Sub_vesselWeight = double.TryParse(myReader[4].ToString(), out Sub_vesselWeight) == true ?? double.Parse(myReader[4].ToString()),
                    Sub_vesselConcentration = double.TryParse(myReader[5].ToString(), out Sub_vesselWeight) ? double.Parse(myReader[4].ToString()),
                    Sub_vesselVolume = double.TryParse(myReader[6].ToString(), out Sub_vesselWeight) ? double.Parse(myReader[4].ToString())
                });
            }
            connection.Close();
            return substanceSamplesList;
        }
        public void CreateProjectCategory(CreateProjectParams createProjectParams)
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

            connection.Open();
            cmd.ExecuteNonQuery();
            //var myReader = ((OracleRefCursor)resultOracleParameter.Value).GetDataReader();
            //var projectCategory = "";
            //if (myReader.Read())
            //{
            //    projectCategory = myReader[0].ToString();
            //}
            //connection.Close();
            //return projectCategory;
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
            OracleParameter resultOracleParameter = cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void CreateProjectSubstance(CreateProjectSubstanceParams createProjectSubstanceParams)
        {
            //SCO_APR.APR_PROJECT_SUBS_K.insert_PROJECT_SUBS_p
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJECT_SUBS_K.insert_PROJECT_SUBS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
            projectCodeOracleParameter.Value = createProjectSubstanceParams.ProjectCode;
            OracleParameter substanceCSNOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
            substanceCSNOracleParameter.Value = createProjectSubstanceParams.SubstanceCSN;
            OracleParameter stillOfInterestOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
            stillOfInterestOracleParameter.Value = createProjectSubstanceParams.StillOfInterest;
            OracleParameter resultOracleParameter = cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void UpdateProjectSubstance(UpdateProjectSubstanceParams updateProjectSubstanceParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "SCO_APR.APR_PROJECT_SUBS_K.update_PROJECT_SUBS_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input);
            projectCodeOracleParameter.Value = updateProjectSubstanceParams.ProjectCode;
            OracleParameter substanceCSNOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
            substanceCSNOracleParameter.Value = updateProjectSubstanceParams.SubstanceCSN;
            OracleParameter stillOfInterestOracleParameter = cmd.Parameters.Add("p_RD_SUBS_PC", OracleDbType.Varchar2, ParameterDirection.Input);
            stillOfInterestOracleParameter.Value = updateProjectSubstanceParams.StillOfInterest;
            OracleParameter resultOracleParameter = cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input);

            connection.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
