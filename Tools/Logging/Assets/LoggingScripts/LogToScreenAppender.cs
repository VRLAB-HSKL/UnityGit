using UnityEngine;
using log4net.Appender;
using log4net.Core;
using System.Collections;
using UnityEngine.UI;


public class LogToScreenAppender : AppenderSkeleton
{

    Text logText;
    //0 = oberste, älteste msg.
    string msg0 = "";
    string msg1 = "";
    string msg2 = "";
    string msg3 = "";
    string msg4 = "";
    string log;

    //Debug.Log(logText);
    protected override void Append(LoggingEvent loggingEvent)
    {
        string message = RenderLoggingEvent(loggingEvent);
        
        WriteToScreen(message);
    }

    void WriteToScreen(string message) {
        logText = GameObject.Find("LoggerOutput").GetComponent<Text>();
        //logText.text = "";
        msg0 = msg1;
        msg1 = msg2;
        msg2 = msg3;
        msg3 = msg4;
        msg4 = message;
        log = msg0 +"\n" + msg1 + "\n" + msg2 + "\n" + msg3 + "\n" + msg4 + "\n";
        logText.text = log;
        
    }


}