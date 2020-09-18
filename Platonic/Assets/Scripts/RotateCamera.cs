using UnityEngine;

/// <summary>
/// Klasse für eine Examine-Metapher in der Landscape-Demo.
/// Drehungen um die x- und die y-Achse eines Objekts.
/// </summary>
[RequireComponent(typeof(Collider))]
public class RotateCamera : MonoBehaviour
{
    /// <summary>
    /// Welches Objekt wollen wir untersuchen?
    /// 
    /// Voraussetzung für diese Klasse ist, dass das beobachtete Objekt
    /// einen Collider besitzt. Wir verwenden den Mittelpunkt
    /// des Colliders als Drehpunkt.
    /// </summary>
    [Tooltip("Objekt, das beobachtet wird")]
    public GameObject examinedObject;

    /// <summary>
    /// Differenz des Rotationswinkels bei Tastendruck
    /// in Gradmaß.
    /// </summary>
    [Range(0.1f, 10.0f)]
    [Tooltip("Veränderung des Rotationswinkels")]
    public float delta = 1.0f;

    /// <summary>
    /// Um welchen Punkt in Weltkoordinaten rotiert die Kamera?
    /// </summary>
	private Vector3 m_rotationPoint;

    /// <summary>
    /// Wir fragen das Zentrums des Colliders ab und verwenden
    /// diesen Punkt als Zentrum der Rotationen.
    /// </summary>
    void Start ()
    {
        m_rotationPoint = examinedObject.GetComponent<Collider>().bounds.center;
	}
	
    /// <summary>
    /// Rotation der Kamera um das festgelegte Zentrum,
    /// um die y-Achse.
    /// <remarks>
    /// Mit "A" oder Cursor nach links drehen wir positiv,
    /// mit "D" oder Cursor nach rechts drehen wir negativ.
    /// </remarks>
    /// </summary>
	void Update ()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			gameObject.transform.RotateAround(m_rotationPoint, Vector3.up, delta);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			gameObject.transform.RotateAround(m_rotationPoint, Vector3.up, -delta);
		}
        if (Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            gameObject.transform.RotateAround(m_rotationPoint, Vector3.right, delta);
        }
        if (Input.GetKey(KeyCode.X) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            gameObject.transform.RotateAround(m_rotationPoint, Vector3.right, -delta);
        }
    }
}
