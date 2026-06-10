using System;

/// @brief Abstrakcyjna klasa bazowa reprezentująca środek przemieszczania się.
public abstract class Transport : PunktHarmonogramu
{
    private string srodekTransportu;
    private string miejsceOdjazdu;
    private string miejscePrzyjazdu;

    /// @brief Inicjalizuje nową instancję klasy bazowej Transport.
    /// @param nazwa Nazwa przejazdu/lotu.
    /// @param czasStart Czas odjazdu.
    /// @param czasKoniec Czas przyjazdu.
    /// @param szacowanyKoszt Koszt transportu.
    /// @param srodekTransportu Nazwa środka transportu (np. pociąg, samochód).
    /// @param miejsceOdjazdu Punkt początkowy podróży.
    /// @param miejscePrzyjazdu Punkt końcowy podróży.
    /// @param typPunktu Konkretny typ transportu z enumeratora.
    protected Transport(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                        string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu, TypPunktu typPunktu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, typPunktu)
    {
        this.srodekTransportu = srodekTransportu;
        this.miejsceOdjazdu = miejsceOdjazdu;
        this.miejscePrzyjazdu = miejscePrzyjazdu;
    }

    /// @brief Wyświetla podstawowe informacje o transporcie.
    public override void PokazSzczegoly()
    {
        Console.WriteLine($"Środek transportu: {srodekTransportu}");
        Console.WriteLine($"Miejsce odjazdu: {miejsceOdjazdu}");
        Console.WriteLine($"Czas odjazdu: {czasStart:dd.MM.yyyy HH:mm}");
        Console.WriteLine($"Miejsce przyjazdu: {miejscePrzyjazdu}");
        Console.WriteLine($"Czas przyjazdu: {czasKoniec:dd.MM.yyyy HH:mm}");
        Console.WriteLine($"Stan rezerwacji: {stanRezerwacji}");
    }
}