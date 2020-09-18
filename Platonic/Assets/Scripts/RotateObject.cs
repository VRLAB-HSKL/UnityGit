using UnityEngine;
using System.Collections;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der WASD-Tasten. 
/// </summary>
public class RotateObject : MonoBehaviour
{
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
    [Range(1.0f, 40.0f)]
    [Tooltip("Veränderung des Rotationswinkels")]
    public float speed = 10.0f;
	
	private void FixedUpdate ()
    {
		KeyboardRotation();
	}
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical (das sind zum Beispiel
    /// die Cursortasten in Unity) und Rotation an Hand dieser Eingaben.
    /// 
    /// Horizontal: Rotation um die z-Achse
    /// Vertikal: Rotation um die y-Achse
    /// 
    /// Je nachdem, ob Bedarf ist rotieren wir auch noch um die x-Achse
    /// </summary>
	private void KeyboardRotation()
    {
        float thetaX = 0.0f,
              thetaY = 0.0f,
              thetaZ = 0.0f;

        thetaY = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        thetaZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Rotate(new Vector3(thetaX, thetaY, thetaZ));
        		
	}
}
