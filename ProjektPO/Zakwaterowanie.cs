using System;

/// @brief Klasa reprezentująca obiekt zakwaterowania w planie podróży.
public class Zakwaterowanie : PunktHarmonogramu, IWymagaRezerwacji
{
    /// @brief Nazwa obiektu noclegowego.
    private string nazwaObiektu;
    /// @brief Adres obiektu zakwaterowania.
    private string adres;
    /// @brief Określa, czy śniadanie jest wliczone w cenę.
    private bool wliczoneSniadanie;

    /// @brief Inicjalizuje nową instancję klasy Zakwaterowanie.
    /// @param nazwa Nazwa punktu harmonogramu.
    /// @param czasStart Data i czas zameldowania.
    /// @param czasKoniec Data i czas wymeldowania.
    /// @param szacowanyKoszt Przewidywany koszt zakwaterowania.
    /// @param nazwaObiektu Nazwa hotelu, pensjonatu lub innego obiektu.
    /// @param adres Adres obiektu zakwaterowania.
    /// @param wliczoneSniadanie Wartość logiczna określająca, czy śniadanie jest wliczone w cenę.
    public Zakwaterowanie(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                          string nazwaObiektu, string adres, bool wliczoneSniadanie)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, TypPunktu.Zakwaterowanie)
    {
        this.nazwaObiektu = nazwaObiektu;
        this.adres = adres;
        this.wliczoneSniadanie = wliczoneSniadanie;
    }

    /// @brief Wyświetla szczegółowe informacje dotyczące miejsca noclegowego w konsoli.
    public override void PokazSzczegoly()
    {
        string sniadanieInfo = wliczoneSniadanie ? "Tak" : "Nie";

        Console.WriteLine($"[Zakwaterowanie] {nazwa}");
        Console.WriteLine($"Obiekt: {nazwaObiektu}, Adres: {adres}");
        Console.WriteLine($"Zameldowanie: {czasStart:dd.MM HH:mm} | Wymeldowanie: {czasKoniec:dd.MM HH:mm}");
        Console.WriteLine($"Koszt: {szacowanyKoszt:C} | Śniadanie wliczone: {sniadanieInfo}");
        Console.WriteLine($"Stan rezerwacji: {stanRezerwacji}");
    }

    /// @brief Potwierdza rezerwację w obiekcie zakwaterowania.
    /// @return Zwraca true po udanej rezerwacji.
    public bool WykonajRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Zarezerwowana;
        Console.WriteLine($"Zarezerwowano nocleg w obiekcie '{nazwaObiektu}'.");
        return true;
    }

    /// @brief Anuluje rezerwację w obiekcie noclegowym.
    /// @return Zwraca true po pomyślnym anulowaniu rezerwacji.
    public bool AnulujRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Anulowana;
        Console.WriteLine($"Anulowano rezerwację noclegu w obiekcie '{nazwaObiektu}'.");
        return true;
    }
}