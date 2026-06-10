using System;

/// @brief Klasa reprezentująca przejazd prywatnym środkiem transportu.
public class TransportPrywatny : Transport
{
    private double kosztPaliwa;

    /// @brief Inicjalizuje nową instancję klasy TransportPrywatny.
    /// @param nazwa Nazwa etapu podróży.
    /// @param czasStart Czas wyjazdu.
    /// @param czasKoniec Czas dojazdu na miejsce.
    /// @param szacowanyKoszt Szacowany koszt przejazdu (np. koszt paliwa i opłat drogowych).
    /// @param srodekTransportu Pojazd (np. Samochód osobowy).
    /// @param miejsceOdjazdu Punkt startowy.
    /// @param miejscePrzyjazdu Punkt docelowy.
    public TransportPrywatny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                             string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu, TypPunktu.TransportPrywatny)
    {
        this.kosztPaliwa = szacowanyKoszt;
    }

    /// @brief Wyświetla informacje o przejeździe prywatnym wraz z kosztami paliwa.
    public override void PokazSzczegoly()
    {
        base.PokazSzczegoly();
        Console.WriteLine($"Koszt paliwa: {kosztPaliwa:C}");
    }

    /// @brief Pobiera wartość kosztów poniesionych na paliwo.
    /// @return Koszt paliwa jako wartość zmiennoprzecinkowa.
    public double ObliczKosztPaliwa()
    {
        return kosztPaliwa;
    }
}