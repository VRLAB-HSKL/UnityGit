# Polygonale Netze
Unity enthält eine Reihe von Grundkörpern und wir können polygonale Netze in verschiedenen
Formaten importieren. Unity bietet aber auch die Möglichkeit in C# poloygonale Netze zu definieren.
Dabei verwendet Unity die klassische Methode, die Geometrie und die Topologie zu speichern, als Eckenliste.
Mehr zu Eckenlisten findet man in Brill, Bender: Computergrafik, Hanser Verlag, 2006 im Kapitel
*Polygonale Netze*. In einem Advanced API scheint Unity auch das Konzept der Vertex Arrays zu unterstützen,
dies wurde bisher aber noch nicht ausgenutzt (das sollte aber aus Performanzgründen unbedingt angegangen werden).

Hier eine Tabelle mit den Demos und den Themen, die behandelt werden:

| Demo | Behandelte Themen |
| ---- | --------------- |
| PolyMeshSimple | Ein Dreieck als einfache Eckenliste in C# erzeugen und darstellen |
| PolyMesh | Erweiterung des Projekts PolyMeshSimple um mehr Punkte, wahlweise mit Flat Shading |
| PolyModel | Eckenliste mit Ecken, die aus Unity-Objekten in der Szene erstellt wird und die interaktiv verschiebbar sind |
| PolyNormals | Eckenliste mit selbst zugewiesenen Normalenvektoren |
| PolyNormalsInspector | Eckenliste mit Normalenvektoren, die im Inspektor definiert werden können |
| -------- | ---------------- | ------------------ |


![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)

