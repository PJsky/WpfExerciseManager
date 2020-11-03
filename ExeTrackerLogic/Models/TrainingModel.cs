using System;
using System.Collections.Generic;
using System.Text;

namespace ExeTrackerLogic.Models
{
    public class TrainingModel
    {
        public TrainingModel()
        {

        }
        public TrainingModel(DateTime trainingDay, List<ExerciseModel> exercises = null)
        {
            TrainingDay = trainingDay;
            //If exercises are not passed assign it a empty list of ExerciseModel 
            Exercises = exercises ?? new List<ExerciseModel>();
        }
        public TrainingModel(int year, int month, int day, List<ExerciseModel> exercises = null)
        {
            TrainingDay = new DateTime(year, month, day);
            //If exercises are not passed assign it a empty list of ExerciseModel 
            Exercises = exercises ?? new List<ExerciseModel>();
        }
        public DateTime TrainingDay { get; set; }
        public int PositionInMonth { get; set; }
        public List<ExerciseModel> Exercises { get; set; }
    }
}
