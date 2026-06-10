using System;
using System.Collections.Generic;

/// @brief Klasa narzędziowa odpowiadająca za interakcję z użytkownikiem w konsoli.
public class Konsola
{
    /// @brief Własna metoda wczytująca tekst, pozwalająca na anulowanie klawiszem ESC.
    /// @return Zwraca wpisany ciąg znaków, jeśli wciśnięto Enter.
    /// @throw OperationCanceledException Rzucany, gdy wciśnięto klawisz ESC.
    public string OdczytajLinieZAnulowaniem()
    {
        string input = "";
        while (true)
        {
            var klawisz = Console.ReadKey(intercept: true);

            if (klawisz.Key == ConsoleKey.Escape)
            {
                Console.WriteLine(); 
                throw new OperationCanceledException("Operacja została przerwana przez użytkownika.");
            }
            else if (klawisz.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input;
            }
            else if (klawisz.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(klawisz.KeyChar))
            {
                input += klawisz.KeyChar;
                Console.Write(klawisz.KeyChar);
            }
        }
    }

    /// @brief Pobiera numeryczny wybór od użytkownika z walidacją danych.
    /// @return Zwraca liczbę całkowitą wybraną przez użytkownika.
    public int PobierzWybor()
    {
        while (true)
        {
            Console.Write("Wybierz opcję: ");
            string input = OdczytajLinieZAnulowaniem();
            if (int.TryParse(input, out int wybor))
            {
                return wybor;
            }
            Console.WriteLine("Niepoprawny wybór. Proszę wprowadzić liczbę.");
        }
    }

    /// @brief Wymusza od użytkownika wprowadzenie niepustego ciągu znaków.
    /// @param prompt Komunikat wyświetlany użytkownikowi.
    /// @return Zwraca wprowadzony ciąg znaków.
    public string PobierzTekst(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = OdczytajLinieZAnulowaniem();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("Nie można wprowadzić pustego tekstu. Proszę spróbować ponownie.");
        }
    }

    /// @brief Odczytuje datę w formacie daty i czasu, pilnując by nie dotyczyła przeszłości.
    /// @param prompt Komunikat wyświetlany użytkownikowi.
    /// @param dateFormat Oczekiwany format daty (np. "dd.MM.yyyy" lub "dd.MM.yyyy HH:mm").
    /// @return Poprawnie sformatowana data.
    public DateTime PobierzDate(string prompt, string dateFormat)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = OdczytajLinieZAnulowaniem();
            if (DateTime.TryParseExact(input, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime data))
            {
                if (data < DateTime.Now.Date)
                {
                    Console.WriteLine("Data nie może być wcześniejsza niż dzisiaj. Proszę wprowadzić poprawną datę.");
                    continue;
                }
                return data;
            }
            Console.WriteLine("Niepoprawny format daty. Proszę spróbować ponownie.");
        }
    }

    /// @brief Wyświetla główne menu aplikacji w oknie konsoli.
    public static void WyswietlMenu()
    {
        Console.WriteLine("=== Plan Podróży (Wciśnij ESC w dowolnym momencie, by anulować akcję) ===");
        Console.WriteLine("1. Utwórz nowy plan podróży");
        Console.WriteLine("2. Dodaj punkt harmonogramu");
        Console.WriteLine("3. Usuń punkt harmonogramu");
        Console.WriteLine("4. Utwórz nowego uczestnika (Baza)");
        Console.WriteLine("5. Przypisz uczestnika z bazy do planu podróży");
        Console.WriteLine("6. Generuj podsumowanie planu podróży");
        Console.WriteLine("7. Wyjdź");
    }

    /// @brief Przeprowadza przez proces tworzenia nowego planu podróży.
    /// @param konsola Instancja konsoli do pobierania danych.
    /// @param plan Nowo utworzony obiekt PlanPodrozy (parametr wyjściowy).
    public static void utworzPlanPodrozy(Konsola konsola, out PlanPodrozy plan)
    {
        string tytul = konsola.PobierzTekst("Podaj tytuł podróży: ");
        DateTime dataRozpoczecia = konsola.PobierzDate("Podaj datę rozpoczęcia (dd.MM.yyyy): ", "dd.MM.yyyy");
        DateTime dataZakonczenia = konsola.PobierzDate("Podaj datę zakończenia (dd.MM.yyyy): ", "dd.MM.yyyy");
        double budzet = 0;
        while (true)
        {
            Console.Write("Podaj budżet całkowity: ");
            string input = konsola.OdczytajLinieZAnulowaniem();
            if (double.TryParse(input, out budzet) && budzet >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny budżet. Proszę wprowadzić nieujemną liczbę.");
        }
        plan = new PlanPodrozy(tytul, dataRozpoczecia, dataZakonczenia, budzet);
        Console.WriteLine("✓ Plan podróży utworzony pomyślnie!");
    }

    /// @brief Tworzy nowego uczestnika i go zwraca.
    /// @param konsola Instancja konsoli do pobierania danych.
    /// @return Zwraca nowo utworzony obiekt uczestnika.
    public static Uczestnik stworzUczestnika(Konsola konsola)
    {
        string imie = konsola.PobierzTekst("Podaj imię uczestnika: ");
        string nazwisko = konsola.PobierzTekst("Podaj nazwisko uczestnika: ");
        int wiek;
        while (true)
        {
            Console.Write("Podaj wiek uczestnika: ");
            string input = konsola.OdczytajLinieZAnulowaniem();
            if (int.TryParse(input, out wiek) && wiek >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny wiek. Proszę wprowadzić nieujemną liczbę.");
        }
        
        Uczestnik uczestnik = new Uczestnik(imie, nazwisko, wiek);
        Console.WriteLine("✓ Uczestnik utworzony i dodany do bazy pomyślnie!");
        return uczestnik;
    }

    /// @brief Przypisuje istniejącego uczestnika z bazy do konkretnego planu.
    /// @param konsola Instancja konsoli do pobierania danych.
    /// @param plan Aktualny plan podróży.
    /// @param baza Lista wszystkich uczestników.
    public static void przypiszUczestnikaDoPlanu(Konsola konsola, PlanPodrozy plan, List<Uczestnik> baza)
    {
        if (baza.Count == 0)
        {
            Console.WriteLine("Baza uczestników jest pusta. Najpierw utwórz uczestnika (Opcja 4).");
            return;
        }

        Console.WriteLine("Dostępni uczestnicy w bazie:");
        for (int i = 0; i < baza.Count; i++)
        {
            Console.WriteLine($"[{i}] {baza[i].PobierzPelneDane()}");
        }

        Console.Write("Podaj numer uczestnika do dodania do planu: ");
        string input = konsola.OdczytajLinieZAnulowaniem();
        
        if (int.TryParse(input, out int indeks) && indeks >= 0 && indeks < baza.Count)
        {
            plan.DodajUczestnika(baza[indeks]);
            Console.WriteLine("✓ Uczestnik pomyślnie przypisany do planu podróży!");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer uczestnika.");
        }
    }

    /// @brief Obsługuje proces dodawania punktu harmonogramu.
    /// @param konsola Instancja konsoli do pobierania danych.
    /// @param plan Aktualny plan podróży.
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
            Console.Write("Wybór typu: ");
            string input = konsola.OdczytajLinieZAnulowaniem();
            if (int.TryParse(input, out int wybor) && Enum.IsDefined(typeof(TypPunktu), wybor))
            {
                typPunktu = (TypPunktu)wybor;
                break;
            }
            Console.WriteLine("Niepoprawny wybór. Proszę wpisać 0, 1, 2 lub 3.");
        }

        string nazwa = konsola.PobierzTekst("Podaj nazwę punktu: ");
        DateTime czasStart = konsola.PobierzDate("Podaj czas rozpoczęcia (dd.MM.yyyy HH:mm): ", "dd.MM.yyyy HH:mm");
        DateTime czasKoniec = konsola.PobierzDate("Podaj czas zakończenia (dd.MM.yyyy HH:mm): ", "dd.MM.yyyy HH:mm");
        double szacowanyKoszt;
        while (true)
        {
            Console.Write("Podaj szacowany koszt: ");
            string input = konsola.OdczytajLinieZAnulowaniem();
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
                    string input = konsola.OdczytajLinieZAnulowaniem().Trim().ToLower();
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
                    Console.Write("Wybór biletu: ");
                    string input = konsola.OdczytajLinieZAnulowaniem();
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
                string odp = konsola.OdczytajLinieZAnulowaniem().Trim().ToLower();
                if (odp == "tak") rezerwacja.WykonajRezerwacje();
            }
        }
    }

    /// @brief Obsługuje proces usuwania punktu harmonogramu.
    /// @param konsola Instancja konsoli do pobierania danych.
    /// @param plan Aktualny plan podróży.
    public static void usunPunkt(Konsola konsola, PlanPodrozy plan)
    {
        Console.Write("Podaj identyfikator punktu do usunięcia: ");
        int id;
        while (true)
        {
            string input = konsola.OdczytajLinieZAnulowaniem();
            if (int.TryParse(input, out id) && id >= 0)
            {
                break;
            }
            Console.WriteLine("Niepoprawny identyfikator. Proszę wprowadzić nieujemną liczbę.");
        }
        plan.UsunPunkt(id-1); 
        Console.WriteLine("✓ Punkt usunięty pomyślnie!");
    }

    /// @brief Wywołuje generator podsumowania.
    /// @param plan Aktualny plan podróży.
    public static void generujPodsumowanie(PlanPodrozy plan)
    {
        plan.GenerujPodsumowanie();
    }
}