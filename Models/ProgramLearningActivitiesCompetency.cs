using System;

namespace UCT.Models
{
    public class ProgramLearningActivitiesCompetency
    {
        public int ID { get; set; }
        public int ProgramLearningActivitiesID { get; set; }
        public int ProgramID { get; set; }
        public int LearningActivitiesID { get; set; }
        public int CompetenciesID { get; set; }
        public Nullable<decimal> LearningYear { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual ProgramLearningActivity ProgramLearningActivities { get; set; }
    }
}