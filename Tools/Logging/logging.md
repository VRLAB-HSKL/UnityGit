# Logging

## Quellen
Diese Darstellung und die Software ist eine Weiterentwicklung der folgenden Dokumente:

1. Manfred Brill: __Logging__, Vorlesungsimteröagem *Software Management Grundlagen*, Studiengang IT-Analyst, Wintersemester 2020/21
2. Simon Nägele: __Logging in Unity__, Projektarbeit in der Lehrveranstaltung *Augmented und Virtual Reality*, Wintersemester 2020/21


## Anforderungen
In vielen Software-Produkten macht es sinn während der Laufzeit Informationen zu protokollieren.
Das kennen wir insbesondere von httpd-Servern oder anderen im Hintergrund laufenden Software-Paketen.
Sinnvoll ist es sicher solche Ausgaben von außen konfigurieren oder aktivierbar/deaktivierbar zu machen.
Mögliche Anwendungen im Bereich von XR ist natürlich die Fehlersuche in einer Anwendung,
aber auch das Protokollieren von Events oder Daten aus dem Tracking, die wir anschließend
für die Evaluation der Anwendungen für die späteer Veröffentlichung und die Weiterentwicklung
einsetzen können.

Hier ein Auszug aus einem Text von Kernighan und Pike (Kernighan, Pike: *The Practice of Programming*, Addison 
Wesley, 1009) zu diesem Thema:
> As personal
> choice, we tend not to use debuggers beyond getting a stack trace or the
> value of a variable or two. One reason is that it is easy to get lost in details
> of complicated data structures and control flow; we find stepping through a
> program less productive than thinking harder and adding output statements
> and self-checking code at critical places. Clicking over statements takes longer
> than scanning the output of judiciously-placed displays. It takes less time
> to decide where to put print statements than to single-step to the critical section
> of code, even assuming we know where that is. More important, debugging
> statements stay with the program; debugging sessions are transient.
Solche Ausgaben bezeichnet man auch als *SOP*, von *System.out.println()*.
In Unity verwenden wir an Stelle einer *print*-Anweisung die Funktionen 
wie *Debug.Log*, die Ausgaben auf der Konsole erzeugen. 
Diese Funktionen erzeugen in einem Build Ausgaben in einer ASCII-Datei. 
Welche Ausgaben in der Datei aufgenommen werden kann in den Eigenschaften des Players
bei der KOnfiguration des Build eingestellt werden.

Die Funktion aus der Unity-Klasse *Debug* sind für einfache Fehlersuchungen
oder während der Software-Entwicklung sicher ausreichend.
Aber insbesondere für die Evaluation von Anwendungen stellen sich Anforderungen,
die über diese Funktionalität inausgehen:
- Logging-Ausgaben sollten einfach realisierbar sein.
- Wir benötigen verschiedene Stufen für Nachrichten wie *DEBUG*, *INFO* oder *FATAL*.
- Für verschiedene Komponenten unserer Anwendung sollten verschiedene Stufen verwendbar sein.
- Wir können das verwendete API zentral, möglichst extern, konfigurierbar sein.
- Logging sollte möglichst geringe Auswirkungen auf dier Perfomanz der Anwendung haben, in der protokolliert wird.

Im Java-Bereich gibt es schon sehr lange die Lösung *log4j* von Apache, die diese Anforderungen
weitgehend erfüllt. Es gibt auch modernere Lösungen wir LOGBack oder SLF4J.

Apache bietet inzwischen die gleiche Funktionalität unter der Bezeichnung *Logging Services* für sehr viele Programmiersprachen an. 
für C# steht die Bibliothek *log4Nnet* zur Verfügung: <http://logging.apache.org/log4net/>.


## Logging Level
Es gibt Meldungen die wir auf jeden Fall ausgeben möchten, weil ein Event aufgetreten ist das zu Beendigung der Anwendung
oder anderen fatalen Umständen führt. Andere Ausgaben sollen nur für die Evaluation oder bei der Entwicklung der Anwendung
ausgegeben werden. Natürlich kann man dies mit der *Debug*-Klasse von Unity erledigen. Aber diese Ausgaben haben die Eigenschaft,
dass wir sie sehr leicht in der Anwendung belassen und in einem Build einsetzen, in dem diese Ausgaben nicht mehr nötig
sind und im schlimmsten Fall die Performanz beeinträchtigen.

Hier sind die __Levels__ oder __Stufen__ in einem API wie *log4net* von großem Nutzen. Diese Stufen sind immer hierarchisch
geordnet. Es gibt eine Rangliste von *keine Ausgabe* bis hin zu *alle Ausgaben*.
Die verfügbaren Stufen unterscheiden sich von API zu API. In *log4net* stehen die folgenden Stufen zur Verfügung:

+ `ALL`
+ `DEBUG`
+ `INFO`
+ `WARN`
+ `ERROR`
+ `FATAL`
+ `OFF`

Diese Liste ist in aufsteigender Priorität geordnet. 

### Beispiel
Verwenden wir in einer Klasse Ausgaben der Stufen `DEBUG` aus und ist extern konfiguriert, dass wir die Stufe `INFO` verwenden,
dann erscheinen die Ausgabe aus dieser Klasse nicht in der Logging-Ausgabe. Die Stufe `ALL`hat die geringste,
`OFF`die höchste Priorität.

## log4net in einem Unity-Projekt
Um *log4net* in einem Unity-Projekt zu verwenden fügen wir die dll-Datei *log4net.dll* dem Projekt hinzu. Dazu kopieren wir
diese Datei in das Verzeichnis __Assets/Plugins__. Es gibt auch XML-Dateien für die Konfiguration des Loggings von außen.
Beispiele für diese konfigurations-Dateien finden wir in diesem Projekt.
Die Datei __log4net.xml__ kopieren wir in das Verzeichnis __Assets__. In das Verzeichnis __Assets/Resources__
kopieren wir die Datei __Log4NetConfig.xml__.


## Logging-Ausgaben
Einer Klasse in der wir Logging-Ausgabe hinzufügen möchten fügen wir mit `using log4net;` die nötigen Deklarationen hinzu.

Die Ausgaben werden mit Instanzen der Klasse __ILog__ hinzu. Als Beispiel finden wir in diesem Unity-Projekt
in der Klasse __MoveTowards__ das private member __Log__, das so vereinbart wird: 

    private static readonly ILog Log = LogManager.GetLogger(typeof(MoveTowards));

Man kann das Argument __typeof(MoveTowards)__ auch weglassen, dann verwendet man den sogenannten *Root-Logger*. 
Wir verwenden jedoch __immer__ solche Logger wie im Beispiel. Mit Hilfe des Klassennamens können wir diesen Logger
für diese Klasse von außen konfigurieren. Wir können mit dieser Systematik eine Hierarchie von Loggern
erzeugen, die wir extern auch hierarchisch konfigurieren können.

Die Ausgaben für die verschiedenen Stufen können wir jetzt mit Funktionen erzeugen, die nach den verschiedenen Stufen benannt sind.
Dabei verwenden wir als Konvention, dass wir den Ein- und Austritt in eine Funktion immer mit entsprechenden Ausgaben
der Stufe `DEBUG`protokollieren. Hier ein Beispiel aus der Funktion __Start__ der Klasse __MoveTowards__, die
drei Ausgaben erzeugt:

    private void Start()
    {
        Log.Debug(">>> Start");
        Log.Info("Info-Ausgabe");
        Log.Debug("<<< Start");
    }

In der __FixedUpdate()__-Funktion der gleichen Klasse finden wir die Ausgaben von Koordinaten eines Objekts:

    Log.Info("Neue Position des Objekts: " + transform.position.ToString());
  
## Konfiguration
Konfigurationen für die Logger können wir mit *log4net* auf verschiedene Weisen erstellen. Wir können
die Konfiguration im Quell-Code der Klasse durchführen. Oder wir verwenden externe Dateien
im XML-, JSON- oder YAML-Format. Dabei ist eine Reihenfolge definiert, in der solche Konfigurationen
angewendet werden. 
Vorrang haben Konfigurationen im Quelltext des Projekts. 
Gibt es diese nicht, dann wird zuerst nach einer Datei `log4net.json` gesucht. Erst wenn
diese nicht gefunden wird erfolgt eine Suche nach `log4net.xml`. Werden keine solchen Dateien gefunden
wird eine Default-Konfiguration verwendet.

Klassen die Logging-Ausgaben erzeugen werden _Appender__ genannt. Es gibt solche __Appender__, die
von `log4net` zur Verfügung gestellt werden. Dies sind Klassen, die auf der System-Konsole ausgeben und 
insbesondere sogenannte __FileAppender__, mit denen wir in eine ASCII-Datei ausgeben können. Die
Konsolen-Ausgaben nutzen in einem  Unity-Projekts nichts, dort geben wir auf die Unity-Konsole aus.
Aber die __FileAppender__ sind wie wir sehen werden von großem Nutzen.

Dar+ber hinaus können wir eigene __Appender__ implementieren, in den wir beispielsweise mit Hilfe
der Unity-Klasse `Debug`Ausgaben auf der Unity-Konsole erzeugen können. Oder wir implementieren eigene
__FileAppender__.

Hier ein Beispiel für die Konfiguration der Logger im Quelltext einer von `MonoBehaviour`abgeleiteten Klasse:

   public static void ConfigureAllLogging()
    {
        var patternLayout = new PatternLayout
        {
            ConversionPattern = "%date %timestamp %-5level - %message%newline"
        };
        patternLayout.ActivateOptions();
  
        var unityLogger = new UnityAppender
        {
            Layout = new PatternLayout()
        };
        unityLogger.ActivateOptions();
    }
    
Wichtig ist hier das __ConversionPattern__, mit dem wir das Format der Ausgaben einstellen können. 
Auf der Website von *log4net* finden wir eine Dokumentation dieser Formate. Mit __%date__ geben
wir das Datum und die Uhrzeit aus, mit __%timestamp__ die Zeit in Millisekunden seit Start
der Anwendung. __&level__ dokumentiert die verwendete Stufe und mit __%message__ geben wir den Text
aus, den wir der Log-Funktion übergeben haben.

Diese konfiguration können wir auch in einer XML-Datei durchführen. Dabei konfigurieren wir
die verwendeten Appender und auch die in der Hierarchie verfügbaren Logger:

     <log4net> 
       
        <!--CustomAppender-->
       <appender name="UnityConsoleAppender" type="UnityConsoleAppender">
         <stringProperty name="MyProperty"></stringProperty>
         <layout type="log4net.Layout.PatternLayout,log4net">
            <param name="ConversionPattern" value="%timestamp %level %class: %message%newline"/>
         </layout>
      </appender>
      
      <!-- Log4net interne appender-->
      <appender name="FileAppender" type="log4net.Appender.FileAppender">
         <file value=".\Logs\loggingExample.txt" />
         <appendToFile value="true" />
         <layout type="log4net.Layout.PatternLayout">
           <conversionPattern value="%timestamp %level %class: %message%newline" />
         </layout>
      </appender>
     
     <logger name="PlayerControl">
        <level value="DEBUG"/>
     </logger>
    
    <logger name="MoveTowards">
       <level value="INFO"/>
    </logger>
    
    <root>
       <level value="FATAL"/>
       <appender-ref ref="UnityConsoleAppender"/>
    </root>
   
    </log4net>
    
Hier erkennen wir die Konfigurationen von Appendern. Dabei gehen wir davon aus, dass es im Projekt einer Klasse __UnityConsoleAppender__ gibt.
Im Projekt finden Sie diese Klasse im Verzeichnis *LoggingScripts*. Diese Klasse verwendet die Unity-Klasse 'Debug' und erzeugt Ausgaben
auf der Unity-Konsole.

In zwei Klassen im Projekt, in `PlayerControl' und in ' MoveTowards' sind __ILog__-Instanzen erzeugt worden. Wir können die zu verwendende
Stufe so extern verändern und damit die Ausgaben steuern. Alle weiteren Einstellungen erfolgen in der Sektion
mit dem `root`-Tag. Hier stellen wir ein, dass außerhalb der beiden Klassen alle anderen Logger die Stufe `FATAL` verwenden und
dass wir den __UnityConsoleAppender__ einsetzen.


![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)

