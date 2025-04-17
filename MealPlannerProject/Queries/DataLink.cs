namespace MealPlannerProject.Queries
{
    using MealPlannerProject.Exceptions;
    using MealPlannerProject.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public sealed partial class DataLink : IDisposable, IDataLink
    {
        [Obsolete]
        private static readonly Lazy<DataLink> InstanceValue = new(() => new DataLink());
        private readonly string connectionString;
        [Obsolete]
        private SqlConnection? sqlConnection;
        private bool disposedValue;

        [Obsolete]
        private DataLink()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                string? localDataSource = config["LocalDataSource"];
                string? initialCatalog = config["InitialCatalog"];

                if (string.IsNullOrEmpty(localDataSource) || string.IsNullOrEmpty(initialCatalog))
                {
                    throw new ArgumentNullException("LocalDataSource or InitialCatalog is null or empty");
                }

                this.connectionString = $"Data Source={localDataSource};Initial Catalog={initialCatalog};Integrated Security=True; TrustServerCertificate = True";

                using var connection = new SqlConnection(this.connectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Failed to establish database connection. Please check your connection settings.", ex);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Failed to initialize database connection.", ex);
            }
        }

        [Obsolete]
        public static DataLink Instance => InstanceValue.Value;

        [Obsolete]
        public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteScalar operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteScalar operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteQuery(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();
                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public T? ExecuteBool<T>(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteBool operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteBool operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteReader operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteReader operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Obsolete]
        public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text,  // Always a raw SQL query
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        private SqlConnection GetConnection()
        {
            ObjectDisposedException.ThrowIf(this.disposedValue, new ObjectDisposedException("DataLink"));

            if (this.sqlConnection == null || this.sqlConnection.State != System.Data.ConnectionState.Open)
            {
                this.sqlConnection = new SqlConnection(this.connectionString);
            }

            return this.sqlConnection;
        }

        [Obsolete]
        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.sqlConnection != null)
                    {
                        if (this.sqlConnection.State == ConnectionState.Open)
                        {
                            this.sqlConnection.Close();
                        }

                        this.sqlConnection.Dispose();
                        this.sqlConnection = null;
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}