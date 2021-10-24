using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace Audit.Models.Common
{
    public class SiteSettings
    {
        public ConnectionString ConnectionString { get; set; }
    }

    public class ConnectionString
    {
        public string AuditableConnectionStrings { get; set; }
    }
}
