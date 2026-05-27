using System;
public class KolizjaTerminowException: Exception
{
    public PunktHarmonogramu IstniejacyPunkt { get; }

    public KolizjaTerminowException(PunktHarmonogramu istniejacyPunkt, string message) : base(message)
    {
        IstniejacyPunkt = istniejacyPunkt;
    }
}
