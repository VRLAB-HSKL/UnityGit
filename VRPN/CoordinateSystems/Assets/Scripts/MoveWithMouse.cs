using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte der Maus für die Bewegung eines Objekts
/// in der xz-Ebene verwenden.
/// </summary>
public class MoveWithMouse : MonoBehaviour
{
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    public string MouseDevice = "Mouse0";
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    public string vrpnServer = "localhost";
    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt vrpnDevice = trackerDevice@vrpnServer.
    /// </summary>
    private string vrpnDevice;
    /// <summary>
    /// Faktor, mit dem wir die Bewegung skalieren
    /// </summary>
    public float factorx = 4.0f;
    public float factorz = 2.0f;

    /// <summary>
    /// Device-Name für VRPN zusammensetzen
    /// </summary>
    private void Awake()
    {
        // Device-Name zusammensetzen
        vrpnDevice = MouseDevice + "@" + vrpnServer;
            Debug.Log(">>> Awake");
            Debug.Log("-- Awake: > " + vrpnDevice);
            Debug.Log("<<< Awake");
    }

    void Update()
    {
        double xValue, yValue;
        bool leftPressed = false;
        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);

        // Werte abholen
        xValue = vrpnAnalog("Mouse0@localhost", 0);
        yValue = vrpnAnalog("Mouse0@localhost", 1);
        leftPressed = vrpnButton("Mouse0@localhost", 0);
        
        if (leftPressed)
        {
            move.x = factorx*(2.0f*(float)xValue-1.0f);
            move.z = - factorz * (2.0f * (float)yValue - 1.0f);
            transform.position = move;
        }
    }
}
