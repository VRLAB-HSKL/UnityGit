using VRKL.MBU;

/// <summary>
/// Subject-Klasse für die Verkehrsampel
/// 
/// Wir fügen die Zustandsklassen als Komponente hinzu,
/// dann können wir sie in Awake abfragen und initialisieren.
/// Farben und Materialien sind auch public, da wir diese
/// Attribute auch in den Zustandsklassen abfragen
/// und setzen. 
/// 
/// Um zu verhindern, dass sie im Inspector auftauchen werden 
/// Sie mit get und set-Klauseln versehen. Man könnte
/// sie auch als Components im Inspector auftauchen lassen,
/// sie müssen aber nicht gesetzt werden, was zu Irritationen
/// führen könnte.
/// 
/// Die verwenden Farben werden aus entsprechenden Materialien
/// im Verzeichnis Resources/Material abgelesen. Assets, die
/// unterhalb des Verzeichnisses Resources liegen können mit
/// Resources.Load in die Anwendung geladen werden.
/// </summary>
public class TrafficLight : Subject
{
    /// <summary>
    /// Integer Counter für Rot
    /// </summary>
    public int TimeForStop
    {
        get { return _timeStop; }
        set { _timeStop = value; }
    }
    private int _timeStop { get; set; }
    /// <summary>
    /// Integer Counter für Rot und Gelb
    /// </summary>
    public int TimeForWait
    {
        get { return _timeWait; }
        set { _timeWait = value; }
    }
    private int _timeWait { get; set; }
    /// <summary>
    /// /// Integer Counter für Grün
    /// /// </summary>
    public int TimeForGo
    {
        get { return _timeGo; }
        set { _timeGo = value; }
    }
    private int _timeGo { get; set; }
    /// <summary>
    /// /// Integer Counter für Gelb
    /// /// </summary>
    public int TimeForAttention
    {
        get { return _timeAttention; }
        set { _timeAttention = value; }
    }
    private int _timeAttention { get; set; }

    /// <summary>
    /// Zeitangaben für die Zustände der Ampel.
    /// <remarks>Aktuell zählen wir eine int-Variable hoch.
    /// Möglich wäre natürlich, mit Hilfe einer Instanz
    /// von <code>DateTime</code> in Sekunden zu denken.</remarks>
    /// </summary>
    public int Counter
    {
        get { return _counter; }
        set { _counter = value; }
    }
    private int _counter { get; set; }

    /// <summary>
    /// Der aktuelle Zustand der Ampel
    /// </summary>
    public TrafficState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    private TrafficState _currentState { get; set; }

    /// <summary>
    /// Sollen die Zustandsklassen eine Ausgabe auf der Konsole durchführen?
    /// </summary>
    private bool LogOutput { get; set; }

    /// <summary>
    /// Update für die Verkehrsampel
    /// </summary>
    public void Update()
    {
        Counter++;
        CurrentState.OnStateUpdate();
        if (Counter > TimeForWait)
        {
            CurrentState = CurrentState.ChangeState();
            CurrentState.DebugOutput = LogOutput;
            Counter = 0;
        }
        Notify();
    }

    /// <summary>
    /// Default-Konstruktor
    /// </summary>
    public TrafficLight()
    {
        LogOutput = false;

        TimeForStop = 20;
        TimeForWait = 20;
        TimeForGo = 20;
        TimeForAttention = 20;

        Counter = 0;
        CurrentState = StateStop.Instance;
        CurrentState.DebugOutput = LogOutput;
        CurrentState.OnStateEntered();
    }

    /// <summary>
    /// Default-Konstruktor
    /// </summary>
    /// <param name="stopTime">Counter für die Rot-Phase</param>
    /// <param name="waitTime">Counter für die Rot-Gelb-Phase</param>
    /// <param name="goTime">Counter für die Grün-Phase</param>
    /// <param name="attentionTime">Counter für die Gelb-Phase</param>
    /// <param name="startState">Anfangszustand der Ampel</param>
    /// <param name="logs">Ausgaben auf der Unity-Konsole?</param>
    public TrafficLight(int stopTime, int waitTime, 
                        int goTime, int attentionTime,
                        TrafficState startState,
                        bool logs)
    {
        LogOutput = logs;

        TimeForStop = stopTime;
        TimeForWait = waitTime;
        TimeForGo = goTime;
        TimeForAttention = attentionTime;

        Counter = 0;
        CurrentState = startState;
        CurrentState.DebugOutput = LogOutput;
        CurrentState.OnStateEntered();
    }
}