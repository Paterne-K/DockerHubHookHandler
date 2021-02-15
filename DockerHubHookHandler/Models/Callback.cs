using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DockerHubHookHandler.Models
{
    public class Callback
    {
        [Required]
        public string state { get; set; }
        public string description { get; set; }
        public string context { get; set; }
        public string target_url { get; set; }
    }
}
