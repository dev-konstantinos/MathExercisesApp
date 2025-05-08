using System;
using System.Collections.Generic;

namespace MainApp
{
    // Class is responsible for storing the results of exercises
    internal class StorageManager
    {
        private readonly int maxExercises = 1000; // Maximum number of exercises to store in memory
        private readonly List<Exercise> exercises = new List<Exercise>();

        public void AddExercise(Exercise exercise)
        {
            if (exercises.Count >= maxExercises)
            {
                exercises.RemoveAt(0);
            }
            exercises.Add(exercise);
        }
        public void ShowResults()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Ergebniszusammenfassung:");
            Console.WriteLine($"Gesamtzahl der Übungen: {GetTotalExercises()}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Richtig beantwortet: {GetCorrectExercises()}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falsch beantwortet: {GetIncorrectExercises()}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Noch nicht beantwortet: {GetUnansweredExercises()}");
            Console.WriteLine($"Prozentualer Erfolg: {Math.Round((double)GetCorrectExercises() / GetTotalExercises() * 100, 2)}%");
            Console.ResetColor();
            Thread.Sleep(1000);

            Console.WriteLine("\n+-------------------+-----------+-----------+-------------+");
            Console.WriteLine("| Aufgabe           | Ergebnis  | Antwort   | Status      |");
            Console.WriteLine("+-------------------+-----------+-----------+-------------+");

            foreach (var exercise in exercises)
            {
                string aufgabe = $"{exercise.FirstTerm} {exercise.Operator} {exercise.SecondTerm}";
                string ergebnis = $"{exercise.Result}";
                string ihreAntwort = exercise.UserAnswer.HasValue ? $"{exercise.UserAnswer}" : "-";
                string status = exercise.UserAnswer.HasValue
                    ? (exercise.IsCorrect ? "Richtig" : "Falsch")
                    : "K/Antw";

                Console.WriteLine($"| {aufgabe,-17} | {ergebnis,-9} | {ihreAntwort,-9} | {status,-11} |");
            }

            Console.WriteLine("+-------------------+-----------+-----------+-------------+");

        }

        public int GetTotalExercises()
        {
            return exercises.Count;
        }

        public int GetCorrectExercises()
        {
            return exercises.Count(e => e.UserAnswer.HasValue && e.IsCorrect);
        }

        public int GetIncorrectExercises()
        {
            return exercises.Count(e => e.UserAnswer.HasValue && !e.IsCorrect);
        }

        public int GetUnansweredExercises()
        {
            return exercises.Count(e => !e.UserAnswer.HasValue);
        }
    }
}
