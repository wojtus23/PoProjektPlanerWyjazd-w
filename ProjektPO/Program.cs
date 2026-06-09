using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Konsola konsola = new Konsola();
        PlanPodrozy? plan = null;

        while (true)
        {
            Konsola.WyswietlMenu();
            int wybor = konsola.PobierzWybor();

            try
            {
                switch (wybor)
                {
                    case 1:
                        Konsola.utworzPlanPodrozy(konsola, out plan);
                        break;
                    case 2:
                        if (plan == null)
                        {
                            Console.WriteLine("Najpierw utwórz plan podróży.");
                        }
                        else
                        {
                            Konsola.dodajPunkt(konsola, plan);
                        }
                        break;
                    case 3:
                        if (plan == null)
                        {
                            Console.WriteLine("Najpierw utwórz plan podróży.");
                        }
                        else
                        {
                            Konsola.usunPunkt(konsola, plan);
                        }
                        break;
                    case 4:
                        if (plan == null)
                        {
                            Console.WriteLine("Najpierw utwórz plan podróży.");
                        }
                        else
                        {
                            Konsola.dodajUczestnika(konsola, plan);
                        }
                        break;
                    case 5:
                        if (plan == null)
                        {
                            Console.WriteLine("Najpierw utwórz plan podróży.");
                        }
                        else
                        {
                            Konsola.generujPodsumowanie(plan);
                        }
                        break;
                    case 6:
                        Console.WriteLine("Do widzenia!");
                        return;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Proszę spróbować ponownie.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ Błąd: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}
