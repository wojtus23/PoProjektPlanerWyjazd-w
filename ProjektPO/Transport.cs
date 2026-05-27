using System;
public class Transport: PunktHarmonogramu
{
    private string srodekTransportu;
    private string miejsceOdjazdu;
    private string miejscePrzyjazdu;

    public Transport(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                     string srodekTransportu,string miejsceOdjazdu,string miejscePrzyjazdu)
        :base(nazwa, czasStart, czasKoniec, szacowanyKoszt)
        {
            this.srodekTransportu=srodekTransportu;
            this.miejsceOdjazdu=miejsceOdjazdu;
            this.miejscePrzyjazdu=miejscePrzyjazdu;
        }
    public override void PokazSzczegoly()
    {
        Console.WriteLine($"Środek transportu: {srodekTransportu}.");
        Console.WriteLine($"Miejsce odjazdu: {miejsceOdjazdu}");
        Console.WriteLine($"Miejsce przyjazdu: {miejscePrzyjazdu}");
    }           
}
