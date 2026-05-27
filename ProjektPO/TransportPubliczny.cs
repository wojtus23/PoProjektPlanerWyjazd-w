using System;
public class TransportPubliczny: Transport, IWymagaRezerwacji
{
    private string numerLini;
    private string rodzajBiletu;

    public TransportPubliczny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                              string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu,
                              string numerLini, string rodzajBiletu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu)
        {
            this.numerLini=numerLini;
            this.rodzajBiletu=rodzajBiletu;
        }

    public override void PokazSzczegoly()
    {
        Console.WriteLine($"Numer lini: {numerLini}");
        Console.WriteLine($"Rodzaj biletu: {rodzajBiletu}");

    }
    public bool WykonajRezerwacje(){
        Console.WriteLine($"Zarezerwowano przejazd");
        return true;
    }

    public bool AnulujRezerwacje(){
        Console.WriteLine($"Anulowano przejazd");
        return true;
    }
}