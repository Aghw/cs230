using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.DataAccessLayer;

namespace LearningCenter
{
    public class DatabaseAccessor
    {
        private static readonly EnrollmentEntities entities;

        static DatabaseAccessor()
        {
            entities = new EnrollmentEntities();
            entities.Database.Connection.Open();
        }

        public static EnrollmentEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}