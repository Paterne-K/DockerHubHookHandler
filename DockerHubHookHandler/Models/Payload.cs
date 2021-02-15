using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerHubHookHandler.Models
{
    public class Payload
    {
        public string callback_url { get; set; }
        public Push_Data push_data { get; set; }
        public Repository repository { get; set; }

        public class Push_Data
        {
            public string[] images { get; set; }
            public float pushed_at { get; set; }
            public string pusher { get; set; }
            public string tag { get; set; }
        }

        public class Repository
        {
            public int comment_count { get; set; }
            public float date_created { get; set; }
            public string description { get; set; }
            public string dockerfile { get; set; }
            public string full_description { get; set; }
            public bool is_official { get; set; }
            public bool is_private { get; set; }
            public bool is_trusted { get; set; }
            public string name { get; set; }
            public string _namespace { get; set; }
            public string owner { get; set; }
            public string repo_name { get; set; }
            public string repo_url { get; set; }
            public int star_count { get; set; }
            public string status { get; set; }
        }

    }
}
