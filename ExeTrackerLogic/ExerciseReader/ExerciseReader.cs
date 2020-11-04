using ExeTrackerLogic.ExerciseReader;
using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExeTrackerLogic.ExcersizeReader
{
    public static class _ExerciseReader
    {
        //Return all exercise records from a list of trainings
        public static List<ExerciseModel> GetFromTrainingList(List<TrainingModel> trainingList)
        {
            var exerciseList = new List<ExerciseModel>();
            foreach (TrainingModel training in trainingList)
                exerciseList.AddRange(training.Exercises);
            return exerciseList;
        }

        //Returns a list of exercise records with a given type
        public static List<ExerciseModel> GetByType(List<ExerciseModel> exerciseList, string type)
        {
            var exercisesByType = new List<ExerciseModel>();
            exercisesByType = exerciseList.Where(e => e.Type == type).ToList();
            return exercisesByType;
        }

        //Overload for training list
        public static List<ExerciseModel> GetByType(List<TrainingModel> trainingList, string type)
        {
            return GetFromTrainingList(trainingList)
                .Where(e => e.Type == type).ToList();
        }

        //Returns a list of best weights achieved on a given exercises for each Sets x Reps pattern
        public static List<ExerciseModel> GetExerciseBest(List<TrainingModel> trainingList, string exerciseName)
        {
            return GetFromTrainingList(trainingList)
                .Where(e => e.Name == exerciseName)
                .OrderByDescending(e => e.Weight)
                .Distinct(new ExerciseBestComparer())
                .ToList();
        }

        //Returns a list of execercise stats in all training sessions
        public static List<ExerciseModel> GetExerciseHistory(List<TrainingModel> trainingList, string exerciseName)
        {
            return GetFromTrainingList(trainingList.OrderByDescending(t => t.TrainingDay).ToList())
                .Where(e => e.Name == exerciseName)
                .ToList();
        }
    }
}
