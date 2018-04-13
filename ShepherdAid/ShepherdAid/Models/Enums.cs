using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    public class Enums
    {
        public enum Currency
        {
            LRD = 1,
            USD = 2,
        }
        public enum StatusType
        {
            Active = 1,
            Inactive = 2,

        }
        public enum Company
        {
            DefaultCompany = 100,
        }

        public enum DocumentType
        {
            MemberPhoto = 100,
        }

        public enum SystemVariable
        {
            DefaultPassword = 100,
            LRDToUSDRate = 101,
        }

        public enum ObligationStatusType
        {
            Active = 1,
            Paid = 2,
            Cancelled = 3,
            Suspended = 4,
        }

        public enum RoleAccessTypes
        {
            OnlySuperAdmin = 10,
            ClientAdminAndAbove = 11,
            InstitutionGroupAdminAndAbove = 12,
            InstitutionAdmin = 13,
        }

        public enum Nationality
        {
            DefaultNationality = 88,
        }

        public enum ObligationFrequency
        {
            Once = 1,
            Monthly = 2,
            Yearly = 3
        }

        public enum ObligationStatus
        {
            Active = 1,
            Paid = 2,
            Suspended = 3,
            Cancelled = 4,
        }
    }
}