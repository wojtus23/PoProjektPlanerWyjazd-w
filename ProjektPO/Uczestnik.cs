using System;
public class Uczestnik
{
    public string Imie { get; }
    public string Nazwisko { get; }

    public int wiek { get;}

    public Uczestnik(string imie, string nazwisko, int wiek)
    {
        if (string.IsNullOrWhiteSpace(imie))
        {
            throw new ArgumentException("Imię nie może być puste.");
        }
        if (string.IsNullOrWhiteSpace(nazwisko))
        {
            throw new ArgumentException("Nazwisko nie może być puste.");
        }
        if (wiek < 0)
        {
            throw new ArgumentException("Wiek nie może być ujemny.");
        }
        Imie = imie;
        Nazwisko = nazwisko;
        this.wiek = wiek;
    }

    public string PobierzPelneDane()
    {
        return $"{Imie} {Nazwisko} ({wiek} lat)";
    }
    public bool CzyJestPelnoletni()
    {
        return wiek >= 18;
    }
}