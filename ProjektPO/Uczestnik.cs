using System;

/// @brief Klasa reprezentująca uczestnika podróży.
public class Uczestnik
{
    /// @brief Imię uczestnika.
    public string Imie { get; }
    
    /// @brief Nazwisko uczestnika.
    public string Nazwisko { get; }

    /// @brief Wiek uczestnika.
    public int wiek { get; }

    /// @brief Inicjalizuje nową instancję klasy Uczestnik.
    /// @param imie Imię uczestnika.
    /// @param nazwisko Nazwisko uczestnika.
    /// @param wiek Wiek uczestnika w latach.
    /// @throw ArgumentException Wyrzucany, gdy imię lub nazwisko są puste, albo gdy wiek jest ujemny.
    public Uczestnik(string imie, string nazwisko, int wiek)
    {
        if (string.IsNullOrWhiteSpace(imie))
        {
            throw new ArgumentException("Imię nie może być puste.");
        }
        if (string.IsNullOrWhiteSpace(nazwisko))
        {
            throw new ArgumentException("Nazwisko nie może być puste.");
        }
        if (wiek < 0)
        {
            throw new ArgumentException("Wiek nie może być ujemny.");
        }
        Imie = imie;
        Nazwisko = nazwisko;
        this.wiek = wiek;
    }

    /// @brief Zwraca pełne dane uczestnika w czytelnym formacie.
    /// @return Sformatowany ciąg znaków zawierający imię, nazwisko i wiek.
    public string PobierzPelneDane()
    {
        return $"{Imie} {Nazwisko} ({wiek} lat)";
    }
    
    /// @brief Sprawdza, czy uczestnik jest pełnoletni.
    /// @return Zwraca true, jeśli uczestnik ma co najmniej 18 lat, w przeciwnym razie false.
    public bool CzyJestPelnoletni()
    {
        return wiek >= 18;
    }
}