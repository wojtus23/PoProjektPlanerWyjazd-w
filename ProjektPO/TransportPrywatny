using System;
public class TransportPrywatny: Transport
{
    private double kosztPaliwa;
    public TransportPrywatny(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                              string srodekTransportu, string miejsceOdjazdu, string miejscePrzyjazdu,
                              double kosztPaliwa)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, srodekTransportu, miejsceOdjazdu, miejscePrzyjazdu)
        {
            this.kosztPaliwa=kosztPaliwa;
        }
    
    public double ObliczKosztPaliwa(){
        return kosztPaliwa;
    }

}