using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lab7.Models.EntityMetadata;

namespace Lab7.Models.DataAccess
{
    [ModelMetadataType(typeof(RoleMetaData))]
    public partial class Role
    {
    }
}
