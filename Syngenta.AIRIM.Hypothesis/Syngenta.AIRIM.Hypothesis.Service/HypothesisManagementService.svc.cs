using System;
using System.Collections.Generic;
using System.Linq;
using Syngenta.AIRIM.Hypothesis.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Types;

namespace Syngenta.AIRIM.Hypothesis.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HypothesisManagementService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HypothesisManagementService.svc or HypothesisManagementService.svc.cs at the Solution Explorer and start debugging.
    public class HypothesisManagementService : IHypothesisManagementService
    {
        const string ConnectionString = "Data Source = (DESCRIPTION = "
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=frialora15.eame.syngenta.org)(PORT=1528)))"
             + "(CONNECT_DATA=(SID = AIRR4)(SERVER = DEDICATED)));"
             + "User Id=SCO_APR;Password=SCO_APR;";
        public List<string> GetProjectCodes()
        {
            var context = new HypothesisEntities();
            return context.APR_PROJECT_HYPOTHESIS_K_GET_PROJECT_CODES_P().Select(c => c.PROJECT_CODE).ToList();
        }

        public GET_PROJECT_DETAILS_RESULT GetProjectDetails(string code)
        {
            var connection = new OracleConnection();
            connection.ConnectionString = ConnectionString;

            string commText = "APR_PROJECT_HYPOTHESIS_K.Get_PROJECT_DETAILS_p";
            OracleCommand cmd = new OracleCommand(commText, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter ay7ag = cmd.Parameters.Add("PROJECT_SUBSTANCE", OracleDbType.RefCursor, ParameterDirection.Output);
            connection.Open();
            //cmd.exc
            cmd.ExecuteNonQuery();
            // Retrieving out parameter value
            OracleDataReader myReader = default(OracleDataReader);
            myReader = ((OracleRefCursor)ay7ag.Value).GetDataReader();
            while (myReader.Read())
            {
                for (int x = 0; x <= myReader.FieldCount - 1; x++)
                {
                    var y = myReader[x];
                }
            }
            var uploadSeq = cmd.Parameters[""].Value;
            connection.Close();

            return new GET_PROJECT_DETAILS_RESULT();
        }
    }
}
