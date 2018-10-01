using Day3Database.Models;
using Day3Database.Repositories;
using System;
using System.Collections.Generic;

namespace Day3Database
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Applicant
             var shan = CreateApplicant("Shanaiah", "Raguine", "Roldan", DateTime.Parse("1997-05-20"));
             var jeline = CreateApplicant("Jeline", "Raguine", "Roldan", DateTime.Parse("1995-03-20"));
             var ren = CreateApplicant("Ren", "Cordero", "Pedrajeta", DateTime.Parse("1995-07-25"));
             
             var applicant = RetrieveApplicant(shan.ApplicantID);
             
             UpdateApplicant(shan);
             
             var allApplicants = RetrieveAllApplicants();
             
             allApplicants.ForEach((a) => DeleteApplicant(applicant.ApplicantID));
           
             */

            //Employee
         //   var Emp1 = CreateEmployee("Shanaiah", "Raguine", "Roldan", new Guid("4D7E3FAF-CAB2-43B7-9E51-1EBB99AEA227"));
            /* var Emp2 = CreateEmployee("Jeline", "Raguine", "Roldan", );
             var Emp3 = CreateEmployee("Renren", "Cordero", "Pedrajeta", "Information Technology");
             var Emp4 = CreateEmployee("Lina", "Raguine", "Roldan", "Accounting");
             var Emp5 = CreateEmployee("Mickel", "Abadilla", "Berdos", "Marketing"); */

            var allEmployees = RetrieveAllEmployees();

            foreach (var x in allEmployees)
            {
                Console.WriteLine("Employee ID: {0}", x.EmployeeID);
                Console.WriteLine("Employee's First Name: {0}", x.EmpFirstName);
                Console.WriteLine("Employee's Middle Name: {0}", x.EmpFirstName);
            }
           // allEmployees.ForEach((a) => DeleteEmployee(a.EmployeeID));

          //  UpdateEmployee(Emp1, "Shan");

            //Department

            var IT = CreateDepartment("Information Technology", true);
            var HR = CreateDepartment("Human Resources", true);
            var Accounting = CreateDepartment("Accounting", true);
            var RD = CreateDepartment("Research and Development", false);
            var MKT = CreateDepartment("Marketing", false);

            var department = RetrieveDepartment(IT.DepartmentID);

            MKT.IsActive = true;

            UpdateDepartment(IT, "ITD");

            var allDepartments = RetrieveAllDepartments();

            allDepartments.ForEach((a) => DeleteDepartment(department.DepartmentID));

            Console.ReadLine();
        }
        private static List<Employee> RetrieveAllEmployees()
        {
            var repository = new EmployeeRepository();
            return repository.Retrieve();
        }
        static void UpdateEmployee(Employee employee, string empFirstName)
        {

            employee.EmpFirstName = empFirstName;
            var repository = new EmployeeRepository();
            repository.Update(employee);

        }
        private static Employee RetrieveEmployee(Guid employeeid)
        {
            var repository = new EmployeeRepository();
            return repository.Retrieve(employeeid);
        }
        private static void DeleteEmployee(Guid employeeID)
        {
            var repository = new EmployeeRepository();
            repository.Delete(employeeID);
        }
        static Employee CreateEmployee(string empFirstName, string empMiddleName, string empLastName, Guid department)
        {
            var employee = new Employee
            {
                EmployeeID = Guid.NewGuid(),
                EmpFirstName = empFirstName,
                EmpMiddleName = empMiddleName,
                EmpLastName = empLastName,
                DepartmentID = department
                
            };

            var repository = new EmployeeRepository();
            var newEmployee = repository.Create(employee);
            return newEmployee;
        }

        
        //Department
        private static List<Department> RetrieveAllDepartments()
        {
            var repository = new DepartmentRepository();
            return repository.Retrieve();
        }
        static void UpdateDepartment(Department department, string departmentName)
        {

            department.DepartmentName = departmentName;
            var repository = new DepartmentRepository();
            repository.Update(department);
        }
        private static Department RetrieveDepartment(Guid departmentid)
        {
            var repository = new DepartmentRepository();
            return repository.Retrieve(departmentid);
        }
        private static void DeleteDepartment(Guid departmentid)
        {
            var repository = new DepartmentRepository();
            repository.Delete(departmentid);
        }
        static Department CreateDepartment(string departmentName, bool isActive)
        {
            var department = new Department
            {
                DepartmentID = Guid.NewGuid(),
                DepartmentName = departmentName,
                IsActive = isActive

            };

            var repository = new DepartmentRepository();
            var newDepartment = repository.Create(department);
            return newDepartment;
        }
    }
}       