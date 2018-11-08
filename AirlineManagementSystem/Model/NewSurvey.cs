using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class NewSurvey
    {
        public string Question { get; set; }
        public int Outstanding { get; set; }
        public int VeryGood { get; set; }
        public int Good { get; set; }
        public int Adequate { get; set; }
        public int NeedImprovement { get; set; }
        public int Poor { get; set; }
        public int NotKnow { get; set; }
    }
    class SurveyData
    {
        public string Question { get; set; }
        public int NumberOfAnswer { get; set; }
    }
}
