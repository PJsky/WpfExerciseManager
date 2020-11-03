using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeTrackerTests
{
    public static class TrainingSetupService
    {
        public static List<TrainingModel> GetTestTrainingList(List<List<ExerciseModel>> exerciseLists) 
        {
            var trainingList = new List<TrainingModel>();
            for(int i = 0; i<exerciseLists.Count;i++)
                trainingList.Add(new TrainingModel(
                    DateTime.Now.AddDays(i * Math.Pow(-1, i)),
                    exerciseLists[i]
                    ));
            return trainingList;
        }

        public static List<TrainingModel> GetTestTrainingList()
        {
            var trainingList = new List<TrainingModel>();
            for(int i =0; i < 15; i++)
            {
                trainingList.Add(new TrainingModel(DateTime.Now.AddDays(i).AddMonths(i)));
            }
            trainingList.Add(new TrainingModel(DateTime.Now));
            return trainingList;
        }
    }
}
