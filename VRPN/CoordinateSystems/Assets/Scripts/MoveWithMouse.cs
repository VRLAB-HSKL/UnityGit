using UnityEngine;
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte der Maus für die Bewegung eines Objekts
/// in der xz-Ebene verwenden.
/// </summary>
public class MoveWithMouse : MonoBehaviour
{
    [Header("VRPN Server und Devices")]
    [Tooltip("Server")]
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    public string VRPNServer = "localhost";
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>   
    [Tooltip("Name der Maus")]
    public string MouseDevice = "Mouse0";
    /// <summary>
    /// Faktoren, mit dem wir die Bewegung skalieren
    /// </summary>
    [Header("Faktoren für die Bewegung")]
    [Tooltip("Multiplikator für die x-Achse")]
    [Range(1.0f, 20.0f)]
    public float FactorX = 12.0f;
    [Tooltip("Multiplikator für die z-Achse")]
    [Range(1.0f, 20.0f)]
    public float FactorZ = 8.0f;

    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt vrpnDevice = MouseDevice@vrpnServer.
    /// </summary>
    private string vrpnDevice;

    /// <summary>
    /// Konstanten für die Maustasten und die Taste mit der Zahl 1
    /// </summary>
    private const int mouseleft = 0,
        mousemiddle = 1,
        mouseright = 2,
        keyOne = 2;

    // <summary>
    /// Konstanten für die Achsen der Maus als analoges Gerät
    /// </summary>
    private const int mouseX = 0, mouseY = 1;

    /// <summary>
    /// Device-Name für VRPN zusammensetzen
    /// </summary>
    private void Awake()
    {
        // Device-Name zusammensetzen
        vrpnDevice = MouseDevice + "@" + VRPNServer;
            Debug.Log(">>> Awake");
            Debug.Log("-- VRPN Maus > " + vrpnDevice);
            Debug.Log("<<< Awake");
    }

    private void Update()
    {
        double xValue, yValue;
        bool leftPressed = false;
        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);

        // Werte abholen
        xValue = vrpnAnalog("Mouse0@localhost", mouseX);
        yValue = vrpnAnalog("Mouse0@localhost", mouseY);
        leftPressed = vrpnButton("Mouse0@localhost", mousemiddle);
        
        if (leftPressed)
        {
            move.x = FactorX*(2.0f*(float)xValue-1.0f);
            move.z = - FactorZ * (2.0f * (float)yValue - 1.0f);
            transform.position = move;
        }
    }
}
