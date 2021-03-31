using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using log4net;

/// <summary> 
/// 
/// Klasse kann verwendet werden, um Logger-Konfigrationsänderungen
/// zur Laufzeit zu demonstrieren
/// </summary>

public class Updater : MonoBehaviour
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Updater));
    //private static readonly ILog Log2 = LogManager.GetLogger("TEST");
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Log.Warn("Updated with Warning");
        Log.Fatal("Updated with Fatal");
        //Log2.Info("Updated TESTLogger");
    }
}
