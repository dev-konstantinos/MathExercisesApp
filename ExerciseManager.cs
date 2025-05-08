using System;
using System.Collections.Generic;

namespace MainApp
{
    // Class is responsible for storing the results of exercises
    internal class Exercise
    {
        public char Operator { get; }
        public int FirstTerm { get; }
        public int SecondTerm { get; }
        public int Result => CalculateResult();
        public int? UserAnswer { get; set; } // null, which means the exercise is not answered yet
        public bool IsCorrect => UserAnswer.HasValue && UserAnswer.Value == Result;

        public Exercise(char op, int firstTerm, int secondTerm)
        {
            Operator = op;
            FirstTerm = firstTerm;
            SecondTerm = secondTerm;

        }

        // Method to calculate the result of the exercise
        private int CalculateResult()
        {
            return Operator switch
            {
                '+' => FirstTerm + SecondTerm,
                '-' => FirstTerm - SecondTerm,
                '*' => FirstTerm * SecondTerm,
                '/' => SecondTerm != 0 ? FirstTerm / SecondTerm : throw new DivideByZeroException(),
                _ => throw new InvalidOperationException("Nicht unterstützter Operator...")
            };
        }
    }

    // Method to store the results of exercises
    internal class ExerciseGenerator
    {
        private readonly Randomizer randomizer = new Randomizer();

        // Generation of addition
        public Exercise GenerateAdditionExercise()
        {
            int firstTerm = randomizer.GetRandomNumber(100, 900);
            int maxSecond = 999 - firstTerm;

            while (maxSecond < 100)
            {
                firstTerm = randomizer.GetRandomNumber(100, 900);
                maxSecond = 999 - firstTerm;
            }

            int secondTerm = randomizer.GetRandomNumber(100, maxSecond + 1);

            return new Exercise('+', firstTerm, secondTerm);
        }

        // Generation of subtraction
        public Exercise GenerateSubtractionExercise()
        {
            int firstTerm = randomizer.GetRandomNumber(100, 900);
            int maxSecond = firstTerm - 100;
            while (maxSecond < 100)
            {
                firstTerm = randomizer.GetRandomNumber(100, 900);
                maxSecond = firstTerm - 100;
            }
            int secondTerm = randomizer.GetRandomNumber(100, maxSecond + 1);
            return new Exercise('-', firstTerm, secondTerm);
        }

        // Generation of multiplication
        public Exercise GenerateMultiplicationExercise()
        {
            int firstTerm = randomizer.GetRandomNumber(2, 10);
            int maxSecondTerm = 999 / firstTerm;
            int secondTerm = randomizer.GetRandomNumber(2, maxSecondTerm);
            return new Exercise('*', firstTerm, secondTerm);
        }

        // Generation of division
        public Exercise GenerateDivisionExercise()
        {
            int secondTerm = randomizer.GetRandomNumber(2, 10);
            int quotient = randomizer.GetRandomNumber(1, 999 / secondTerm);
            int firstTerm = secondTerm * quotient;
            return new Exercise('/', firstTerm, secondTerm);
        }

        // Generation of random exercises
        public Exercise GenerateRandomExercise()
        {
            int operation = randomizer.GetRandomNumber(1, 5);
            return operation switch
            {
                1 => GenerateAdditionExercise(),
                2 => GenerateSubtractionExercise(),
                3 => GenerateMultiplicationExercise(),
                4 => GenerateDivisionExercise(),
                _ => throw new InvalidOperationException("Nicht unterstützte zufällige Operation...")
            };
        }
    }

    // Class is responsible for managing the exercises
    internal class ExerciseManager
    {
        StorageManager storage = new StorageManager();

        private readonly ExerciseGenerator generator = new ExerciseGenerator();

        // Generation and storage of addition exercises
        public Exercise CreateAndStoreAdditionExercise()
        {
            var exercise = generator.GenerateAdditionExercise();
            storage.AddExercise(exercise);
            return exercise;
        }

        // Generation and storage of subtraction exercises
        public Exercise CreateAndStoreSubtractionExercise()
        {
            var exercise = generator.GenerateSubtractionExercise();
            storage.AddExercise(exercise);
            return exercise;
        }

        // Generation and storage of multiplication exercises
        public Exercise CreateAndStoreMultiplicationExercise()
        {
            var exercise = generator.GenerateMultiplicationExercise();
            storage.AddExercise(exercise);
            return exercise;
        }

        // Generation and storage of division exercises
        public Exercise CreateAndStoreDivisionExercise()
        {
            var exercise = generator.GenerateDivisionExercise();
            storage.AddExercise(exercise);
            return exercise;
        }

        // Generation and storage of random exercises
        public Exercise CreateAndStoreRandomExercise()
        {
            var exercise = generator.GenerateRandomExercise();
            storage.AddExercise(exercise);
            return exercise;
        }

        public bool CheckAnswer(Exercise exercise, int userAnswer)
        {
            exercise.UserAnswer = userAnswer;
            return exercise.IsCorrect;
        }

        // Method to show the results of exercises
        public void ShowResults()
        {
            storage.ShowResults();
        }
    }

    // Generator class for random numbers, used in all exercise generators
    public class Randomizer
    {
        private static readonly Random random = new Random();

        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }

}

