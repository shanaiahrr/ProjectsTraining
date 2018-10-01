using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Day3Database.Models;
using System.Data;

namespace Day3Database.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
    {
        

        protected string insertStatement = @"INSERT INTO [Department] 
                                    (     DepartmentID,
                                          DepartmentName,
                                          IsActive

                                    )     VALUES
                                    (
                                          @DepartmentID,
                                          @DepartmentName,
                                          @IsActive

                                    )";

        private readonly string retrieveStatement = @"SELECT 
                                                        DepartmentID, 
                                                        DepartmentName, 
                                                        IsActive

                                                    FROM [Department] ";

        private readonly string retrieveFilter = @"WHERE DepartmentID = @departmentID";

        private readonly string retrieveAllStatement = @"SELECT 
                                                        DepartmentID, 
                                                        DepartmentName, 
                                                        IsActive

                                                    FROM [Department] ";

        private readonly string updateStatement = @"UPDATE [Department] 
                                                    SET
                                                    DepartmentName = @departmentName,
                                                    IsActive = @isActive                          
                                                    WHERE DepartmentID = @departmentID";

        private readonly string deleteStatement = @"DELETE FROM [Department] WHERE DepartmentID = @departmentID";

        public DepartmentRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveStatement = this.retrieveStatement;
            base.RetrieveAllStatement = this.retrieveAllStatement;
        }
        protected override void LoadInsertParameters(SqlCommand command, Department newDepartment)
        {
            command.Parameters.Add("@DepartmentID", SqlDbType.UniqueIdentifier).Value = newDepartment.DepartmentID;
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 50).Value = newDepartment.DepartmentName;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = newDepartment.IsActive;
        }
        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override void LoadUpdateParameters(SqlCommand command, Department department)
        {
            command.Parameters.Add("@DepartmentID", SqlDbType.UniqueIdentifier).Value = department.DepartmentID;
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 50).Value = department.DepartmentName;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = department.IsActive;

        }
        protected override Department LoadEntity(SqlDataReader reader)
        {
            var department = new Department();
            department.DepartmentID = reader.GetGuid(0);
            department.DepartmentName = reader.GetString(1);
            department.IsActive = reader.GetBoolean(2);
            return department;
        }
        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = id;
        }
        
        
        }
 
    }

