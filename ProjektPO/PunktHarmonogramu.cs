using System;

public abstract class PunktHarmonogramu : IComparable<PunktHarmonogramu>
{
    protected string nazwa;
    protected DateTime czasStart;
    protected DateTime czasKoniec;
    protected double szacowanyKoszt;
    protected TypPunktu typPunktu;
    protected StanRezerwacji stanRezerwacji;

    public double SzacowanyKoszt => szacowanyKoszt;

    protected PunktHarmonogramu(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt, TypPunktu typPunktu)
    {
        if (string.IsNullOrWhiteSpace(nazwa))
            throw new ArgumentException("Nazwa nie może być pusta.", nameof(nazwa));

        if (czasKoniec <= czasStart)
            throw new ArgumentException("Czas zakończenia musi być późniejszy niż czas rozpoczęcia.", nameof(czasKoniec));

        if (szacowanyKoszt < 0)
            throw new ArgumentException("Koszt nie może być ujemny.", nameof(szacowanyKoszt));

        this.nazwa = nazwa;
        this.czasStart = czasStart;
        this.czasKoniec = czasKoniec;
        this.szacowanyKoszt = szacowanyKoszt;
        this.typPunktu = typPunktu;
        this.stanRezerwacji = StanRezerwacji.Oczekująca;
    }

    public TimeSpan ObliczCzasTrwania()
    {
        return czasKoniec - czasStart;
    }

    public bool CzyKoliduje(PunktHarmonogramu innyPunkt)
    {
        if (innyPunkt == null) 
            return false;
        if (innyPunkt.typPunktu != this.typPunktu)
            return false;

        return this.czasStart < innyPunkt.czasKoniec && innyPunkt.czasStart < this.czasKoniec;
    }

    public abstract void PokazSzczegoly();

    public int CompareTo(PunktHarmonogramu? inny)
    {
        if (inny == null) 
            return 1;

        return this.czasStart.CompareTo(inny.czasStart);
    }
}