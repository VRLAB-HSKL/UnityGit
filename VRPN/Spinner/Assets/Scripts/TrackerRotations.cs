using UnityEngine;

// Plugin für die Kommunikation mit einem VRPN-Server
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte eines Trackers abfragen.
/// 
/// Wir verwenden den in der VRPN-Distritubion
/// enthaltenen Dummy-Tracker vrpn_Tracker_Spin,
/// der als Default eine Rotation um die y-Achse
/// übertragt. Dieser Tracker ist als Default
/// unter dem Namen "Tracker1" verfügbar.
/// </summary>
public class TrackerRotations : MonoBehaviour
{
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    [Header("VRPN Server und Devices")]
    [Tooltip("VRPN Server")]
    public string VRPNServer = "localhost";
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    [Tooltip("Name des Trackers")]
    public string TrackerDevice = "Tracker1";
    /// <summary>
    /// Channel des Trackers
    /// 
    /// Damit legen wir fest, welchen Sensor wir abfragen.
    /// </summary>
    [Tooltip("Tracker Channel")]
    public int TrackerChannel = 0;

    /// <summary>
    /// Schalter, um die Rotation zu starten oder zu stoppen
    /// </summary>
    [Header("Anwendungssteuerung")]
    [Tooltip("Rotation verwenden? (Toggle mit Pause-Taste)")]
    public bool On = true;

    /// <summary>
    /// Geschwindigkeit der Rotation
    /// </summary>  
    [Tooltip("Faktor für die Rotationsgeschwindigkeit")]
    [Range(0.0f, 2.0f)]
    public float Speed = 1.0f;

    /// <summary>
    /// Orientierung der Rotation
    /// 
    /// cw oder ccw?
    /// </summary>  
    [Tooltip("Drehrichtung")]
    public bool cw = true;

    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt vrpnDevice = TrackerDevice@VRPNServer.
    /// </summary>
    private string VRPNDevice;

    /// <summary>
    /// Device-Name für VRPN zusammensetzen
    /// </summary>
    private void Awake()
    {
            VRPNDevice = TrackerDevice + "@" + VRPNServer;
            Debug.Log(">>> Awake");
            Debug.Log("-- Fracker-Device und Server > " + VRPNDevice);
            Debug.Log("<<< Awake");
    }

    /// <summary>
    /// Daten vom Server abfragen, auf der Konsole ausgeben
    /// und mit Hilfe von transform auf das GameObject anwenden.
    /// </summary>
    private void Update()
    {
        float angle;
        Vector3 axis;

        if (Input.GetKeyDown(KeyCode.Space))
            On = !On;
        
        // Neue Rotation setzen
        if (On)
        {
            Debug.Log(">>> Update");
            Quaternion trackerQuat = vrpnTrackerQuat(VRPNDevice, TrackerChannel);
            if (!cw)
                trackerQuat = Quaternion.Inverse(trackerQuat);

            Debug.Log("--  Quaternion: " + trackerQuat);
            trackerQuat.ToAngleAxis(out angle, out axis);
            Debug.Log("--  Drehwinkel : " + angle);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  trackerQuat,
                                                  Time.deltaTime * Speed);
            Debug.Log("<<< Update");
        }
    }
}
