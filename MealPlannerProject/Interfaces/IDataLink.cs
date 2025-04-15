
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces
{
    public interface IDataLink
    {
        [Obsolete]
        T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters);
        [Obsolete]
        DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters);
    }
}
