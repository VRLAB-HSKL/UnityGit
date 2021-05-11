/// <summary>
/// States für eine konventionelle Verkehrsampel.
/// </summary>
public enum TrafficLightStates { Stop, Wait, Go, Attention }

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
public abstract class TrafficState : VRKL.MBU.State
{
    protected TrafficState() { }

    /// <summary>
    /// Die Klasse selbst verwendet dieses Enum für das Wechseln der
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
    /// Jeder Zustand weiß, was der nachfolgende Zustand ist
    /// und wechselt in diesen Zustand mit Hilfe dieser Funktion.
    /// </summary>
    public abstract TrafficState ChangeState();
}
