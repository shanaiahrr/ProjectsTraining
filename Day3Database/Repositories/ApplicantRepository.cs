using Day3Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Day3Database.Repositories
{
    public class ApplicantRepository : RepositoryBase<Applicant>
    {
        

        protected string insertStatement = @"INSERT INTO [APPLICANT] 
                                    (     ApplicantID,
                                          FirstName,
                                          MiddleName,
                                          LastName,
                                          Birthdate
                                    )     VALUES
                                    (
                                          @applicantID,
                                          @firstName,
                                          @middleName,
                                          @lastName,
                                          @birthDate
                                    )";

        private readonly string deleteStatement = @"DELETE FROM [Applicant] WHERE ApplicantID = @applicantID";

        private readonly string retrieveStatement = @"SELECT 
                                                        ApplicantID, 
                                                        FirstName, 
                                                        MiddleName, 
                                                        LastName, 
                                                        Birthdate 
                                                    FROM [Applicant] "; 
                                                    
        private readonly string retrieveFilter = @"WHERE ApplicantID = @applicantID";

        private readonly string updateStatement = @"UPDATE [Applicant] 
                                    SET
                                         FirstName = @firstName,
                                          MiddleName = @middleName,
                                          LastName = @lastName,
                                          Birthdate = @Birthdate
                                   
                                    WHERE ApplicantID = @applicantID";
        public ApplicantRepository()
        {
            base.InsertStatement = this.InsertStatement;
            base.DeleteStatement = this.DeleteStatement;
            base.UpdateStatement = this.UpdateStatement;
            base.RetrieveStatement = this.RetrieveStatement;
            base.RetrieveAllStatement = this.RetrieveAllStatement;
        }
        protected override void LoadInsertParameters(SqlCommand command, Applicant newApplicant)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = newApplicant.ApplicantID;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = newApplicant.FirstName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50).Value = newApplicant.MiddleName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = newApplicant.LastName;
            command.Parameters.Add("@birthdate", SqlDbType.Date).Value = newApplicant.Birthdate;
            command.ExecuteNonQuery();
        }

        protected override void LoadDeleteParameters(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = id;
        }
        protected override void LoadUpdateParameters(SqlCommand command, Applicant applicant)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = applicant.ApplicantID;
            command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = applicant.FirstName;
            command.Parameters.Add("@middleName", SqlDbType.NVarChar, 50).Value = applicant.MiddleName;
            command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = applicant.LastName;
            command.Parameters.Add("@birthdate", SqlDbType.Date).Value = applicant.Birthdate;
        }

        protected override Applicant LoadEntity(SqlDataReader reader)
        {
            var applicant = new Applicant();
            applicant.ApplicantID = reader.GetGuid(0);
            applicant.FirstName = reader.GetString(1);
            applicant.MiddleName = reader.GetString(2);
            applicant.LastName = reader.GetString(3);
            applicant.Birthdate = reader.GetDateTime(4);
            return applicant;
        }
        protected override void LoadRetrieveParameter(SqlCommand command, Guid id)
        {
            command.Parameters.Add("@applicantID", SqlDbType.UniqueIdentifier).Value = id;
        }
       
    }
}
