using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeTrackerTests
{
    public static class ExerciseSetupService
    {
        public static List<ExerciseModel> GetTestTrainingExercises(int sets, int reps, int weight) {
            var exerciseList = new List<ExerciseModel>()
            {
                new ExerciseModel("Bench Press",weight,sets,reps,"HPush"),
                new ExerciseModel("Pendley Row", weight,sets,reps,"HPull"),
                new ExerciseModel("OHP", weight,sets,reps,"VPush"),
                new ExerciseModel("Pullups", weight,sets,reps,"VPull"),
                new ExerciseModel("Bicep curl", weight,sets,reps,"Bicep"),
                new ExerciseModel("Tricep pushdown", weight,sets,reps,"Tricep"),
                new ExerciseModel("Squats", weight,sets,reps,"Legs")
            };
            return exerciseList;
        }
    }
}
