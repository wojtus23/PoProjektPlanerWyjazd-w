using System;

/// @brief Abstrakcyjna klasa bazowa reprezentująca pojedynczy punkt w harmonogramie podróży.
public abstract class PunktHarmonogramu : IComparable<PunktHarmonogramu>
{
    /// @brief Nazwa punktu harmonogramu.
    protected string nazwa;
    
    /// @brief Czas rozpoczęcia.
    protected DateTime czasStart;
    
    /// @brief Czas zakończenia.
    protected DateTime czasKoniec;
    
    /// @brief Przewidywany koszt.
    protected double szacowanyKoszt;
    
    /// @brief Typ punktu.
    protected TypPunktu typPunktu;
    
    /// @brief Stan rezerwacji punktu.
    protected StanRezerwacji stanRezerwacji;

    /// @brief Właściwość pobierająca szacowany koszt punktu harmonogramu.
    public double SzacowanyKoszt => szacowanyKoszt;

    /// @brief Czas rozpoczęcia wydarzenia (właściwość publiczna).
    public DateTime CzasStart => czasStart;

    /// @brief Czas zakończenia wydarzenia (właściwość publiczna).
    public DateTime CzasKoniec => czasKoniec;

    /// @brief Inicjalizuje nową instancję klasy bazowej PunktHarmonogramu.
    /// @param nazwa Nazwa punktu.
    /// @param czasStart Czas rozpoczęcia wydarzenia.
    /// @param czasKoniec Czas zakończenia wydarzenia.
    /// @param szacowanyKoszt Przewidywany koszt.
    /// @param typPunktu Typ przypisany do tego punktu.
    /// @throw ArgumentException Wyrzucany, gdy nazwa jest pusta, czas zakończenia jest przed czasem rozpoczęcia lub koszt jest ujemny.
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

    /// @brief Oblicza czas trwania punktu harmonogramu.
    /// @return Różnica między czasem zakończenia i rozpoczęcia jako obiekt TimeSpan.
    public TimeSpan ObliczCzasTrwania()
    {
        return czasKoniec - czasStart;
    }

    /// @brief Sprawdza, czy ten punkt nakłada się czasowo z innym punktem harmonogramu.
    /// @param innyPunkt Inny punkt harmonogramu do porównania.
    /// @return Zwraca true, jeśli punkty się nakładają; w przeciwnym razie false.
    public bool CzyKoliduje(PunktHarmonogramu innyPunkt)
    {
        if (innyPunkt == null) 
            return false;

        if (this.typPunktu == TypPunktu.Zakwaterowanie || innyPunkt.typPunktu == TypPunktu.Zakwaterowanie)
        {
            if (this.typPunktu != innyPunkt.typPunktu)
            {
                return false;
            }
        }

        return this.czasStart < innyPunkt.czasKoniec && innyPunkt.czasStart < this.czasKoniec;
    }

    /// @brief Abstrakcyjna metoda do wyświetlania szczegółów punktu harmonogramu w konsoli.
    public abstract void PokazSzczegoly();

    /// @brief Porównuje dwa punkty po czasie ich rozpoczęcia, co umożliwia ich sortowanie.
    /// @param inny Inny punkt do porównania.
    /// @return Wartość całkowita wskazująca kolejność.
    public int CompareTo(PunktHarmonogramu? inny)
    {
        if (inny == null) 
            return 1;

        return this.czasStart.CompareTo(inny.czasStart);
    }
}