using System;

namespace MainApp
{
    // Class is responsible for the presentation layer of the application (console menu, user interaction)
    internal class PresentationLayer
    {
        public static void ConsoleMenu()
        {
            Console.Title = "Mathematikübungen";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Willkommen in der Welt der Mathematik!\n");

            var manager = new ExerciseManager();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nWählen Sie einen Übungstyp:\n");
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Subtraktion");
                Console.WriteLine("3. Multiplikation");
                Console.WriteLine("4. Division");
                Console.WriteLine("5. Zufällige Übung");
                Console.WriteLine("6. Ergebnisse anzeigen");
                Console.WriteLine("7. Beenden\n");

                Console.ResetColor();
                Console.Write("Ihre Wahl: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Bitte geben Sie eine gültige Zahl ein.");
                    continue;
                }

                if (choice == 7)
                    break;

                // Create an exercise based on the user's choice
                Exercise? exercise = null;
                switch (choice)
                {
                    case 1:
                        exercise = manager.CreateAndStoreAdditionExercise();
                        break;
                    case 2:
                        exercise = manager.CreateAndStoreSubtractionExercise();
                        break;
                    case 3:
                        exercise = manager.CreateAndStoreMultiplicationExercise();
                        break;
                    case 4:
                        exercise = manager.CreateAndStoreDivisionExercise();
                        break;
                    case 5:
                        exercise = manager.CreateAndStoreRandomExercise();
                        break;
                    case 6:
                        manager.ShowResults();
                        continue;
                    default:                     
                        Console.WriteLine("Ungültige Wahl...");
                        continue;
                }

                if (exercise == null)
                {
                    Console.WriteLine("Die Erstellung der Übung ist fehlgeschlagen...");
                    continue;
                }

                // Loop to continue generating exercises until user presses 'Q'
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"{exercise.FirstTerm} {exercise.Operator} {exercise.SecondTerm} = ");
                    if (!int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        Console.WriteLine("Ungültige Eingabe. Diese Übung wird übersprungen.");
                        break;
                    }

                    if (manager.CheckAnswer(exercise, userInput))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Richtig!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Falsch! Die richtige Antwort: {exercise.Result}");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Noch einmal? ('Q' zum Beenden, oder eine andere Taste für eine neue Übung)");
                    Console.ResetColor();
                    var userChoice = Console.ReadKey(true).Key;
                    if (userChoice == ConsoleKey.Q)
                    {
                        break;
                    }

                    // If the user presses any other key, create a new exercise of the same type
                    switch (choice)
                    {
                        case 1:
                            exercise = manager.CreateAndStoreAdditionExercise();
                            break;
                        case 2:
                            exercise = manager.CreateAndStoreSubtractionExercise();
                            break;
                        case 3:
                            exercise = manager.CreateAndStoreMultiplicationExercise();
                            break;
                        case 4:
                            exercise = manager.CreateAndStoreDivisionExercise();
                            break;
                        case 5:
                            exercise = manager.CreateAndStoreRandomExercise();
                            break;
                    }
                }
            }
            Console.WriteLine("Danke fürs Spielen! Auf Wiedersehen.");
        }
    }
}
