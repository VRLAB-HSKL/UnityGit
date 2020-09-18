using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für eine State Machine.
/// 
/// Der Zustandsautomat ist so implementiert, dass
/// die einzelnen Zustände, die von dieser Basisklasse
/// abgeleitet werden selbst entscheiden,
/// in welchen zukünftigen Zustand sie wechseln.
/// Klassen, die den Zustandsautomaten verwenden 
/// rufen die Funktion <code>OnStateUpdate</code>
/// auf. Dort wird in einer abgeleiteten State-Klasse
/// entschieden, ob der Zustand verlassen wird
/// und welcher Zustand jetzt angenommen wird.
/// 
/// Von dieser Basisklasse abgeleitete Klassen
/// implementieren das Singleton Pattern!
/// </summary>
public abstract class State
{
    protected State() { }

    /// <summary>
    /// Die Klasse selbst verwenden dieses Enum für das Wechseln der
    /// Zustände nicht. 
    /// Der Wert wird ausschließlich verwendet, um für
    /// Anfragen von außen den Zustand zu definieren.
    /// </summary>
    protected TrafficLightStates _state { get; set; }
    public TrafficLightStates LightState
    {
        get { return _state; }
    }

    /// <summary>
    /// Wird während des Eintretens in einen State aufgerufen
    /// </summary>
    public abstract void OnStateEntered();

    /// <summary>
    /// Wird aufgerufen während der State aktiv ist
    /// </summary>
    public abstract void OnStateUpdate();

    /// <summary>
    /// Wird während des Verlassens eines States aufgerufen
    /// </summary>
    public abstract void OnStateQuit();

    /// <summary>
    /// Jeder Zustand weiß, was der nachfolgende Zustand ist
    /// und wechselt in diesen Zustand mit Hilfe dieser Funktion.
    /// </summary>
    public abstract State ChangeState();

    /// <summary>
    /// Sollen die Zustandsklassen eine Ausgabe auf der Konsole durchführen?
    /// </summary>
    public bool DebugOutput
    {
        get { return _output; }
        set { _output = value; }
    }
    protected bool _output { get; set; } = false;
}
