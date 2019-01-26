using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Role
    {
        public enum RoleType
        {
            Admin,
            Teacher,
            Student
        }
    }
}