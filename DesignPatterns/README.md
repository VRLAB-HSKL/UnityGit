# Design Pattern in C# und Unity
Design Patterns wie Observer, Command oder State sind bei der Beschäftigung mit interaktiven Systemen entstanden. Mit solchen 
Entwurfsmustern können wir Code wiederverwenden und Anwendungen extrem gut strukturieren. Die Beispiele in diesem Verzeichnis
zeigen die Implementierung und Anwendung von Patterns.

## Pattern und Demos
Implementiert sind aktuell die folgenden Design Pattern:
- Singleton
- State
- Observer
- MVC 

Die Implementierungen folgen dabei immer dem 'Gang-of-Four'-Buch von Erich Gamma et. al., "Design Patterns" 
aus dem Addison Wesley Verlag.

### MVC und Observer
Eine Uhr ist ein hervorragendes Beispiel für die Anwendung von MVC und Observer. Die View für die Uhrzeit
kann eine digitale Anzeige, aber auch eine klassische Anzeige mit Uhrzeigern sein.

- MVC finden wir in der Demo 'ClockMVC'.
- Observer finden wir in der Demo 'Clock Observer'.
- Observer (in Verbindung mit State) findet man in der Demo 'TrafficLightObserver'.


### State
Der Kern so gut wie jeder Game Engine ist eine State Machine. Das klassische Beispiel, nicht nur bei Gamma, ist eine
Verkehrsampel.

- State Pattern für eine Verkehrsampel finden wir in der Demo 'StatePattern'.
- State Pattern und Observer für die Verkehrsampel finden wir in der Demo 'TrafficLightObserver'.

## Unity-Version
Alle Anwendungen verwenden (Stand Februar 2021) die Version Unity 2019.4.10f1 LTS.


![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)

