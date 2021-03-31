using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using log4net;

/// <summary> 
/// Klasse/n zur Demonstration von log4net
/// über mehr als eine Klasse
/// 
/// </summary>

public class MultiClassDebuggingExample : MonoBehaviour
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(MultiClassDebuggingExample));
    void Start()
    {
        Log.Info("Trying to create Triangles...");
        Triangle tria = new Triangle(100, 40, 40);
        Triangle trib = new Triangle(50, 50, 50);
        Log.Info("End of Triangle Creation..");

        int sum = tria.GetAngleSum();
        Log.Info("WinkelSumme: "+ sum);
    }
}

class Triangle {
    private static readonly ILog TriangleLog = LogManager.GetLogger(typeof(Triangle));

    int angleSum;

    //Winkel werden übergeben
    public Triangle(int a, int b, int c) {
        if (a + b + c != 180)
        {
            TriangleLog.Error("Impossible Triangle!");
        }
        else {
            TriangleLog.Info("Triangle created");
        }
        this.angleSum = a + b + c;
    }

    public int GetAngleSum() {
        TriangleLog.Info("WinkelSumme: " + angleSum);
        return angleSum;
    }
}

