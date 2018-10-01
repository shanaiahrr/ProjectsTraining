using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    { 
        protected string ConnectionString { get; private set; }
        protected string InsertStatement { get; set; }
        protected string DeleteStatement { get; set; }
        protected string UpdateStatement { get; set; }
        protected string RetrieveStatement { get; set; }
        protected string RetrieveAllStatement { get; set; }
        public RepositoryBase()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["OnlineStoreDB"].ConnectionString;
        }

        protected abstract void LoadInsertParameters(SqlCommand command, TEntity newEntity);
        public TEntity Create(TEntity newEntity)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = InsertStatement;
                    LoadInsertParameters(command,newEntity);
                    command.ExecuteNonQuery();

                    return newEntity;
                }
                
            }
            
        }

        protected abstract void LoadDeleteParameters(SqlCommand command, Guid id);
        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = DeleteStatement;
                    LoadDeleteParameters(command, id);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected abstract void LoadUpdateParameters(SqlCommand comand, TEntity entity);
        public TEntity Update(TEntity entity)
        {
            //TODO: Use validations here.
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = UpdateStatement;
                    LoadUpdateParameters(command, entity);
                    command.ExecuteNonQuery();

                    return entity;

                }
            }
        }
        protected abstract void LoadRetrieveParameter(SqlCommand command, Guid id);
        protected abstract TEntity LoadEntity(SqlDataReader reader);

        public TEntity Retrieve(Guid id)
        {
            TEntity entity = null;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = RetrieveStatement;

                    LoadRetrieveParameter(command, id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                            break;
                        }
                    }
                }
            }
            return entity;
        }
        public List<TEntity> Retrieve()
        {
            var entities = new List<TEntity>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = RetrieveAllStatement;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = LoadEntity(reader);
                            entities.Add(entity);

                        }
                    }
                }
            }
            return entities;
        }
    }
    
}
