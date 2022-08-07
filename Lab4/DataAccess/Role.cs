using System;
using System.Collections.Generic;

#nullable disable

namespace Lab4.DataAccess
{
    public partial class Role
    {
        public Role()
        {
            EmployeeRoles = new HashSet<EmployeeRole>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
    }
}
