using System;

/// @brief Klasa reprezentująca środek transportu publicznego podlegający rezerwacji.
public class TransportPubliczny : Transport, IWymagaRezerwacji
{
    private string numerLini;
    private RodzajBiletu rodzajBiletu;

    /// @brief Inicjalizuje nową instancję klasy TransportPubliczny.
    /// @param nazwa Nazwa przejazdu.
    /// @param czasStart Czas odjazdu.
    /// @param czasKoniec Czas przyjazdu.
    /// @param szacowanyKoszt Koszt biletu.
    /// @param srodekTransportu Rodzaj transportu publicznego (np. Pociąg, Autobus).
    /// @param miejsceOdjazdu Miejsce startu.
    /// @param miejscePrzyjazdu Miejsce docelowe.
    /// @param numerLini Oznaczenie linii komunikacyjnej.
    /// @param rodzajBiletu Typ biletu.
    public TransportPubliczny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                              string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu,
                              string numerLini, RodzajBiletu rodzajBiletu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu, TypPunktu.TransportPubliczny)
    {
        this.numerLini = numerLini;
        this.rodzajBiletu = rodzajBiletu;
    }

    /// @brief Wyświetla szczegółowe informacje o transporcie publicznym.
    public override void PokazSzczegoly()
    {
        base.PokazSzczegoly();
        Console.WriteLine($"Numer linii: {numerLini}");
        Console.WriteLine($"Rodzaj biletu: {rodzajBiletu}");
    }

    /// @brief Zmienia status punktu transportu na zarezerwowany.
    /// @return Zwraca true po udanej rezerwacji.
    public bool WykonajRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Zarezerwowana;
        Console.WriteLine($"Zarezerwowano przejazd linią '{numerLini}'.");
        return true;
    }

    /// @brief Anuluje aktywną rezerwację przejazdu.
    /// @return Zwraca true po anulowaniu rezerwacji.
    public bool AnulujRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Anulowana;
        Console.WriteLine($"Anulowano przejazd linią '{numerLini}'.");
        return true;
    }
}