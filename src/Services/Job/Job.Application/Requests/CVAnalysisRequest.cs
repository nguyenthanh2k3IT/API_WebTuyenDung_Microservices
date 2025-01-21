using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.Requests
{
    public class CVAnalysisRequest
    {
        public string CVContent { get; set; } 
        public string JobDescription { get; set; }
        public double MatchingScore { get; set; } 
        public List<string> MatchingSkills { get; set; } 
        public List<string> MissingSkills { get; set; }
    }
}
