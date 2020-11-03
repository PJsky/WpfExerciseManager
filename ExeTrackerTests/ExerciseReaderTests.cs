using ExeTrackerLogic.ExcersizeReader;
using ExeTrackerLogic.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExeTrackerTests
{
    [TestFixture]
    public class ExerciseReaderTests
    {
        private List<TrainingModel> trainingList = new List<TrainingModel>();
        private List<ExerciseModel> exerciseList = new List<ExerciseModel>();

        [SetUp]
        public void Setup()
        {

            trainingList = TrainingSetupService.GetTestTrainingList(new List<List<ExerciseModel>>()
            {
                ExerciseSetupService.GetTestTrainingExercises(5,5,100),
                ExerciseSetupService.GetTestTrainingExercises(5,5,120),
                ExerciseSetupService.GetTestTrainingExercises(5,5,140),
                ExerciseSetupService.GetTestTrainingExercises(1,1,200),
                ExerciseSetupService.GetTestTrainingExercises(1,1,190),
                ExerciseSetupService.GetTestTrainingExercises(10,10,50),
            });
            exerciseList = ExerciseSetupService.GetTestTrainingExercises(5, 5, 100);
        }

        [Test]
        public void ShouldReturnOnlyBests()
        {

            var records = _ExerciseReader.GetExerciseBest(trainingList, "Bench Press");
            Console.WriteLine(records);

            Assert.AreEqual(records[0].Sets, 1);
            Assert.AreEqual(records[0].Reps, 1);
            Assert.AreEqual(records[0].Weight, 200);
            Assert.AreEqual(records[1].Weight, 140);
            Assert.AreEqual(records[2].Weight, 50);
        }

        [Test]
        public void ShouldGetAllTrainingsFromList()
        {
            var exercises = _ExerciseReader.GetFromTrainingList(trainingList);

            //Amout of exercises is equal to 7 exercies times 6 trainings
            Assert.AreEqual(exercises.Count, 6 * 7);
            Assert.AreEqual(exercises[7], trainingList[1].Exercises[0]);
        }

        [Test]
        public void ShouldGetAllExercisesOfTheSameType()
        {
            var exerciseByType = _ExerciseReader.GetByType(exerciseList, "VPush");
            //Has only 1 instance of vertical pushing in a single session
            Assert.AreEqual(exerciseByType.Count, 1);
        }

        [Test]
        public void ShouldGetAllExercisesOfTheSameTypeFromAllTrainings()
        {
            var exercises = _ExerciseReader.GetFromTrainingList(trainingList);
            var VerticalPushExercises = _ExerciseReader.GetByType(exercises, "VPush");

            //Amout of vertical push exercies equal to 1 per training
            Assert.AreEqual(VerticalPushExercises.Count, 6);
            //No exercises of a different type
            Assert.AreEqual(VerticalPushExercises.Where(e => e.Type != "VPush").ToList().Count, 0);
        }

        [Test]
        public void ShouldGetAllTheRecordsOfExercise()
        {
            var exercies = _ExerciseReader.GetExerciseHistory(trainingList, "Bench Press");

            //Amount of bench presses is equal to the amout of training sessions
            Assert.AreEqual(exercies.Count, 6);
            //No different exercies than bench press
            Assert.AreEqual(exercies.Where(e => e.Name != "Bench Press").ToList().Count, 0);
        }
    }
}