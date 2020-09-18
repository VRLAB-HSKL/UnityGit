using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte der Maus ausgeben!
/// </summary>
public class SimpleVRPN : MonoBehaviour
{
    void Update()
    {
        double xValue, yValue;
        bool leftPressed = false;

        // Werte abholen
        xValue = vrpnAnalog("Mouse0@localhost", 0);
        yValue = vrpnAnalog("Mouse0@localhost", 1);

        Vector3 trackerPos = vrpnTrackerPos("Tracker0@localhost", 0);
        Quaternion trackerQuat = vrpnTrackerQuat("Tracker0@localhost", 0);

        leftPressed = vrpnButton("Mouse0@localhost", 0);
        
        Debug.Log("Button 0 an Mouse0 gedrückt!");

        Debug.Log("x:" + xValue);
        Debug.Log("y:" + yValue);

        Debug.Log("Trackerposition:" + trackerPos);
        Debug.Log("Tracker Quaternion:" + trackerQuat);
    }
}
