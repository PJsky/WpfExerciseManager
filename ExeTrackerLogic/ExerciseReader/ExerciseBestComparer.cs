using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExeTrackerLogic.ExerciseReader
{
    class ExerciseBestComparer : IEqualityComparer<ExerciseModel>
    {
        public bool Equals(ExerciseModel exercise_x, ExerciseModel exercise_y)
        {
            return (exercise_x.Sets == exercise_y.Sets && exercise_x.Reps == exercise_y.Reps);
        }

        public int GetHashCode(ExerciseModel exercise)
        {
            return (exercise.Reps + exercise.Sets + exercise.Name).GetHashCode();
        }
    }
}

