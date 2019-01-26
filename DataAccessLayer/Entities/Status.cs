using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Status
    {
        public enum StatusType
        {
            Uploaded,
            Commented,
            Accepted,
            Rejected
        }
    }
}