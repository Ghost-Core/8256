using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lab7.Models.EntityMetadata;

namespace Lab7.Models.DataAccess
{
    [ModelMetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {

        [NotMapped]
        public List<Role> Roles
        {
            get
            {
                List<Role> roles = new();
                using (StudentRecordContext context = new StudentRecordContext())
                {
                    roles = (from r in context.Roles where context.EmployeeRoles.Any(er => er.RoleId == r.Id && er.EmployeeId == this.Id) select r).ToList<Role>();
                }
                return roles;
            }
        }
    }
}
