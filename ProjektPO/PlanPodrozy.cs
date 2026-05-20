using System;
using System.Collections.Generic;
using System.Linq;

public class PlanPodrozy
{
    private string tytulPodrozy;
    private DateTime dataRozpoczecia;
    private DateTime dataZakonczenia; 
    private double budzetCalkowity;
    private List<PunktHarmonogramu> listaPunktow;
    private List<Uczestnik> listaUczestnikow;

    public PlanPodrozy(string tytulPodrozy, DateTime dataRozpoczecia, DateTime dataZakonczenia, double budzetCalkowity)
    {
        if (string.IsNullOrWhiteSpace(tytulPodrozy))
            throw new ArgumentException("Tytuł podróży nie może być pusty.", nameof(tytulPodrozy));
            
        if (dataZakonczenia < dataRozpoczecia)
            throw new ArgumentException("Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.");
            
        if (budzetCalkowity < 0)
            throw new ArgumentException("Budżet całkowity nie może być ujemny.");

        this.tytulPodrozy = tytulPodrozy;
        this.dataRozpoczecia = dataRozpoczecia;
        this.dataZakonczenia = dataZakonczenia;
        this.budzetCalkowity = budzetCalkowity;
        this.listaPunktow = new List<PunktHarmonogramu>();
        this.listaUczestnikow = new List<Uczestnik>();
    }

    public void DodajPunkt(PunktHarmonogramu punkt)
    {
        if (punkt == null)
            throw new ArgumentNullException(nameof(punkt), "Punkt harmonogramu nie może być pusty.");

        foreach (var istniejacyPunkt in listaPunktow)
        {
            if (istniejacyPunkt.CzyKoliduje(punkt))
            {
                throw new KolizjaTerminowException(istniejacyPunkt, 
                    "Wykryto kolizję! Nowy punkt nakłada się czasowo z istniejącym zdarzeniem.");
            }
        }

        listaPunktow.Add(punkt);
        listaPunktow.Sort();
    }

    public void UsunPunkt(int id)
    {
        if (id < 0 || id >= listaPunktow.Count)
            throw new ArgumentOutOfRangeException(nameof(id), "Nie znaleziono punktu o podanym identyfikatorze.");

        listaPunktow.RemoveAt(id);
    }

    public double ObliczKosztCalkowity()
    {
        return listaPunktow.Sum(p => p.SzacowanyKoszt);
    }

    public void GenerujPodsumowanie()
    {
        double aktualnyKoszt = ObliczKosztCalkowity();
        double saldo = budzetCalkowity - aktualnyKoszt;

        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"[ PLAN PODRÓŻY: {tytulPodrozy.ToUpper()} ]");
        Console.WriteLine($"Termin: {dataRozpoczecia:dd.MM.yyyy} - {dataZakonczenia:dd.MM.yyyy}");
        Console.WriteLine($"Budżet: {budzetCalkowity:C} | Koszt: {aktualnyKoszt:C}");
        
        if (saldo < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"UWAGA: Przekroczono budżet o {Math.Abs(saldo):C}!");
            Console.ResetColor();
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"UCZESTNICY ({listaUczestnikow.Count}):");
        foreach (var uczestnik in listaUczestnikow)
        {
            Console.WriteLine($"   - {uczestnik.PobierzPelneDane()}");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"HARMONOGRAM ({listaPunktow.Count} zdarzeń):");
        
        if (listaPunktow.Count == 0)
        {
            Console.WriteLine("   Brak zaplanowanych punktów.");
        }
        else
        {
            foreach (var punkt in listaPunktow)
            {
                punkt.PokazSzczegoly(); 
            }
        }
        Console.WriteLine(new string('=', 60));
    }

    public void DodajUczestnika(Uczestnik uczestnik)
    {
        if (uczestnik == null)
            throw new ArgumentNullException(nameof(uczestnik));
            
        listaUczestnikow.Add(uczestnik);
    }
}
public abstract class PunktHarmonogramu: IComparable<PunktHarmonogramu>
{
    private string nazwa;
    private DateTime czasStart;
    private DateTime czasKoniec;
    private double szacowanyKoszt;

    public string Nazwa => nazwa;
    public DateTime CzasStart => czasStart;
    public DateTime CzasKoniec => czasKoniec;
    public double SzacowanyKoszt => szacowanyKoszt;

    protected PunktHartmonogramy(string nazwa, DateTime czasStart,DateTime czasKoniec,double szacowanyKoszt){
        if (string.IsNullOrWhiteSpace(nazwa))
            throw new ArgumentException("Nazwa nie może być pusta", nameof(nazwa));
        if (czasKoniec<czasStart)
            throw new ArgumentException("Czas zakończenia nie może być wcześniejszy niż czas rozpoczęcia.", nameof(czasKoniec));
        if (szacowanyKoszt <0)
            throw new ArgumentException("Koszt nie może być ujemny.", nameof(szacowanyKoszt));
        
        this.nazwa=nazwa;
        this.czasStart=czasStart;
        this.czasKoniec=czasKoniec;
        this.szacowanyKoszt=szacowanyKoszt;
    }

    public TimeSpan ObliczCzasTrwania(){
        return czasKoniec-czasStart;
    }

    public bool CzyKoliduje(PunktHarmonogramu innyPunkt){
        if(innyPunkt==null) return false;
        return this.czasStart<innyPunkt.czasKoniec && innyPunkt.czasSTart<this.czasKoniec;
    }

    public abstract void PokazSzczegoly();

    public int CompareTo(PunktHarmonogramu inny){
        if(inny==null) return 1;
        return this.czasStart.CompareTo(inny.CzasStart);
    }
}
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
public class Zakwaterowanie: PunktHarmonogramu, IWymagaRezerwacji
{
    private string nazwaObiektu;
    private string adres;
    private bool wliczoneSniadanie;

    public Zakwaterowanie(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt, 
                          string nazwaObiektu, string adres, bool wliczoneSniadanie) 
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt)
        {
            this.nazwaObiektu=nazwaObiektu;
            this.adres=adres;
            this.wliczoneSniadanie=wliczoneSniadanie;
        }

    public override void PokazSzczegoly()
    {
        string sniadanieInfo = wliczoneSniadanie ? "Tak" : "Nie";
        
        Console.WriteLine($"[Zakwaterowanie] {nazwa}");
        Console.WriteLine($"Obiekt: {nazwaObiektu}, Adres: {adres}");
        Console.WriteLine($"Zameldowanie: {czasStart:dd.MM HH:mm} | Wymeldowanie: {czasKoniec:dd.MM HH:mm}");
        Console.WriteLine($"Koszt: {szacowanyKoszt:C} | Śniadanie wliczone: {sniadanieInfo}");
    }

    public bool WykonajRezerwacje(){
        Console.WriteLine($"Zarezerwowano nocleg w obiekcie '{nazwaObiektu}'.");
        return true;
    }

    public bool AnulujRezerwacje(){
        Console.WriteLine($"Anulowano rezerwację noclegu w obiekcie '{nazwaObiektu}'.");
        return true;
    }
}

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

public class TransportPrywatny: Transport
{

}
public interface IWymagaRezerwacji
{
    bool WykonajRezerwacje();
    bool AnulujRezerwacje();
}
