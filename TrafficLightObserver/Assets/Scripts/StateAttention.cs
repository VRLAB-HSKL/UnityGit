using UnityEngine;
using VRKL.MBU;

/// <summary>
/// State für "Ampel wird demnächst rot"
/// </summary>
sealed class StateAttention : TrafficState
{
    /// <summary>
    /// Instanz-Variable für das Abfragen. 
    /// <remarks>
    /// Wir verwenden die Instanz mit
    /// <code>StateAttention s = StateAttention.Instance;</code>.
    /// </remarks>
    /// </summary>
    public static readonly StateAttention Instance = new StateAttention();

    /// <summary>
    /// Private Konstruktor für das Singleton-Pattern.
    /// <remarks>
    /// Quelle für die Implementierung des Patterns:
    /// https://wiki.byte-welt.net/wiki/Singleton_Beispiele_(Design_Pattern)#Eager_Creation
    /// </remarks>
    /// </summary>
    private StateAttention()
    {
        _state = TrafficLightStates.Attention;
    }

    /// <summary>
    /// Wird während des Eintretens in einen State aufgerufen
    /// </summary>
    public override void OnStateEntered()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel schaltet auf gelb");
    }

    /// <summary>
    /// Wird aufgerufen während der State aktiv ist
    /// </summary>
    public override void OnStateUpdate()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel ist gelb");
    }

    /// <summary>
    /// Wird während des Verlassens eines States aufgerufen
    /// </summary>
    public override void OnStateQuit()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel wechselt auf rot");
    }

    /// <summary>
    /// Wir wechseln von "Gelb" auf "Rot"
    /// </summary>
    public override TrafficState ChangeState()
    {
        OnStateQuit();
        StateStop.Instance.OnStateEntered();
        return StateStop.Instance;
    }
}
