using UnityEngine;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und nicht verändert.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Grenzen in x und z
    /// </summary>
	public const float MIN_X = -15,
	                   MAX_X = 15,
	                   MIN_Z = -10,
	                   MAX_Z = 10;
    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float y;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private float speed = 20.0F;

    /// Initialisierung
    /// 
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht verändert wird.
    /// Wir benötigen diesen Wert für die Translationsmatrix,
    /// mit der wir die Bewegung durchführen.
    private void Awake()
    {
		y = transform.position.y;
	}

    /// <summary>
    /// Bewegung und Verfolgung durchführen
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
		KeyboardMovement();
		CheckBounds();
	}
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical (das sind zum Beispiel
    /// die Cursortasten in Unity) und Translation an Hand dieser Eingaben.
    /// </summary>
	private void KeyboardMovement(){
		float dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		transform.Translate( new Vector3(dx,y,dz) );		
	}
	
    /// <summary>
    /// Überprüfen, ob die Grenzen eingehalten werden.
    /// </summary>
	private void CheckBounds(){
		float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, MIN_X, MAX_X);
		z = Mathf.Clamp(z, MIN_Z, MAX_Z);
		
		transform.position = new Vector3(x, y, z);
	}
}
