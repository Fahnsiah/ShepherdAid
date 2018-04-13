using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShepherdAid.Models
{
    public class AvaliableRolesModel
    {
        
        public IList<string> SelectedRoles { get; set; }
        public IList<SelectListItem> AvailableRoles { get; set; }

        public AvaliableRolesModel()
        {
            SelectedRoles = new List<string>();
            AvailableRoles = new List<SelectListItem>();
        }
    }

    public class AssignedRolesModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

}