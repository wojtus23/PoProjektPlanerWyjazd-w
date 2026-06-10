using System;
using System.Collections.Generic;

/// @brief Główna klasa programu, pełniąca rolę punktu wejściowego aplikacji.
public class Program
{
    /// @brief Metoda startowa (Main) sterująca pętlą menu.
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Konsola konsola = new Konsola();
        PlanPodrozy? plan = null;
        
        List<Uczestnik> bazaUczestnikow = new List<Uczestnik>(); 

        while (true)
        {
            Konsola.WyswietlMenu();

            try
            {
                int wybor = konsola.PobierzWybor();

                switch (wybor)
                {
                    case 1:
                        if (plan != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n--- UWAGA: Istnieje już aktywny plan podróży! ---");
                            Console.WriteLine("Oto podsumowanie zamykanego planu:\n");
                            Console.ResetColor();
                            
                            Konsola.generujPodsumowanie(plan);
                            
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n--- Rozpoczynanie tworzenia NOWEGO planu (poprzedni zostanie nadpisany) ---");
                            Console.ResetColor();
                        }
                        Konsola.utworzPlanPodrozy(konsola, out plan);
                        break;
                    case 2:
                        if (plan == null) Console.WriteLine("Najpierw utwórz plan podróży.");
                        else Konsola.dodajPunkt(konsola, plan);
                        break;
                    case 3:
                        if (plan == null) Console.WriteLine("Najpierw utwórz plan podróży.");
                        else Konsola.usunPunkt(konsola, plan);
                        break;
                    case 4:
                        Uczestnik nowyUczestnik = Konsola.stworzUczestnika(konsola);
                        bazaUczestnikow.Add(nowyUczestnik);
                        break;
                    case 5:
                        if (plan == null) Console.WriteLine("Najpierw utwórz plan podróży, aby dodać do niego uczestników.");
                        else Konsola.przypiszUczestnikaDoPlanu(konsola, plan, bazaUczestnikow);
                        break;
                    case 6:
                        if (plan == null) Console.WriteLine("Najpierw utwórz plan podróży.");
                        else Konsola.generujPodsumowanie(plan);
                        break;
                    case 7:
                        Console.WriteLine("Do widzenia!");
                        return;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Proszę spróbować ponownie.");
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n➜ Operacja anulowana. Powrót do menu głównego.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ Błąd: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}