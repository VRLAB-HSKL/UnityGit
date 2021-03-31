using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using log4net;

/// <summary> 
/// Klasse zur Demonstration von ExceptionHandling
/// mit log4net.
/// </summary>

public class Log4NetExceptionDemo : MonoBehaviour
{
    // Start is called before the first frame update
    private static readonly ILog Log = LogManager.GetLogger(typeof(Log4NetExceptionDemo));
    void Start()
    {
        int[] numbers = new int[] { 1, 2 };
        
        try
        {
            int crash = numbers[4];
        }
        catch (System.IndexOutOfRangeException e) {
            Log.Fatal(e);
        }
    }
}
