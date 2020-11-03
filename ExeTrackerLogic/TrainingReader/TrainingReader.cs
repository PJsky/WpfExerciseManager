using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExeTrackerLogic.TrainingReader
{
    public static class _TrainingReader
    {
        public static List<TrainingModel> GetByYear(List<TrainingModel> trainingList, int year)
        {
            return trainingList.Where(t => t.TrainingDay.Year == year).ToList();
        }

        public static List<TrainingModel> GetByMonth(List<TrainingModel> trainingList, int year, int month)
        {
            return trainingList.Where(t => t.TrainingDay.Year == year 
                                      && t.TrainingDay.Month == month)
                                      .ToList();
        }

        public static List<TrainingModel> GetByDay(List<TrainingModel> trainingList, int year, int month, int day)
        {
            return trainingList.Where(t => t.TrainingDay.Year == year
                                      && t.TrainingDay.Month == month
                                      && t.TrainingDay.Day == day)
                                      .ToList();
        }

        public static List<TrainingModel> GetByDay(List<TrainingModel> trainingList, DateTime trainingDay)
        {
            return trainingList.Where(t => t.TrainingDay.Year == trainingDay.Year
                                      && t.TrainingDay.Month == trainingDay.Month
                                      && t.TrainingDay.Day == trainingDay.Day)
                                      .ToList();
        }
    }
}
