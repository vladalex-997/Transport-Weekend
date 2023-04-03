using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Transport_Weekend
{
    public class GetPrivilage
    {
        public string GetRights(string inputuserId)
        {
            Database databaseObject = new Database();
            string temporary = "";
            string queryuser = "SELECT UserRole from Users where UniqueId = @UniqueId";


            SqlCommand myCommanduser = new SqlCommand(queryuser, databaseObject.myConnection);
            myCommanduser.Parameters.AddWithValue("@UniqueId", inputuserId);   

            databaseObject.OpenConnection();
            temporary = (string)myCommanduser.ExecuteScalar();

            databaseObject.CloseConnection();

            return temporary;
        }
    }
}