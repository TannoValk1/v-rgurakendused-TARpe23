using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; 

namespace võrgurakendused.model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {  
        }
        public DbSet<Exercise>? ExerciseList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Title = "Kätekõverdused jala tõgtega",
                    Description = "Kätekõverdused korda mööda jalga tõstes",
                    Intensity = Exercise.ExerciseIntensity.Normal,
                    RecommendedDurationInSeconds = 40,
                    RecommendedTimeInSecondsBeforeExercise = 10,
                    RecommendedTimeInSecondsAfterExercise = 10
                },
                new Exercise
                {
                    Id = 2,
                    Title = "Slaalomhüpped",
                    Description = "Kükist hüpped küljelt küljele",
                    Intensity = Exercise.ExerciseIntensity.High,
                    RecommendedDurationInSeconds = 40,
                    RecommendedTimeInSecondsBeforeExercise = 10,
                    RecommendedTimeInSecondsAfterExercise = 10,
                    RestTimeInstructions = "Venita reie esikülge"
                },
                new Exercise
                {
                    Id = 3,
                    Title = "Alt läbi jooks",
                    Description = "Toenglamangus jooksmine",
                    Intensity = Exercise.ExerciseIntensity.Normal,
                    RecommendedDurationInSeconds = 40,
                    RecommendedTimeInSecondsBeforeExercise = 10,
                    RecommendedTimeInSecondsAfterExercise = 10
                }
            );
        }
    }
}
