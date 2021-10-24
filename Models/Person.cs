using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.Models
{
    public class Person
    {
        public virtual int Id { get; set; }

        public virtual string Name  { get; set; }  

        public virtual string SureName { get; set; }

    }
}
