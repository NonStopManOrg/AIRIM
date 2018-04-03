using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.AIRIM.Hypothesis.Data
{
    public class Constants
    {
        public const string ConnectionString = "Data Source = (DESCRIPTION = "
                                        + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=frialora15.eame.syngenta.org)(PORT=1528)))"
                                        + "(CONNECT_DATA=(SID = AIRR4)(SERVER = DEDICATED)));"
                                        + "User Id=SCO_APR;Password=SCO_APR;";
    }
}
