using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRPN;

/// <summary>
/// Ein Hello World für die Verwendung eines VRPN Servers in Unity.
/// 
/// Wir verwenden das Plugin VRPN.
/// </summary>
public class SimpleVRPN : MonoBehaviour
{
    [Header("VRPN Server und Devices")]
    [Tooltip("Server")]
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    public string vrpnServer = "localhost";
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>   
    [Tooltip("Name der Maus")]
    public string MouseDevice = "Mouse0";
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    [Tooltip("Name des Keyboards")]
    public string KeyboardDevice = "Keyboard0";

    /// <summary>
    /// Ausgabe der analogen Werte auf der Konsole starten und stoppen.
    /// </summary>
    [Header("Ausgaben auf der Konsole")]
    [Tooltip("Mauskoordinaten ausgeben (Toggle mit 'a')")]
    public bool PrintAnalogValues = false;
    /// <summary>
    /// Ausgabe der Information über eine gedruckte Maustaste auf der Konsole starten und stoppen.
    /// </summary>
    [Tooltip("Buttons ausgeben (Toggle mit 'b')")]
    public bool PrintButtonValues = false;

    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt mouse = MouseDevice@vrpnServer.
    /// </summary>
    private string mouse;
    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt keyboard = KeyboardDevice@vrpnServer.
    /// </summary>
    private string keyboard;

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
        mouse = MouseDevice + "@" + vrpnServer;
        keyboard = KeyboardDevice + "@" + vrpnServer;
        Debug.Log(">>> Awake");
        Debug.Log("-- VRPN Devices > " + mouse);
        Debug.Log("-- VRPN Devices > " + keyboard);
        Debug.Log("<<< Awake");
    }

    /// <summary>
    /// VRPN Geräte abfragen und, falls gewünscht,
    /// Informationen auf der Konsole ausgeben.
    /// </summary>
    private void Update()
    {
        double xValue, yValue;
        bool leftPressed=false, 
             middlePressed=false, 
             rightPressed=false,
             onePressed = false;

        // Mit der Pausetaste die Konsolenausgaben starten und stoppen
        if (Input.GetKeyUp(KeyCode.A))
        {
            PrintAnalogValues = !PrintAnalogValues;
            Debug.Log("Toggle für analoge Werte!");
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            PrintButtonValues = !PrintButtonValues;
            Debug.Log("Toggle für Button-Werte!");
        }

        // Werte abholen
        xValue = vrpnAnalog(mouse, mouseX);
        yValue = vrpnAnalog(mouse, mouseY);

        leftPressed = vrpnButton(mouse, mouseleft);
        middlePressed = vrpnButton(mouse, mousemiddle);
        rightPressed = vrpnButton(mouse, mouseright);

        // Die Taste 1 hat den Scan-Code 1
        onePressed = vrpnButton(keyboard, keyOne);

        // Werte auf der Konsole ausgeben
        if (PrintButtonValues)
        {
            if (leftPressed)
                Debug.Log("Linke Maustaste an Mouse0 gedrückt!");
            if (middlePressed)
                Debug.Log("Mittlere Maustaste an Mouse0 gedrückt!");
            if (rightPressed)
                Debug.Log("Rechte Maustaste an Mouse0 gedrückt!");
            if (onePressed)
                Debug.Log("Die Taste 1 auf dem Keyboard gedrückt!");
        }

        if (PrintAnalogValues)
        {
            Debug.Log("Mauskoordinaten");
            Debug.Log("x:" + xValue);
            Debug.Log("y:" + yValue);
        }      
    }
}
