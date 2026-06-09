using System;

public class TransportPubliczny : Transport, IWymagaRezerwacji
{
    private string numerLini;
    private RodzajBiletu rodzajBiletu;

    public TransportPubliczny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                              string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu,
                              string numerLini, RodzajBiletu rodzajBiletu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu, TypPunktu.TransportPubliczny)
    {
        this.numerLini = numerLini;
        this.rodzajBiletu = rodzajBiletu;
    }

    public override void PokazSzczegoly()
    {
        base.PokazSzczegoly();
        Console.WriteLine($"Numer linii: {numerLini}");
        Console.WriteLine($"Rodzaj biletu: {rodzajBiletu}");
    }

    public bool WykonajRezerwacje()
    {
        stanRezerwacji = stanRezerwacji.Zarezerwowana;
        Console.WriteLine($"Zarezerwowano przejazd linią '{numerLini}'.");
        return true;
    }

    public bool AnulujRezerwacje()
    {
        stanRezerwacji = stanRezerwacji.Anulowana;
        Console.WriteLine($"Anulowano przejazd linią '{numerLini}'.");
        return true;
    }
}