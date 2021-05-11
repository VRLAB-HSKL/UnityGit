using UnityEngine;
using VRKL.MBU;

/// <summary>
/// Manager für die Demo der State Machine.
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
public class TrafficLightView : Observer
{
    /// <summary>
    /// Integer Counter für Rot
    /// </summary>
    [Tooltip("Integer Counter für rot")]
    [Range(10, 300)]
    public int TimeForStop = 60;
    /// <summary>
    /// Integer Counter für Rot und Gelb
    /// </summary>
    [Tooltip("Integer Counter für rot und gelb")]
    [Range(10, 300)]
    public int TimeForWait = 60;
    /// <summary>
    /// Integer Counter für Grün
    /// </summary>
    [Tooltip("Integer Counter für grün")]
    [Range(10, 300)]
    public int TimeForGo = 60;
    /// <summary>
    /// Integer Counter für Gelb
    /// </summary>
    [Tooltip("Integer Counter für gelb")]
    [Range(10, 300)]
    public int TimeForAttention = 60;

    /// <summary>
    /// Sollen die Zustandsklassen eine Ausgabe auf der Konsole durchführen?
    /// </summary>
    [Tooltip("Log Ausgaben?")]
    public bool LogOutput = false;

    /// <summary>
    /// Farbe für aktives rotes Licht
    /// </summary>
    private Color RedActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives rotes Licht
    /// </summary>
    private Color RedPassiveColor { get; set; }
    /// <summary>
    /// Farbe für aktives gelbes Licht
    /// </summary>
    private Color YellowActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives gelbes Licht
    /// </summary>
    private Color YellowPassiveColor { get; set; }
    /// <summary>
    /// Farbe für aktives grünes Licht
    /// </summary>
    private Color GreenActiveColor { get; set; }
    /// <summary>
    /// Farbe für passives grünes Licht
    /// </summary>
    private Color GreenPassiveColor { get; set; }

    /// <summary>
    /// GameObject für das rote Licht der Ampel
    /// </summary>
    public GameObject Red { get; set; }
    /// <summary>
    /// Material des roten Lichts
    /// </summary>
    private Material RedMaterial { get; set; }
    /// <summary>
    /// GameObject für das gelbe Licht der Ampel
    /// </summary>
    public GameObject Yellow { get; set; }
    /// <summary>
    /// Material des gelben Lichts
    /// </summary>
    private Material YellowMaterial { get; set; }
    /// <summary>
    /// GameObject für das grüne Licht der Ampel
    /// </summary>
    public GameObject Green { get; set; }
    /// <summary>
    /// Material des grünen Lichts
    /// </summary>
    private Material GreenMaterial { get; set; }

    /// <summary>
    /// Das beobachtete Objekt - eine Verkehrsampel
    /// </summary>
    private TrafficLight Model;

    /// <summary>
    /// Verbindungen herstellen
    /// 
    /// Die GameObjects, die die drei Leuchten der Ampel
    /// ausgeben werden als eine Hierarchie erwartet.
    /// Die Wurzel heißt "Ampel", und die drei Leuchten
    /// wie zu erwarten "Rot", "Gelb" und "Grün". Sie werden
    /// mit <code>GameObject.Find</code> mit ihrem Namen, z.b.
    /// "Ampel/Rot", abgefragt.
    /// </summary>
    private void Awake()
    {
        // Das Subject erzeugen und die View-Klasse registrieren
        Model = new TrafficLight(TimeForStop, TimeForWait, 
                                 TimeForGo, TimeForAttention,
                                 StateStop.Instance,
                                 LogOutput);
        Model.Attach(this);

        // Wir suchen nach GameObjects mit den Namen
        // Rot, Gelb, Grün, die Kinder des GameObjects
        // mit dem Namen Ampel.
        Red = GameObject.Find("Ampel/Rot");
        Yellow = GameObject.Find("Ampel/Gelb");
        Green = GameObject.Find("Ampel/Grün");

        // Materialkomponenten dieser GameObjects abfragen
        RedMaterial = Red.GetComponent<Renderer>().material;
        YellowMaterial = Yellow.GetComponent<Renderer>().material;
        GreenMaterial = Green.GetComponent<Renderer>().material;

        // Materialien aus den Resourcen laden
        TrafficColors();
    }

    /// <summary>
    /// Wir zählen den Counter hoch
    /// und geben eine Statusmeldung aus.
    /// </summary>
    private void FixedUpdate()
    {
        Model.Update();   
    }

    /// <summary>
    /// Die grafische Ausgabe durchführen, falls sich das Subject verändert hat
    /// </summary>
    /// <returns></returns>
    public override void Refresh()
    {
        switch (Model.CurrentState.LightState)
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
}
