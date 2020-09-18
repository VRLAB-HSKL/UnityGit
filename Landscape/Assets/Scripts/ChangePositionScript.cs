/*! 
 * \brief This script changes the position of an object
 * \ingroup Landscape
 * \author Sascha Hayton
 * \date 2014 2015
 */ 
using UnityEngine;

/// <summary>
/// Verschieben des Geländemodells in Richtung der y-Achse.
/// 
/// Das Geländemodell wird mit Hilfe eines Shaders dargestellt,
/// der abhängig von der Höhe (y-Werte) eines Punkts eine Farbe zuweist.
/// 
/// Um diesen Shader zu sehen kann mit dieser Klasse das Gelände
/// nach oben bzw. nach unten bewegt werden.
/// </summary>
/// <remarks> 
/// Autor: Sascha Hayton, Wintersemester 2014/15
/// Projekt Studiengang Informatik
/// 
/// Veränderungen: Manfred Brill, Sommersemester 2020.
/// </remarks> 
public class ChangePositionScript : MonoBehaviour
{
    /// <summary>
    /// Veränderung der Position
    /// </summary>
    [Header("Anheben und Absenken")]
    [Tooltip("Veränderung der y-Koordinate")]
    [Range(0.1f, 10.0f)]
	public float stepSize = 3.0f;

    /// <summary>
    /// Mit F und G können wir die Landschaft anheben bzw. absenken.
    /// </summary>
	void Update ()
    {
		if(Input.GetKey(KeyCode.F) )
		{
			gameObject.transform.Translate( new Vector3(0.0f, stepSize, 0.0f));
		}
		if(Input.GetKey(KeyCode.G))
		{
			gameObject.transform.Translate( new Vector3(0.0f, -stepSize, 0.0f));
		}
	}
}
