using UnityEngine;
//using System.Collections;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und nicht ver�ndert.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Grenzen in x und z
    /// </summary>
	public const float MIN_X = -15.0F,
	                   MAX_X = 15.0F,
	                   MIN_Z = -10.0F,
	                   MAX_Z = 10.0F;
    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt
    /// und nicht mehr ver�ndert.
    /// </summary>
    private float m_y;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private float m_speed = 20.0f;
	
    /// <summary>
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht ver�ndert wird.
    /// Wir ben�tigen diesen Wert f�r die Translationsmatrix,
    /// mit der wir die Bewegung durchf�hren.
    /// </summary>
	private void Awake()
    {
		m_y= transform.position.y;
	}

    /// <summary>
    /// Bewegung in FixedUpdate, da wir Time.deltaTime verwenden.
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: �berpr�fen, ob wir im zul�ssigen Bereich sind.
    /// </summary>
	private void FixedUpdate ()
    {
		KeyboardMovement();
		CheckBounds();
	}
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical und Translation an Hand dieser Eingaben.
    /// </summary>
	private void KeyboardMovement(){

		float dx = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
		transform.Translate( new Vector3(dx, m_y, dz) );		
	}
	
    /// <summary>
    /// �berpr�fen, ob die Grenzen eingehalten werden.
    /// 
    /// Wir f�hren ein Clamp durch.
    /// </summary>
	private void CheckBounds(){
		float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, MIN_X, MAX_X);
		z = Mathf.Clamp(z, MIN_Z, MAX_Z);
		
		transform.position = new Vector3(x, m_y, z);
	}
}
