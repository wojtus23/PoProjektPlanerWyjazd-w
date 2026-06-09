using System;
using System.Collections.Generic;
using System.Linq;

public class Konsola
{
    public int PobierzWybor()
    {
        while (true)
        {
            Console.Write("Wybierz opcję: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int wybor))
            {
                return wybor;
            }
            Console.WriteLine("Niepoprawny wybór. Proszę wprowadzić liczbę.");
        }
    }

    public string PobierzTekst(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("Nie można wprowadzić pustego tekstu. Proszę spróbować ponownie.");
        }
    }

    public DateTime PobierzDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime data))
            {
                if (data < DateTime.Now.Date)
                {
                    Console.WriteLine("Data nie może być wcześniejsza niż dzisiaj. Proszę wprowadzić poprawną datę.");
                    continue;
                }
                return data;
            }
            Console.WriteLine("Niepoprawny format daty. Proszę wprowadzić datę w formacie dd.MM.yyyy.");
        }
    }

    public static void WyswietlMenu()
    {
        Console.WriteLine("=== Plan Podróży ===");
        Console.WriteLine("1. Utwórz nowy plan podróży");
        Console.WriteLine("2. Dodaj punkt harmonogramu");
        Console.WriteLine("3. Usuń punkt harmonogramu");
        Console.WriteLine("4. Dodaj uczestnika");
        Console.WriteLine("5. Generuj podsumowanie");
        Console.WriteLine("6. Wyjdź");
    }

    public static void utworzPlanPodrozy(Konsola konsola, out PlanPodrozy plan)
    {
        string tytul = konsola.PobierzTekst("Podaj tytuł podróży: ");
        DateTime dataRozpoczecia = konsola.PobierzDate("Podaj datę rozpoczęcia (dd.MM.yyyy): ");
        DateTime dataZakonczenia = konsola.PobierzDate("Podaj datę zakończenia (dd.MM.yyyy): ");
        double budzet = 0;
        while (true)
        {
            Console.Write("Podaj budżet całkowity: ");
            string? input = Console.ReadLine();
            if (double.TryParse(input, out budzet) && budzet >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny budżet. Proszę wprowadzić nieujemną liczbę.");
        }
        plan = new PlanPodrozy(tytul, dataRozpoczecia, dataZakonczenia, budzet);
        Console.WriteLine("✓ Plan podróży utworzony pomyślnie!");
    }

    public static void dodajUczestnika(Konsola konsola, PlanPodrozy plan)
    {
        string imie = konsola.PobierzTekst("Podaj imię uczestnika: ");
        string nazwisko = konsola.PobierzTekst("Podaj nazwisko uczestnika: ");
        int wiek;
        while (true)
        {
            Console.Write("Podaj wiek uczestnika: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out wiek) && wiek >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny wiek. Proszę wprowadzić nieujemną liczbę.");
        }
        Uczestnik uczestnik = new Uczestnik(imie, nazwisko, wiek);
        plan.DodajUczestnika(uczestnik);
        Console.WriteLine("✓ Uczestnik dodany pomyślnie!");
    }

    public static void dodajPunkt(Konsola konsola, PlanPodrozy plan)
    {
        Console.WriteLine("Wybierz typ punktu harmonogramu:");
        Console.WriteLine("0. Atrakcja");
        Console.WriteLine("1. Zakwaterowanie");
        Console.WriteLine("2. Transport publiczny");
        Console.WriteLine("3. Transport prywatny");

        TypPunktu typPunktu;
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int wybor) && Enum.IsDefined(typeof(TypPunktu), wybor))
            {
                typPunktu = (TypPunktu)wybor;
                break;
            }
            Console.WriteLine("Niepoprawny wybór. Proszę wpisać 0, 1, 2 lub 3.");
        }

        string nazwa = konsola.PobierzTekst("Podaj nazwę punktu: ");
        DateTime czasStart = konsola.PobierzDate("Podaj czas rozpoczęcia (dd.MM.yyyy HH:mm): ");
        DateTime czasKoniec = konsola.PobierzDate("Podaj czas zakończenia (dd.MM.yyyy HH:mm): ");
        double szacowanyKoszt;
        while (true)
        {
            Console.Write("Podaj szacowany koszt: ");
            string? input = Console.ReadLine();
            if (double.TryParse(input, out szacowanyKoszt) && szacowanyKoszt >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny koszt. Proszę wprowadzić nieujemną liczbę.");
        }

        PunktHarmonogramu? punkt = null;

        switch (typPunktu)
        {
            case TypPunktu.Atrakcja:
                string typAtrakcji = konsola.PobierzTekst("Podaj typ atrakcji: ");
                string krotkiOpis = konsola.PobierzTekst("Podaj krótki opis atrakcji: ");
                punkt = new Atrakcja(nazwa, czasStart, czasKoniec, szacowanyKoszt, typAtrakcji, krotkiOpis);
                break;
            case TypPunktu.Zakwaterowanie:
                string nazwaObiektu = konsola.PobierzTekst("Podaj nazwę obiektu zakwaterowania: ");
                string adres = konsola.PobierzTekst("Podaj adres zakwaterowania: ");
                bool wliczoneSniadanie = false;
                while (true)
                {
                    Console.Write("Czy śniadanie jest wliczone? (tak/nie): ");
                    string? input = Console.ReadLine()?.Trim().ToLower();
                    if (input == "tak") { wliczoneSniadanie = true; break; }
                    else if (input == "nie") { wliczoneSniadanie = false; break; }
                    Console.WriteLine("Niepoprawna odpowiedź. Proszę wpisać 'tak' lub 'nie'.");
                }
                punkt = new Zakwaterowanie(nazwa, czasStart, czasKoniec, szacowanyKoszt, nazwaObiektu, adres, wliczoneSniadanie);
                break;
            case TypPunktu.TransportPubliczny:
                string srodekTransportu = konsola.PobierzTekst("Podaj środek transportu: ");
                string miejsceOdjazdu = konsola.PobierzTekst("Podaj miejsce odjazdu: ");
                string miejscePrzyjazdu = konsola.PobierzTekst("Podaj miejsce przyjazdu: ");
                string numerLinii = konsola.PobierzTekst("Podaj numer linii: ");
                Console.WriteLine("Wybierz rodzaj biletu (0 - Normalny, 1 - Ulgowy): ");
                RodzajBiletu rodzajBiletu;
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out int wybor) && Enum.IsDefined(typeof(RodzajBiletu), wybor))
                    {
                        rodzajBiletu = (RodzajBiletu)wybor;
                        break;
                    }
                    Console.WriteLine("Niepoprawny wybór. Proszę wpisać 0 lub 1.");
                }
                punkt = new TransportPubliczny(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu, numerLinii, rodzajBiletu);
                break;
            case TypPunktu.TransportPrywatny:
                string srodekTransportuPrywatny = konsola.PobierzTekst("Podaj środek transportu: ");
                string miejsceOdjazduPrywatny = konsola.PobierzTekst("Podaj miejsce odjazdu: ");
                string miejscePrzyjazduPrywatny = konsola.PobierzTekst("Podaj miejsce przyjazdu: ");
                double kosztPaliwa;
                while (true)
                {
                    Console.Write("Podaj koszt paliwa: ");
                    string? input = Console.ReadLine();
                    if (double.TryParse(input, out kosztPaliwa) && kosztPaliwa >= 0)
                    {
                        break;
                    }
                    Console.WriteLine("Niepoprawny koszt. Proszę wprowadzić nieujemną liczbę.");
                }
                punkt = new TransportPrywatny(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportuPrywatny, miejsceOdjazduPrywatny, miejscePrzyjazduPrywatny);
                break;
        }

        if (punkt != null)
        {
            plan.DodajPunkt(punkt);
            Console.WriteLine("✓ Punkt dodany do harmonogramu pomyślnie!");

            if (punkt is IWymagaRezerwacji rezerwacja)
            {
                Console.Write("Czy chcesz od razu zarezerwować? (tak/nie): ");
                string? odp = Console.ReadLine()?.Trim().ToLower();
                if (odp == "tak") rezerwacja.WykonajRezerwacje();
            }
        }
    }

    public static void usunPunkt(Konsola konsola, PlanPodrozy plan)
    {
        Console.Write("Podaj identyfikator punktu do usunięcia: ");
        int id;
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out id) && id >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny identyfikator. Proszę wprowadzić nieujemną liczbę.");
        }
        plan.UsunPunkt(id-1); 
        Console.WriteLine("✓ Punkt usunięty pomyślnie!");
    }

    public static void generujPodsumowanie(PlanPodrozy plan)
    {
        plan.GenerujPodsumowanie();
    }
}