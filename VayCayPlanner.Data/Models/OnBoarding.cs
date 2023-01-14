using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class OnBoarding
    {
        public int Id { get; set; }
        public string SubscriberId { get; set; }
        public int CurrentStep { get; set; }
        public bool isComplete { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
