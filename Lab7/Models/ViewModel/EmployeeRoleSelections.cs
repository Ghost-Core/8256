using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab7.Models.DataAccess;

namespace Lab7.Models
{
    public class EmployeeRoleSelections
    {

        public Employee employee { get; set; }
        public List<RoleSelection> roleSelections { get; set; }
        public EmployeeRoleSelections()
        {
            employee = new Employee();
            roleSelections = new();
            StudentRecordContext context = new StudentRecordContext();
            foreach (Role role in context.Roles)
            {
                RoleSelection roleSelection = new RoleSelection(role);
                roleSelections.Add(roleSelection);
            }
        }

    }
}
