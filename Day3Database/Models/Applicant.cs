﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Day3Database.Models
{
    public class Applicant
    {

        public Guid ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }
        
    }
}
