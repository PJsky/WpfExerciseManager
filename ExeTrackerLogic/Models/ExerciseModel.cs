using System;
using System.Collections.Generic;
using System.Text;

namespace ExeTrackerLogic.Models
{
    public class ExerciseModel
    {
        public ExerciseModel()
        {

        }
        public ExerciseModel(string name, int weight, int sets, int reps, string type = "")
        {
            Name = name;
            Weight = weight;
            Sets = sets;
            Reps = reps;
            Type = type;
        }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Type { get; set; } = "no type assigned";

        public int ExerciseVolume
        {
            get { return Sets*Reps*Weight; }
        }

    }
}
