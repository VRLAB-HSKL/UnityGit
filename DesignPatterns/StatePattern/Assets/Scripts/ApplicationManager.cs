using UnityEngine;

/// <summary>
/// Manager für die Demo der State Machine "Verkehrsampel".
/// 
/// Die verwendeten Farben werden aus entsprechenden Materialien
/// im Verzeichnis Resources/Material abgelesen.
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    /// <summary>
    /// Integer Counter für Rot
    /// </summary>
    [Tooltip("Integer Counter für rot")]
    [Range(40, 300)]
    public int TimeForStop = 60;
    /// <summary>
    /// Integer Counter für Rot und Gelb
    /// </summary>
    [Tooltip("Integer Counter für rot und gelb")]
    [Range(40, 300)]
    public int TimeForWait = 60;
    /// <summary>
    /// Integer Counter für Grün
    /// </summary>
    [Tooltip("Integer Counter für grün")]
    [Range(40, 300)]
    public int TimeForGo = 60;
    /// <summary>
    /// Integer Counter für Gelb
    /// </summary>
    [Tooltip("Integer Counter für gelb")]
    [Range(40, 300)]
    public int TimeForAttention = 60;

    /// <summary>
    /// Sollen die Zustandsklassen eine Ausgabe auf der Konsole durchführen?
    /// </summary>
    public bool LogOutput = false;

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
    /// Farbe für aktives rotes Licht
    /// </summary>
    public Color RedActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives rotes Licht
    /// </summary>
    public Color RedPassiveColor { get; set; }
    /// <summary>
    /// Farbe für aktives gelbes Licht
    /// </summary>
    public Color YellowActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives gelbes Licht
    /// </summary>
    public Color YellowPassiveColor { get; set; }
    /// <summary>
    /// Farbe für aktives grünes Licht
    /// </summary>
    public Color GreenActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives grünes Licht
    /// </summary>
    public Color GreenPassiveColor { get; set; }

    /// <summary>
    /// GameObject für das rote Licht der Ampel
    /// </summary>
    public GameObject Red { get; set; }
    /// <summary>
    /// Material des roten Lichts
    /// </summary>
    public Material RedMaterial { get; set; }
    /// <summary>
    /// GameObject für das gelbe Licht der Ampel
    /// </summary>
    public GameObject Yellow { get; set; }
    /// <summary>
    /// Material des gelben Lichts
    /// </summary>
    public Material YellowMaterial { get; set; }
    /// <summary>
    /// GameObject für das grüne Licht der Ampel
    /// </summary>
    public GameObject Green { get; set; }
    /// <summary>
    /// Material des grünen Lichts
    /// </summary>
    public Material GreenMaterial { get; set; }

    /// <summary>
    /// Verbindungen herstellen.
    /// 
    /// Die GameObjects, die die drei Leuchten der Ampel
    /// ausgeben werden als eine Hierarchie erwartet.
    /// Die Wurzel heißt "Ampel", und die drei Leuchten
    /// wie zu erwarten "Rot", "Gelb" und "Grün". Sie werden
    /// mit <code>GameObject.Find</code> mit ihrem Namen, z.b.
    /// "Ampel/Rot", abgefragt.
    /// 
    /// Wir setzen alle Farben, die wir für die Darstellung
    /// der einzelnen Zustände der Ampel benötigen.
    /// </summary>
    private void Awake()
    {
        Red = GameObject.Find("Ampel/Rot");
        if (LogOutput && (Red = null))
            Debug.Log("Rote Leuchte nicht gefunden!");
        Yellow = GameObject.Find("Ampel/Gelb");
        if (LogOutput && (Yellow = null))
            Debug.Log("Rote Leuchte nicht gefunden!");
        Green = GameObject.Find("Ampel/Grün");
        if (LogOutput && (Green = null))
            Debug.Log("Rote Leuchte nicht gefunden!");

        // Materialkomponenten dieser GameObjects abfragen
        RedMaterial = Red.GetComponent<Renderer>().material;
        YellowMaterial = Yellow.GetComponent<Renderer>().material;
        GreenMaterial = Green.GetComponent<Renderer>().material;

        // Materialien aus den Resourcen laden
        TrafficColors();
    }

    /// <summary>
    /// Die Ampel startet mit Rot.
    /// </summary>
    private void Start()
    {
        CurrentState = StateStop.Instance;
        CurrentState.OnStateEntered();
    }

    /// <summary>
    /// Wir zählen den Counter hoch
    /// und geben eine Statusmeldung aus.
    /// </summary>
    private void FixedUpdate()
    {
        Counter++;
        CurrentState.OnStateUpdate();
        if (Counter > TimeForWait)
        {
            CurrentState = CurrentState.ChangeState();
            SetColors();
            Counter = 0;
        }
    }

    /// <summary>
    /// Die Materialien für die Ampel aus dem Resources-Verzeichnis laden
    /// </summary>
    private void TrafficColors()
    {
        // Material der drei GameObjects abfragen und Farben speichern
        Material RedActive = Resources.Load("Material/Red", typeof(Material)) as Material;
        RedActiveColor = RedActive.color;
        Material YellowActive = Resources.Load("Material/Yellow", typeof(Material)) as Material;
        YellowActiveColor = YellowActive.color;
        Material GreenActive = Resources.Load("Material/Green", typeof(Material)) as Material;
        GreenActiveColor = GreenActive.color;

        Material RedPassive = Resources.Load("Material/RedPassive", typeof(Material)) as Material;
        RedPassiveColor = RedPassive.color;
        Material YellowPassive = Resources.Load("Material/YellowPassive", typeof(Material)) as Material;
        YellowPassiveColor = YellowPassive.color;
        Material GreenPassive = Resources.Load("Material/GreenPassive", typeof(Material)) as Material;
        GreenPassiveColor = GreenPassive.color;
    }

    /// <summary>
    /// Einstellung der drei Farben für die verschiedenen Zustände der Ampel
    /// </summary>
    private void SetColors()
    {
        switch (CurrentState.LightState)
        {
            case TrafficLightStates.Stop:
                RedMaterial.color = RedActiveColor;
                YellowMaterial.color = YellowPassiveColor;
                GreenMaterial.color = GreenPassiveColor;
                break;
            case TrafficLightStates.Wait:
                RedMaterial.color = RedActiveColor;
                YellowMaterial.color = YellowActiveColor;
                GreenMaterial.color = GreenPassiveColor;
                break;
            case TrafficLightStates.Attention:
                RedMaterial.color = RedPassiveColor;
                YellowMaterial.color = YellowActiveColor;
                GreenMaterial.color = GreenPassiveColor;
                break;
            case TrafficLightStates.Go:
                RedMaterial.color = RedPassiveColor;
                YellowMaterial.color = YellowPassiveColor;
                GreenMaterial.color = GreenActiveColor;
                break;
            default:
                Debug.LogError("Unbekannter Zustand! in SetColors");
                break;
        }
    }
}
