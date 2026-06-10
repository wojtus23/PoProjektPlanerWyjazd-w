using System;

/// @brief Określa typ punktu w harmonogramie.
public enum TypPunktu
{
    /// @brief Atrakcja turystyczna.
    Atrakcja,
    /// @brief Miejsce noclegowe.
    Zakwaterowanie,
    /// @brief Środek transportu publicznego (np. pociąg, autobus).
    TransportPubliczny,
    /// @brief Prywatny środek transportu (np. własny samochód).
    TransportPrywatny
}

/// @brief Określa rodzaj biletu dla transportu.
public enum RodzajBiletu
{
    /// @brief Bilet normalny bez zniżek.
    Normalny,
    /// @brief Bilet ulgowy (np. studencki, szkolny).
    Ulgowy
}

/// @brief Określa aktualny stan rezerwacji dla danego punktu.
public enum StanRezerwacji
{
    /// @brief Zarezerwowano pomyślnie.
    Zarezerwowana,
    /// @brief Rezerwacja została anulowana.
    Anulowana,
    /// @brief Oczekuje na potwierdzenie rezerwacji.
    Oczekująca
}