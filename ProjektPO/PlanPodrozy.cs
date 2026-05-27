using System;
using System.Collections.Generic;
using System.Linq;

public class PlanPodrozy
{
    private string tytulPodrozy;
    private DateTime dataRozpoczecia;
    private DateTime dataZakonczenia; 
    private double budzetCalkowity;
    private List<PunktHarmonogramu> listaPunktow;
    private List<Uczestnik> listaUczestnikow;

    public PlanPodrozy(string tytulPodrozy, DateTime dataRozpoczecia, DateTime dataZakonczenia, double budzetCalkowity)
    {
        if (string.IsNullOrWhiteSpace(tytulPodrozy))
            throw new ArgumentException("Tytuł podróży nie może być pusty.", nameof(tytulPodrozy));
            
        if (dataZakonczenia < dataRozpoczecia)
            throw new ArgumentException("Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.");
            
        if (budzetCalkowity < 0)
            throw new ArgumentException("Budżet całkowity nie może być ujemny.");

        this.tytulPodrozy = tytulPodrozy;
        this.dataRozpoczecia = dataRozpoczecia;
        this.dataZakonczenia = dataZakonczenia;
        this.budzetCalkowity = budzetCalkowity;
        this.listaPunktow = new List<PunktHarmonogramu>();
        this.listaUczestnikow = new List<Uczestnik>();
    }

    public void DodajPunkt(PunktHarmonogramu punkt)
    {
        if (punkt == null)
            throw new ArgumentNullException(nameof(punkt), "Punkt harmonogramu nie może być pusty.");

        foreach (var istniejacyPunkt in listaPunktow)
        {
            if (istniejacyPunkt.CzyKoliduje(punkt))
            {
                throw new KolizjaTerminowException(istniejacyPunkt, 
                    "Wykryto kolizję! Nowy punkt nakłada się czasowo z istniejącym zdarzeniem.");
            }
        }

        listaPunktow.Add(punkt);
        listaPunktow.Sort();
    }

    public void UsunPunkt(int id)
    {
        if (id < 0 || id >= listaPunktow.Count)
            throw new ArgumentOutOfRangeException(nameof(id), "Nie znaleziono punktu o podanym identyfikatorze.");

        listaPunktow.RemoveAt(id);
    }

    public double ObliczKosztCalkowity()
    {
        return listaPunktow.Sum(p => p.SzacowanyKoszt);
    }

    public void GenerujPodsumowanie()
    {
        double aktualnyKoszt = ObliczKosztCalkowity();
        double saldo = budzetCalkowity - aktualnyKoszt;

        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"[ PLAN PODRÓŻY: {tytulPodrozy.ToUpper()} ]");
        Console.WriteLine($"Termin: {dataRozpoczecia:dd.MM.yyyy} - {dataZakonczenia:dd.MM.yyyy}");
        Console.WriteLine($"Budżet: {budzetCalkowity:C} | Koszt: {aktualnyKoszt:C}");
        
        if (saldo < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"UWAGA: Przekroczono budżet o {Math.Abs(saldo):C}!");
            Console.ResetColor();
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"UCZESTNICY ({listaUczestnikow.Count}):");
        foreach (var uczestnik in listaUczestnikow)
        {
            Console.WriteLine($"   - {uczestnik.PobierzPelneDane()}");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"HARMONOGRAM ({listaPunktow.Count} zdarzeń):");
        
        if (listaPunktow.Count == 0)
        {
            Console.WriteLine("   Brak zaplanowanych punktów.");
        }
        else
        {
            foreach (var punkt in listaPunktow)
            {
                punkt.PokazSzczegoly(); 
            }
        }
        Console.WriteLine(new string('=', 60));
    }

    public void DodajUczestnika(Uczestnik uczestnik)
    {
        if (uczestnik == null)
            throw new ArgumentNullException(nameof(uczestnik));
            
        listaUczestnikow.Add(uczestnik);
    }
}






