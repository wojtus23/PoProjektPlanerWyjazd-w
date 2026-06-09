using System;

public class TransportPrywatny : Transport
{
    private double kosztPaliwa;

    public TransportPrywatny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                             string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu, TypPunktu.TransportPrywatny)
    {
        this.kosztPaliwa = szacowanyKoszt;
    }

    public override void PokazSzczegoly()
    {
        base.PokazSzczegoly();
        Console.WriteLine($"Koszt paliwa: {kosztPaliwa:C}");
    }

    public double ObliczKosztPaliwa()
    {
        return kosztPaliwa;
    }
}