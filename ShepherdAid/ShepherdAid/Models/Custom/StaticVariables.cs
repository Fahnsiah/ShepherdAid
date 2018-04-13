using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    public class StaticVariables
    {
        public static string MemberDefaultPhoto = "Images/Members/default.jpg";
        public static string MemberDefaultFolder = "Images/Members";
        public static string SuperAdmin = "Super Admin";
        public static string ClientAdmin = "Client Admin";
        public static string InstitutionGroupAdmin = "Institution Group Admin";
        public static string InstitutionAdmin = "Institution Admin";
        public static string DefaultUserGroup = "USERS";
        public const string SuperAdminConstant = "Super Admin";
        public const string SuperClientAdminConstant = "Super Admin, Client Admin";
        public const string SuperClientInstitutionGroupAdminConstant = "Super Admin, Client Admin, Institution Group Admin";
        public const string SuperClientInstitutionGroupInstitutionAdminConstant = "Super Admin, Client Admin, Institution Group Admin, Institution Admin";
    }
}