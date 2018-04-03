using System;
using System.Collections.Generic;
using System.Linq;
using Syngenta.AIRIM.Hypothesis.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;
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
            var context = new HypothesisEntities();
            return context.APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_CODES_P().Select(c => c.PROJECT_CODE).ToList();
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
            //Get_PROJECT_SUBSTANCECNS_CSN_p
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

        public void CreateCategorySubstance(CreateCategorySubstanceParams createCategorySubstanceParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJECT_HYPOTHESIS_K.insert_PROJECT_SUBS_CATEGORY_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            OracleParameter categoryIdOracleParameter = cmd.Parameters.Add("p_subs_category_id", OracleDbType.Varchar2, ParameterDirection.Input);
            categoryIdOracleParameter.Value = createCategorySubstanceParams.Categoryid;
            OracleParameter substanceIdOracleParameter = cmd.Parameters.Add("p_subs_id", OracleDbType.Varchar2, ParameterDirection.Input);
            substanceIdOracleParameter.Value = createCategorySubstanceParams.Substancecsn;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public string CreateProjectCategory(CreateProjectParams createProjectParams)
        {
            var connection = new OracleConnection { ConnectionString = Constants.ConnectionString };
            var commText = "APR_PROJECT_HYPOTHESIS_K.insert_PROJECT_SUBS_CATEGORY_p";
            OracleCommand cmd = new OracleCommand(commText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            OracleParameter subCategoryNameOracleParameter = cmd.Parameters.Add("p_SUBS_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input);
            subCategoryNameOracleParameter.Value = createProjectParams.Categoryname;
            OracleParameter subCategoryIdOracleParameter = cmd.Parameters.Add("p_SUBS_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.Input);
            subCategoryIdOracleParameter.Value = createProjectParams.Categoryid;
            OracleParameter projectCodeOracleParameter = cmd.Parameters.Add("p_PROJECT_CODE", OracleDbType.Varchar2, ParameterDirection.Input); connection.Open();
            projectCodeOracleParameter.Value = createProjectParams.Projectcode;
            OracleParameter resultOracleParameter = cmd.Parameters.Add("p_results", OracleDbType.RefCursor, ParameterDirection.Input); connection.Open();

            projectCodeOracleParameter.Value = createProjectParams.Categoryid;

            cmd.ExecuteNonQuery();
            var myReader = ((OracleRefCursor)resultOracleParameter.Value).GetDataReader();
            var projectCategory = "";   
            if (myReader.Read())
            {
                projectCategory = myReader[0].ToString();
            }
            connection.Close();
            return projectCategory;
        }

        public List<string> GetSubstanceSamples(string code)
        {
            throw new NotImplementedException();
        }
        public ProjectDetail GetProjectMembers(string code)
        {
            throw new NotImplementedException();
        }
        public string CreateProjectSubstance(CreateProjectSubstanceParams createProjectSubstanceParams)
        {
            throw new NotImplementedException();
        }
    }
}
