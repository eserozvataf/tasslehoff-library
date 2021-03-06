﻿// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="-">
// Copyright (c) 2014 Eser Ozvataf (eser@sent.com). All rights reserved.
// Web: http://eser.ozvataf.com/ GitHub: http://github.com/larukedi
// </copyright>
// <author>Eser Ozvataf (eser@sent.com)</author>
// -----------------------------------------------------------------------

//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
////
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Tasslehoff.Library.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    /// Database class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Database
    {
        // constants

        /// <summary>
        /// The default command timeout
        /// </summary>
        public const int DefaultCommandTimeout = 3600;

        // fields

        /// <summary>
        /// The database driver
        /// </summary>
        [DataMember]
        private string databaseDriver;

        /// <summary>
        /// The connection string
        /// </summary>
        [DataMember]
        private string connectionString;

        /// <summary>
        /// The database provider factory
        /// </summary>
        [NonSerialized]
        private DbProviderFactory providerFactory;

        // constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="databaseDriver">The database driver</param>
        /// <param name="connectionString">The connection string</param>
        public Database(string databaseDriver, string connectionString)
        {
            this.databaseDriver = databaseDriver;
            this.connectionString = connectionString;
        }

        // methods

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <returns>The database factory class of the database driver</returns>
        public DbProviderFactory GetFactory()
        {
            if (this.providerFactory == null)
            {
                this.providerFactory = DbProviderFactories.GetFactory(databaseDriver);
            }

            return this.providerFactory;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>A database connection</returns>
        /// <exception cref="System.NotImplementedException">If connection could not be created.</exception>
        public DbConnection GetConnection()
        {
            DbConnection connection = this.GetFactory().CreateConnection();
            if (connection == null)
            {
                throw new NotImplementedException();
            }

            connection.ConnectionString = this.connectionString;
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            
            return connection;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="connection">The connection</param>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="commandTimeout">The command timeout</param>
        /// <returns>A command object related to database connection</returns>
        /// <exception cref="System.NotImplementedException">If command object could not be created.</exception>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Data access layer already sends queries parameterized.")]
        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType = CommandType.Text, int commandTimeout = Database.DefaultCommandTimeout)
        {
            DbCommand result = connection.CreateCommand();
            if (result == null)
            {
                throw new NotImplementedException();
            }
            
            result.CommandText = commandText;
            result.CommandType = commandType;
            result.CommandTimeout = commandTimeout;
            
            return result;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="value">The value</param>
        /// <returns>A parameter object related to database</returns>
        /// <exception cref="System.NotImplementedException">If parameter object could not be created.</exception>
        public DbParameter GetParameter(string name, object value)
        {
            DbParameter result = this.GetFactory().CreateParameter();
            if (result == null)
            {
                throw new NotImplementedException();
            }
            
            result.ParameterName = name;
            result.Value = value;
            
            return result;
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="commandBehavior">The command behavior</param>
        /// <param name="parameters">The parameters</param>
        /// <param name="function">The function</param>
        public void ExecuteReader(string commandText, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default, IEnumerable<DbParameter> parameters = null, Action<DbDataReader> function = null)
        {
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = this.GetCommand(connection, commandText, commandType))
                {
                    if (parameters != null)
                    {
                        // Parallel.ForEach<DbParameter>(parameters, parameter => {
                        foreach (DbParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    
                    DbDataReader reader = command.ExecuteReader(commandBehavior);
                    try
                    {
                        if (function != null)
                        {
                            function.Invoke(reader);
                        }
                    }
                    finally
                    {
                        reader.Close(); // reader.Disponse() instead?
                    }
                }
                
                // connection.Close();
            }
        }

        /// <summary>
        /// Executes the enumerable.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="commandBehavior">The command behavior</param>
        /// <param name="parameters">The parameters</param>
        public IEnumerable ExecuteEnumerable(string commandText, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default, IEnumerable<DbParameter> parameters = null)
        {
            List<object[]> result = new List<object[]>();

            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = this.GetCommand(connection, commandText, commandType))
                {
                    if (parameters != null)
                    {
                        // Parallel.ForEach<DbParameter>(parameters, parameter => {
                        foreach (DbParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    DbDataReader reader = command.ExecuteReader(commandBehavior);
                    try
                    {
                        while (reader.Read())
                        {
                            object[] row = new object[reader.FieldCount];
                            for (int i = row.Length - 1; i >= 0; i--)
                            {
                                row[i] = reader[i];
                            }

                            result.Add(row);
                        }
                    }
                    finally
                    {
                        reader.Close(); // reader.Disponse() instead?
                    }
                }

                // connection.Close();
            }

            return result.ToArray();
        }

        /// <summary>
        /// Executes the DataTable.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="commandBehavior">The command behavior</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>A DataTable returned from the database query</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default, IEnumerable<DbParameter> parameters = null)
        {
            DataTable dataTable = new DataTable();

            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = this.GetCommand(connection, commandText, commandType))
                {
                    if (parameters != null)
                    {
                        // Parallel.ForEach<DbParameter>(parameters, parameter => {
                        foreach (DbParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    DbDataReader reader = command.ExecuteReader(commandBehavior);
                    try
                    {
                        dataTable.Load(reader);
                    }
                    finally
                    {
                        reader.Close(); // reader.Disponse() instead?
                    }
                }

                // connection.Close();
            }

            return dataTable;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>A single field returned from the database query</returns>
        public object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text, IEnumerable<DbParameter> parameters = null)
        {
            object result;
            
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = this.GetCommand(connection, commandText, commandType))
                {
                    if (parameters != null)
                    {
                        // Parallel.ForEach<DbParameter>(parameters, parameter => {
                        foreach (DbParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    
                    result = command.ExecuteScalar();
                }
                
                // connection.Close();
            }
            
            return result;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="commandType">Type of the command</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>Number of records affected</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, IEnumerable<DbParameter> parameters = null)
        {
            int result;
            
            using (DbConnection connection = this.GetConnection())
            {
                using (DbCommand command = this.GetCommand(connection, commandText, commandType))
                {
                    if (parameters != null)
                    {
                        // Parallel.ForEach<DbParameter>(parameters, parameter => {
                        foreach (DbParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    
                    result = command.ExecuteNonQuery();
                }
                
                // connection.Close();
            }
            
            return result;
        }

        /// <summary>
        /// Creates a new DataQuery instance.
        /// </summary>
        /// <returns>A new data query instance</returns>
        public DataQuery NewQuery()
        {
            return new DataQuery(this);
        }
    }
}
