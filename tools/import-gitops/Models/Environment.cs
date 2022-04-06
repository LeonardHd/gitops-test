using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace import_gitops.Models
{
    internal class Environment
    {
        public string? Name { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();
    }
}
