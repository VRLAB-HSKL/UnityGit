using UnityEngine;
using VRKL.MBU;

/// <summary>
/// State für "Ampel wird demnächst grün".
/// 
/// Wir steuern eine Ampel, die grafisch und
/// auf Wunsch auch auf der Konsole ausgegeben 
/// werden kann. Die grafische Ausgabe
/// erfolgt in einer von MonoBehaviour abgeleiteten
/// Klasse <code>ApplicationManager</code>.
/// Wir setzen diese Instanz in der Funktion
/// <code>ConnectManager</code>.
/// </summary>
sealed class StateWait : TrafficState
{
    /// <summary>
    /// Instanz-Variable für das Abfragen. 
    /// <remarks>
    /// Wir verwenden die Instanz mit
    /// <code>StateGo s = StateGo.Instance;</code>.
    /// </remarks>
    /// </summary>
    public static readonly StateWait Instance = new StateWait();

    /// <summary>
    /// Private Konstruktor für das Singleton-Pattern.
    /// <remarks>
    /// Quelle für die Implementierung des Patterns:
    /// https://wiki.byte-welt.net/wiki/Singleton_Beispiele_(Design_Pattern)#Eager_Creation
    /// </remarks>
    /// </summary>
    private StateWait()
    {
        _state = TrafficLightStates.Wait;
    }

    /// <summary>
    /// Wird während des Eintretens in einen State aufgerufen
    /// </summary>
    public override void OnStateEntered()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel schaltet auf rot und gelb");
    }

    /// <summary>
    /// Wird aufgerufen während der State aktiv ist
    /// </summary>
    public override void OnStateUpdate()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel  ist rot und gelb");
    }

    /// <summary>
    /// Wird während des Verlassens eines States aufgerufen
    /// </summary>
    public override void OnStateQuit()
    {
        if (DebugOutput)
            Debug.Log("Die Ampel wechselt auf grün");
    }

    /// <summary>
    /// Wir wechseln von "Rot und Gelb" auf "Grün"
    /// </summary>
    public override TrafficState ChangeState()
    {
        OnStateQuit();
        StateGo.Instance.OnStateEntered();
        return StateGo.Instance;
    }
}
