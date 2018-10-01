using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        protected string insertStatement = @"INSERT INTO [EMPLOYEE] 
                                    (     EmployeeID,
                                          EmpFirstName,
                                          EmpMiddleName,
                                          EmpLastName,
                                          DepartmentID,
                                          HireDate

                                    )     VALUES
                                    (
                                          @employeeID,
                                          @empfirstName,
                                          @empmiddleName,
                                          @emplastName,
                                          @departmentID
                                          @hireDate
                                    )";
        private readonly string deleteStatement = @"DELETE FROM [Employee] WHERE EmployeeID = @employeeID";

        private readonly string retrieveStatement = @"SELECT 
                                                        emp.EmployeeID,
                                                        emp.EmpFirstName,
                                                        emp.EmpMiddleName,
                                                        emp.EmpLastName,
                                                        dept.DepartmentName,
                                                        emp.HireDate
                                                        FROM [Employee] emp
                                                        INNER JOIN
                                                        Department dept 
                                                        ON emp.EmployeeID = dept.DepartmentID";

        private readonly string retrieveFilter = @" WHERE EmployeeID = @employeeID";

        private readonly string retrieveAllStatement = @"SELECT 
                                                        emp.EmployeeID,
                                                        emp.EmpFirstName,
                                                        emp.EmpMiddleName,
                                                        emp.EmpLastName,
                                                        dept.DepartmentName, 
                                                        emp.HireDate
                                                        FROM [Employee] emp
                                                        INNER JOIN
                                                        Department dept 
                                                        ON emp.DepartmentID = dept.DepartmentID";

        private readonly string updateStatement = @"UPDATE [Employee] 
                                    SET
                                          EmpFirstName = @empfirstName,
                                          EmpMiddleName = @empmiddleName,
                                          EmpLastName = @emplastName,
                                          DepartmentID = @departmentID, 
                                          HireDate =  @hireDate
                                    WHERE EmployeeID = @employeeID";
        public EmployeeRepository()
        {
            base.InsertStatement = this.insertStatement;
            base.DeleteStatement = this.deleteStatement;
            base.UpdateStatement = this.updateStatement;
            base.RetrieveAllStatement = this.retrieveAllStatement;

            base.RetrieveStatement = this.retrieveStatement + this.retrieveFilter;
        }
        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override Employee LoadEntity(SqlDataReader reader)
        {
            var employee = new Employee();
            employee.EmployeeID = reader.GetGuid(0);
            employee.EmpFirstName = reader.GetString(1);
            employee.EmpMiddleName = reader.GetString(2);
            employee.EmpLastName = reader.GetString(3);
            employee.Department = new Department();
            employee.Department.DepartmentID = reader.GetGuid(3);
            employee.Department.DepartmentName = reader.GetString(4);
            employee.HireDate = reader[5] != DBNull.Value ? reader.GetDate(5) : null;

            return employee;
        }

        protected override void LoadInsertParameters(SqlCommand command, Employee newEmployee)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = newEmployee.EmployeeID;
            command.Parameters.Add("@empfirstName", SqlDbType.NVarChar, 50).Value = newEmployee.EmpFirstName;
            command.Parameters.Add("@empmiddleName", SqlDbType.NVarChar, 50).Value = newEmployee.EmpMiddleName;
            command.Parameters.Add("@emplastName", SqlDbType.NVarChar, 50).Value = newEmployee.EmpLastName;
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = newEmployee.DepartmentID;
            command.Parameters.Add("@hireDate", SqlDbType.Date).Value = newEmployee.HireDate;
        }

        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = id;
        }

        protected override void LoadUpdateParameters(SqlCommand command, Employee employee)
        {   
            command.Parameters.Add("@employeeID", SqlDbType.UniqueIdentifier).Value = employee.EmployeeID;
            command.Parameters.Add("@empfirstName", SqlDbType.NVarChar, 50).Value = employee.EmpFirstName;
            command.Parameters.Add("@empmiddleName", SqlDbType.NVarChar, 50).Value = employee.EmpMiddleName;
            command.Parameters.Add("@emplastName", SqlDbType.NVarChar, 50).Value = employee.EmpLastName;
            command.Parameters.Add("@departmentID", SqlDbType.UniqueIdentifier).Value = employee.DepartmentID;
            command.Parameters.Add("@hireDate", SqlDbType.Date).Value = employee.HireDate;
        }
    }
}
