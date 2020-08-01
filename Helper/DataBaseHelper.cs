using ABM.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ABM.Helper
{
    class DataBaseHelper
    {
        public string ConnectionString { get; set; }
        public enum ExecutionType { Scalar, NonQuery };
        public SqlConnection Connection { get; set; }
        public SqlTransaction SqlTransaction { get; set; }

        public DataBaseHelper(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
        }

        public void BeginTransaction()
        {
            if (Connection != null)
            {
                if (Connection.State != ConnectionState.Open)
                {
                    if (string.IsNullOrEmpty(Connection.ConnectionString))
                        Connection.ConnectionString = ConfigurationManager.AppSettings["connectionString"];
                    try
                    {
                        Connection.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(Resources.ErrorBD + ex);
                    }
                }
                if (SqlTransaction == null)
                {
                    try
                    {
                        SqlTransaction = Connection.BeginTransaction();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(Resources.ErrorBeginTrans, ex);
                    }
                }
            }
            else
                throw new Exception(Resources.ErrorNoConnection);
        }

        public void RollbackTransaction()
        {
            if (SqlTransaction != null)
            {
                try
                {
                    SqlTransaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw new Exception(Resources.ErrorRollbackTrans, ex);
                }
            }
            else
                throw new Exception(Resources.ErrorNoTrans);
        }

        public void EndTransaction()
        {
            if (SqlTransaction != null)
            {
                try
                {
                    SqlTransaction.Commit();

                    try
                    {
                        SqlTransaction.Dispose();
                        SqlTransaction = null;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(Resources.ErrorEndTrans, ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(Resources.ErrorTrans, ex);
                }
            }
            else
                throw new Exception(Resources.ErrorNoTrans);
        }

        public DataTable GetDataAsTable(string storedProcedure)
        {
            SqlCommand sqlCommand = Connection.CreateCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

            sqlCommand.Connection = Connection;
            sqlCommand.Transaction = SqlTransaction;

            try
            {
                DataTable sqlTable = new DataTable();

                sqlCommand.CommandText = storedProcedure;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlAdapter.Fill(sqlTable);

                return sqlTable;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(Resources.ErrorSP, ex);
            }
            finally
            {
                if (SqlTransaction == null)
                    EndConnection();

                sqlCommand.Dispose();
                sqlAdapter.Dispose();
            }
        }

        public DataTable GetDataAsTable(string storedProcedure, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = Connection.CreateCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

            sqlCommand.Connection = Connection;
            sqlCommand.Transaction = SqlTransaction;

            try
            {
                DataTable sqlTable = new DataTable();

                sqlCommand.CommandText = storedProcedure;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter sqlPrm in parameters)
                {
                    sqlCommand.Parameters.Add(sqlPrm);
                }

                sqlAdapter.Fill(sqlTable);

                return sqlTable;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(Resources.ErrorSP, ex);
            }
            finally
            {
                if (SqlTransaction == null)
                    EndConnection();

                sqlCommand.Dispose();
                sqlAdapter.Dispose();
            }
        }

        public object ExecInstruction(ExecutionType execType, string storedProcedure, List<SqlParameter> parameters)
        {
            SqlCommand sqlCommand = Connection.CreateCommand();

            sqlCommand.Connection = Connection;
            sqlCommand.Transaction = SqlTransaction;

            try
            {
                object result = null;

                sqlCommand.CommandText = storedProcedure;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter sqlPrm in parameters)
                {
                    sqlCommand.Parameters.Add(sqlPrm);
                }

                switch (execType)
                {
                    case ExecutionType.NonQuery:
                        result = sqlCommand.ExecuteNonQuery();
                        break;
                    case ExecutionType.Scalar:
                        result = sqlCommand.ExecuteScalar();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(Resources.ErrorSP, ex);
            }
            finally
            {
                if (SqlTransaction == null)
                    EndConnection();

                sqlCommand.Dispose();
            }
        }

        public void EndConnection()
        {
            try
            {
                if (Connection != null)
                {
                    if (SqlTransaction != null)
                    {
                        EndTransaction();
                    }

                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Resources.ErrorBD, ex);
            }
        }
    }
}