using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte der Maus und Tastatus 
/// auf der Konsole ausgeben!
/// </summary>
public class SimpleVRPN : MonoBehaviour
{
    public string mouseDevice = "Mouse0@localhost";
    public string keyboardDevice = "Keyboard0@localhost";
    /// <summary>
    /// Ausgabe der analogen Werte auf der Konsole starten und stoppen.
    /// </summary>
    private bool printAnalogValues = false;
    /// <summary>
    /// Ausgabe der Information über eine gedruckte Maustaste auf der Konsole starten und stoppen.
    /// </summary>
    private bool printButtonValues = false;

    void Start()
    {

    }
    void Update()
    {
        double xValue, yValue;
        bool leftPressed = false;
        bool middlePressed = false;
        bool rightPressed = false;
        bool onePressed = false;

        // Mit der Pausetaste die Konsolenausgaben starten und stoppen
        if (Input.GetKeyUp(KeyCode.A))
        {
            printAnalogValues = !printAnalogValues;
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            printButtonValues = !printButtonValues;
        }

        // Werte abholen
        xValue = vrpnAnalog(mouseDevice, 0);
        yValue = vrpnAnalog(mouseDevice, 1);

        leftPressed = vrpnButton(mouseDevice, 0);
        middlePressed = vrpnButton(mouseDevice, 1);
        rightPressed = vrpnButton(mouseDevice, 2);

        // Die Taste 1 hat den Scan-Code 1
        onePressed = vrpnButton(keyboardDevice, 2);

        // Werte auf der Konsole ausgeben
        if (printButtonValues)
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

        if (printAnalogValues)
        {
            Debug.Log("Mauskoordinaten");
            Debug.Log("x:" + xValue);
            Debug.Log("y:" + yValue);
        }
       
    }
}
