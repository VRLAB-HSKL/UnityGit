using UnityEngine;

/// <summary>
/// Bewegen eines Objekts mit den Cursortasten und
/// daraus erstellten Geschwindigkeitsvektoren
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Maximaler Betrag der Geschwindigkeit
    /// </summary>
    [Tooltip("Maximale Schrittweite")]
    [Range(1.0f, 1000.0f)]
    public const float MAX_MOVE_DISTANCE = 500.0f;
    /// <summary>
    /// y-Koordinate des Objekts, die konstant gelassen wird
    /// </summary>
	private float y;
    /// <summary>
    /// Rigidbody-Komponente des Objekts
    /// </summary>
    private Rigidbody rbComponent;
    /// <summary>
    /// Geschwindigkeit
    /// </summary>
	private float m_speed = 20.0f;
    /// <summary>
    /// Richtung des Objekts
    /// </summary>
	private Vector3 moveDirection;
	
    /// <summary>
    /// Vor allen Start-Funktionen
    /// </summary>
	private void Awake()
    {
		y = transform.position.y;
		moveDirection = Vector3.zero;
        rbComponent = GetComponent(typeof(Rigidbody)) as Rigidbody;
        if (rbComponent == null)
            Debug.LogError("Das verfolgte Objekt hat keine RigidBody-Component!");
        Debug.Log("Sie finden Details über die Physik im Inspektor unter Info!");
    }

    /// <summary>
    /// Physik-Berechnungen führen wir in FixedUpdate aus,
    /// das in äquidistanten Zeitintervallen aufgerufen wird.
    /// </summary>
	private void FixedUpdate ()
    {
		KeyboardMovement();
	}
	
    /// <summary>
    /// Abfragen der beiden Achsen (als Default die Cursortasten)
    /// und Berechnung des Geschwindigkeitsvektors für die Bewegung.
    /// </summary>
	private void KeyboardMovement()
    {
		float dx = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
		moveDirection = new Vector3 (dx, y, dz);

		float moveDistance = MAX_MOVE_DISTANCE * Time.deltaTime;
		moveDirection = Vector3.ClampMagnitude(moveDirection, moveDistance);

		rbComponent.AddForce(moveDirection, ForceMode.VelocityChange);
	}
}
