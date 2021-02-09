using UnityEngine;


/// <summary>
/// Eine Kamera für "Fly"
/// Die Klasse realisiert zwei verschiedene Möglichkeiten
/// - Travelling mit Starrkörpertransformationen in Transform
/// - Travelling mit Physik und Kollisiionen
/// 
/// Die Klasse setzt voraus, dass das GameObject, dem die Klasse
/// als Komponente hinzugefügt wird eine Komponente Camera besitzt.
/// Dies wird mit RequireComponent sichergestellt.
/// 
/// Wir verwenden nicht RequireComponent für Rigidbody, da
/// wir dies nur für den Mode Physics benötigen.
/// In diesem Fall wird in Start() eine solche Komponente hinzugefügt!
/// Das gleiche gilt für den in diesem Fall benötigten Box-Collider.
/// <remarks> 
/// Autor: Patrick Schwarz, Wintersemester 2014/15
/// Projekt Studiengang Informatik
/// 
/// Änderungen: Manfred Brill, Sommersemester 2020
/// </remarks>
/// </summary>
[AddComponentMenu("Custom/VR/FlyingCamera")]
[RequireComponent(typeof(Camera))]
public class FlyingCamera : MonoBehaviour
{
    /// <summary>
    /// Enum für die möglichen Richtungen der Bewegung
    /// </summary>
    public enum Movement { Horizontal, Vertical, Both }

    /// <summary>
    /// Enum für die Realisierung der Kamera
    /// 
    /// Ghost: auf der Basis von Starrkörpertransformationen. Kamera kann sich durch Objekte durchbewegen.
    /// Physics: verwendet Physik. Kamera kollidiert mit Objekten.
    /// </summary>
    public enum Mode
    {
        Ghost,
        Physics
    }

    /// <summary>
    /// Enum für die erlaubten Rotationen
    /// </summary>
    public enum Rotation { None, Left_Right, Up_Down, Both }

    /// <summary>
    /// Geschwindigkeit für die Bewegung der Kamera
    /// </summary>
    [Header("Velocity and Sensitivity")]
    [Tooltip("Multiplikator für die Geschwindigkeit")]
    [Range(1.0f, 1000.0f)]
    public float movementSpeed = 10.0f;

    /// <summary>
    /// Multiplikator für die Mausbewegung
    /// </summary>
    [Tooltip("Multiplikator für die Mausbewegung")]
    [Range(1.0f, 100.0f)]
    public float mouseSensitivity = 10.0f;

    [Header("Movement Options")]
    [Tooltip("Ghost: Durch Objekte durchfliegen. Keine Physik.\nPhysics: Fliegen mit Physik. Kamera löst Kollisionen aus.")]
    /// <summary>
    /// Art der Kamera
    /// 
    /// Default: Ghost
    /// </summary>
    public Mode mode = Mode.Ghost;

    /// <summary>
    /// Mögliche Bewegungen.
    /// </summary>
    public Movement allowedMovement = Movement.Both;

    /// <summary>
    ///  Mögliche Rotationsachsen
    /// </summary>
    /// <remarks>
    /// Restriktionen sind immer nützlich.
    /// </remarks>/// 
    public Rotation allowedRotation = Rotation.Both;

    /// <summary>
    /// Sollen Achsen "invertiert" werden? Welche?
    /// </summary>
    public Rotation invertAxes = Rotation.None;

    /// <summary>
    /// Axis für das Input-System von Unity
    /// </summary>
    private const string HorizontalMovementAxis = "Horizontal";
    /// <summary>
    /// Axis für das Input-System von Unity
    /// </summary>
    private const string VerticalMovementAxis = "Vertical";
    /// <summary>
    /// Axis für das Input-System von Unity
    /// </summary>
    private const string HorizontalRotationAxis = "Mouse X";
    /// <summary>
    /// Axis für das Input-System von Unity
    /// </summary>
    private const string VerticalRotationAxis = "Mouse Y";

    /// <summary>
    /// Rigidbody Komponente für die Physik
    /// </summary>
    private Rigidbody rigidbody = null;

    /// <summary>
    /// Setup
    /// 
    /// Falls wir Physik verwenden benötigen wir einen Box-Collider
    /// und eine Rigidbody-Komponente.
    /// </summary>
    public void Start()
    {
        if(mode == Mode.Physics)
        {
            this.rigidbody = gameObject.AddComponent<Rigidbody>();
            this.rigidbody.useGravity = false;
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    /// <summary>
    /// Wir überschreiben FixedUpdate für die Bewegung, da wir Time.deltaTime verwenden.
    /// </summary>
    /// <remarks>
    /// To Do: Statt switch Anweisung virtuelle Klasse und davon abgeleitet
    /// zwei Klassen, einmal Ghost und einmal Phyisics!
    /// </remarks>
    public void FixedUpdate()
    {
        // Bewegungsrichtung berechnen
        var dir = movementDirection();
        // Orientierung der Bewegung berechnen
        var newAngles = rotationAngles();

        switch(mode)
        {
            case Mode.Ghost: {
                    transform.eulerAngles = newAngles;
                    transform.Translate(movementSpeed * Time.deltaTime * dir);
                    break;
                }
            case Mode.Physics: {
                    this.rigidbody.velocity = Vector3.zero;
                    this.rigidbody.angularVelocity = Vector3.zero;
                    this.rigidbody.MoveRotation(Quaternion.Euler(newAngles));
                    this.rigidbody.AddRelativeForce(dir * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    break;
                }
        }
    }

    /// <summary>
    /// Die Bewegungsrichtung an Hand der Eingabe im Inputsystem von Unity berechnen.
    /// </summary>
    /// <returns>
    /// Bewegungsrichtung als Vector3-Instanz
    /// </returns>
    private Vector3 movementDirection()
    {
        var horizontal = 0.0f;
        var vertical = 0.0f;

        if (allowedMovement == Movement.Horizontal || allowedMovement == Movement.Both) 
            horizontal = Input.GetAxis(HorizontalMovementAxis);
        
        if (allowedMovement == Movement.Vertical || allowedMovement == Movement.Both)
            vertical = Input.GetAxis(VerticalMovementAxis);

        var dir = new Vector3(horizontal, 0.0f, vertical);
        dir.Normalize();

        return dir;
    }

    //! 
    /// <summary>
    /// Orientierung der Bewegung auf der Basis der Mausbewegung.
    /// 
    /// Die Mausbewegungen werden mit dem Dämpfungsfaktor multipliziert,
    /// um die Sensitivität zu dämpfen.
    /// 
    /// Wir verwenden Eulerwinkel.
    /// </summary>
    /// <returns>
    /// Orientierungen als Instanz von Vector3.
    /// </returns>
    private Vector3 rotationAngles()
    {
        var mouseX = 0.0f;
        var mouseY = 0.0f;

        if (allowedRotation == Rotation.Left_Right || allowedRotation == Rotation.Both)
        {
            mouseX = Input.GetAxis(HorizontalRotationAxis);

            if(invertAxes == Rotation.Left_Right || invertAxes == Rotation.Both)
                mouseX = -mouseX;

            mouseX *= mouseSensitivity;
        }

        if (allowedRotation == Rotation.Up_Down || allowedRotation == Rotation.Both)
        {
            mouseY = Input.GetAxis(VerticalRotationAxis);

            if(invertAxes == Rotation.Up_Down || invertAxes == Rotation.Both)
                mouseY = -mouseY;

            mouseY *= mouseSensitivity;
        }

        return new Vector3(transform.eulerAngles.x - mouseY, transform.eulerAngles.y + mouseX, 0);
    }
}
