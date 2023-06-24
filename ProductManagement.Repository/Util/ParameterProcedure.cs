using System.Data.SqlClient;

namespace ProductManagement.DAL.Util
{
    public class ParameterProcedure
    {
        public void AddParameter(List<SqlParameter> sqlParameters, string ParameterName, object value)
        {
            SqlParameter parameter = new SqlParameter();

            parameter.ParameterName = ParameterName;
            parameter.Value = value;

            sqlParameters.Add(parameter);

        }
    }
}
