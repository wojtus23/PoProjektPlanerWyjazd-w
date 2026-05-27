using System;
public class Atrakcja: PunktHarmonogramu, IWymagaRezerwacji
{
    private string typAtrakcji;
    private string krotkiOpis;

    public Atrakcja(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt, 
                    string typAtrakcji, string krotkiOpis) 
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt){
        this.typAtrakcji=typAtrakcji;
        this.krotkiOpis=krotkiOpis;
    }
    public override void PokazSzczegoly(){
        Console.WriteLine($"[Atrakcja] {nazwa} ({typAtrakcji})");
        Console.WriteLine($"Czas: {czasStart:dd.MM HH.mm}-{czasKoniec:HH.mm}");
        Console.WriteLine($"Koszt; {szacowanyKoszt:C}");
        Console.WriteLine($"Opis: {krotkiOpis}");
    }
    public bool WykonajRezerwacje()
    {
        Console.WriteLine($"Rezerwacja atrakcji '{nazwa}' przebiegła pomyślnie.");
        return true;
    }

    public bool AnulujRezerwacje()
    {
        Console.WriteLine($"Anulowano rezerwację atrakcji '{nazwa}'.");
        return true;
    }
}