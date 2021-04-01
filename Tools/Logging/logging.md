# Logging
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



![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)

