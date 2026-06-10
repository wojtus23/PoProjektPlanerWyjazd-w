using System;

/// @brief Klasa reprezentująca atrakcję turystyczną w harmonogramie podróży.
public class Atrakcja : PunktHarmonogramu, IWymagaRezerwacji
{
    /// @brief Typ atrakcji turystycznej.
    private string typAtrakcji;
    /// @brief Krótki opis wydarzenia.
    private string krotkiOpis;

    /// @brief Inicjalizuje nową instancję klasy Atrakcja.
    /// @param nazwa Nazwa atrakcji.
    /// @param czasStart Data i czas rozpoczęcia.
    /// @param czasKoniec Data i czas zakończenia.
    /// @param szacowanyKoszt Przewidywany koszt udziału w atrakcji.
    /// @param typAtrakcji Typ atrakcji (np. muzeum, park rozrywki).
    /// @param krotkiOpis Krótki opis miejsca lub wydarzenia.
    public Atrakcja(string nazwa, DateTime czasStart, DateTime czasKoniec, double szacowanyKoszt,
                    string typAtrakcji, string krotkiOpis)
        : base(nazwa, czasStart, czasKoniec, szacowanyKoszt, TypPunktu.Atrakcja)
    {
        this.typAtrakcji = typAtrakcji;
        this.krotkiOpis = krotkiOpis;
    }

    /// @brief Wyświetla szczegółowe informacje na temat atrakcji.
    public override void PokazSzczegoly()
    {
        Console.WriteLine($"[Atrakcja] {nazwa} ({typAtrakcji})");
        Console.WriteLine($"Czas: {czasStart:dd.MM HH:mm} - {czasKoniec:HH:mm}");
        Console.WriteLine($"Koszt: {szacowanyKoszt:C}");
        Console.WriteLine($"Opis: {krotkiOpis}");
        Console.WriteLine($"Stan rezerwacji: {stanRezerwacji}");
    }

    /// @brief Zmienia stan rezerwacji atrakcji na zarezerwowaną.
    /// @return Zawsze zwraca true po pomyślnej zmianie statusu.
    public bool WykonajRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Zarezerwowana;
        Console.WriteLine($"Rezerwacja atrakcji '{nazwa}' przebiegła pomyślnie.");
        return true;
    }

    /// @brief Anuluje aktywną rezerwację atrakcji.
    /// @return Zawsze zwraca true po pomyślnej zmianie statusu.
    public bool AnulujRezerwacje()
    {
        stanRezerwacji = StanRezerwacji.Anulowana;
        Console.WriteLine($"Anulowano rezerwację atrakcji '{nazwa}'.");
        return true;
    }
}