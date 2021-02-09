using UnityEngine;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. 
/// 
/// Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und nicht verändert.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Grenzen in x und z für den Bewegungsraum
    /// </summary>
	public const float MIN_X = -15,
	                   MAX_X = 15,
	                   MIN_Z = -10,
	                   MAX_Z = 10;
    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt
    /// und nicht mehr verändert.
    /// </summary>
    private float m_y;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private float m_speed = 20.0f;
	
    /// <summary>
    /// Abfragen der y-Koordinate, damit wir die einfrieren können.
    /// </summary>
	private void Awake()
    {
		m_y = transform.position.y;
	}

    /// <summary>
    /// Wir führen die Bewegung in FixedUpdate aus, da wir
    /// Time.deltaTime abfragen.
    /// </summary>
	private void FixedUpdate ()
    {
		KeyboardMovement();
		CheckBounds();
	}
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical und Translation an Hand dieser Eingaben.
    /// </summary>
	private void KeyboardMovement()
    {
		float dx = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
		transform.Translate( new Vector3(dx, m_y, dz) );		
	}
	
    /// <summary>
    /// Überprüfen, ob die Grenzen eingehalten werden.
    /// </summary>
	private void CheckBounds(){
		float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, MIN_X, MAX_X);
		z = Mathf.Clamp(z, MIN_Z, MAX_Z);
		
		transform.position = new Vector3(x, m_y, z);
	}
}
