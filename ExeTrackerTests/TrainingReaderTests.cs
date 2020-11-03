using ExeTrackerLogic.Models;
using ExeTrackerLogic.TrainingReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExeTrackerTests
{
    [TestFixture]
    class TrainingReaderTests
    {
        private List<TrainingModel> trainingList;

        [SetUp]
        public void Setup() {
            trainingList = TrainingSetupService.GetTestTrainingList();
        }

        [Test]
        public void GetsByDay() 
        {

            //Assert there are 2 trainings today as it was specified setup in service
            Assert.AreEqual(_TrainingReader.GetByDay(trainingList, DateTime.Now).Count, 2);
            Assert.AreEqual(_TrainingReader.GetByDay(trainingList, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Count, 2);
            //Assert there are no days returned when asked a day which wasn't setup
            Assert.AreEqual(_TrainingReader.GetByDay(trainingList, DateTime.Now.AddDays(20)).Count, 0);
        }

        [Test]
        public void GetsByMonth()
        {
            var trainingsThisMonth = _TrainingReader.GetByMonth(trainingList, DateTime.Now.Year, DateTime.Now.Month).Count;
            var trainingsNextMonth = _TrainingReader.GetByMonth(trainingList, DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month).Count;
            //Assert there are only 2 training in first month as in setup
            Assert.AreEqual(trainingsThisMonth, 2);
            //Assert other month has only 1 training
            Assert.AreEqual(trainingsNextMonth, 1);
        }

        [Test]
        public void GetsByYear() 
        {
            var trainingsThisYear = _TrainingReader.GetByYear(trainingList, DateTime.Now.Year).Count;
            var trainingsNextYear = _TrainingReader.GetByYear(trainingList, DateTime.Now.AddYears(1).Year).Count;

            //Assert there are as many trainings left this year as months (including current one) plus 1 for adittional in current month
            Assert.AreEqual(trainingsThisYear, 12 - DateTime.Now.Month + 1 + 1);
            //Assert that next training year has a proper amount of trainings
            Assert.AreEqual(trainingsNextYear, DateTime.Now.Month >=3 ? 12 : DateTime.Now.Month + 5);
        }
    }
}
