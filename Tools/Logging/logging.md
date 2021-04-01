# Logging

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

Diese Hierarchie machen wir uns in der folgenden Tabelle nochmals klar:

| Stufe |  Externe Konfiguration |   
| ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- |


|  | `ALL` | `DEBUG` | `INFO` | `WARN`| `ERROR`| `FATAL` | `OFF` |
| `ALL` | Ja | Nein | Nein | Nein | Nein | Nein | Nein |
| `DEBUG`| Ja | Ja| Nein | Nein | -Nein | Nein | Nein |


![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)

