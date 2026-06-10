using System;
using System.Collections.Generic;
using System.Linq;

/// @brief Główna klasa zarządzająca planem podróży, uczestnikami i budżetem.
public class PlanPodrozy
{
    private string tytulPodrozy;
    private DateTime dataRozpoczecia;
    private DateTime dataZakonczenia; 
    private double budzetCalkowity;
    private List<PunktHarmonogramu> listaPunktow;
    private List<Uczestnik> listaUczestnikow;

    /// @brief Inicjalizuje nowy plan podróży.
    /// @param tytulPodrozy Tytuł wyjazdu.
    /// @param dataRozpoczecia Data startu podróży.
    /// @param dataZakonczenia Data zakończenia podróży.
    /// @param budzetCalkowity Maksymalny zaplanowany budżet.
    /// @throw ArgumentException Rzucany, gdy tytuł jest pusty, daty są nielogiczne lub budżet jest ujemny.
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

    /// @brief Dodaje nowy punkt do harmonogramu podróży.
    /// @param punkt Punkt harmonogramu do dodania.
    /// @throw ArgumentNullException Rzucany, gdy przekazany punkt jest nullem.
    /// @throw KolizjaTerminowException Rzucany, gdy punkt nakłada się czasowo z innym wydarzeniem.
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

    /// @brief Usuwa punkt z harmonogramu na podstawie jego indeksu.
    /// @param id Identyfikator (indeks) punktu do usunięcia.
    /// @throw ArgumentOutOfRangeException Rzucany, gdy indeks jest poza zakresem listy.
    public void UsunPunkt(int id)
    {
        if (id < 0 || id >= listaPunktow.Count)
            throw new ArgumentOutOfRangeException(nameof(id), "Nie znaleziono punktu o podanym identyfikatorze.");

        listaPunktow.RemoveAt(id);
    }

    /// @brief Oblicza sumaryczny koszt wszystkich zaplanowanych punktów harmonogramu.
    /// @return Całkowity dotychczasowy koszt.
    public double ObliczKosztCalkowity()
    {
        return listaPunktow.Sum(p => p.SzacowanyKoszt);
    }

    /// @brief Generuje i wyświetla podsumowanie planu podróży na konsoli.
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

    /// @brief Dodaje nowego uczestnika wycieczki.
    /// @param uczestnik Obiekt uczestnika do dodania.
    /// @throw ArgumentNullException Rzucany, gdy przekazany uczestnik jest nullem.
    public void DodajUczestnika(Uczestnik uczestnik)
    {
        if (uczestnik == null)
            throw new ArgumentNullException(nameof(uczestnik));
            
        listaUczestnikow.Add(uczestnik);
    }
}