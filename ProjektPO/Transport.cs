using System;

public abstract class Transport : PunktHarmonogramu
{
    private string srodekTransportu;
    private string miejsceOdjazdu;
    private string miejscePrzyjazdu;

    protected Transport(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                        string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu, TypPunktu typPunktu)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, typPunktu)
    {
        this.srodekTransportu = srodekTransportu;
        this.miejsceOdjazdu = miejsceOdjazdu;
        this.miejscePrzyjazdu = miejscePrzyjazdu;
    }

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