using System;

public class Zakwaterowanie : PunktHarmonogramu, IWymagaRezerwacji
{
    private string nazwaObiektu;
    private string adres;
    private bool wliczoneSniadanie;

    public Zakwaterowanie(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                          string nazwaObiektu, string adres, bool wliczoneSniadanie)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, TypPunktu.Zakwaterowanie)
    {
        this.nazwaObiektu = nazwaObiektu;
        this.adres = adres;
        this.wliczoneSniadanie = wliczoneSniadanie;
    }

    public override void PokazSzczegoly()
    {
        string sniadanieInfo = wliczoneSniadanie ? "Tak" : "Nie";

        Console.WriteLine($"[Zakwaterowanie] {nazwa}");
        Console.WriteLine($"Obiekt: {nazwaObiektu}, Adres: {adres}");
        Console.WriteLine($"Zameldowanie: {czasStart:dd.MM HH:mm} | Wymeldowanie: {czasKoniec:dd.MM HH:mm}");
        Console.WriteLine($"Koszt: {szacowanyKoszt:C} | Śniadanie wliczone: {sniadanieInfo}");
        Console.WriteLine($"Stan rezerwacji: {stanRezerwacji}");
    }

    public bool WykonajRezerwacje()
    {
        stanRezerwacji = stanRezerwacji.Zarezerwowana;
        Console.WriteLine($"Zarezerwowano nocleg w obiekcie '{nazwaObiektu}'.");
        return true;
    }

    public bool AnulujRezerwacje()
    {
        stanRezerwacji = stanRezerwacji.Anulowana;
        Console.WriteLine($"Anulowano rezerwację noclegu w obiekcie '{nazwaObiektu}'.");
        return true;
    }
}