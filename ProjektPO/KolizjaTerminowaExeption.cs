using System;
/// @brief Wyjątek rzucany w sytuacji, gdy nowy punkt harmonogramu nakłada się czasowo z już istniejącym.
public class KolizjaTerminowException: Exception
{
    /// @brief Punkt harmonogramu, z którym wystąpiła kolizja.
    public PunktHarmonogramu IstniejacyPunkt { get; }

    /// @brief Inicjalizuje nową instancję klasy KolizjaTerminowException.
    /// @param istniejacyPunkt Punkt, który spowodował kolizję.
    /// @param message Komunikat błędu.
    public KolizjaTerminowException(PunktHarmonogramu istniejacyPunkt, string message) : base(message)
    {
        IstniejacyPunkt = istniejacyPunkt;
    }
}
