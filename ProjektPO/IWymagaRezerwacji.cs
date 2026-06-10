using System;

/// @brief Interfejs definiujący metody dla punktów harmonogramu wymagających rezerwacji.
public interface IWymagaRezerwacji
{
    /// @brief Wykonuje rezerwację dla danego punktu.
    /// @return Zwraca true, jeśli rezerwacja się powiodła.
    bool WykonajRezerwacje();

    /// @brief Anuluje istniejącą rezerwację.
    /// @return Zwraca true, jeśli anulowanie się powiodło.
    bool AnulujRezerwacje();
}

