using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppWithSignalR.Models
{
    public class Roster
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public int Age { get; set; }
    }
}
