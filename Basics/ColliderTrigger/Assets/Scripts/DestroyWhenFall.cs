using UnityEngine;

/// <summary>
/// Löschen einer Instanz, falls die y-Koordinate zu klein ist.
/// </summary>
public class DestroyWhenFall : MonoBehaviour 
{
    /// <summary>
    /// y-Wert, ab dem das GameObjekt gelöscht werden soll
    /// </summary>
	private const float MIN_Y = -1;

    /// <summary>
    /// Wird einmal pro Frame aufgerufen.
    /// 
    /// Der Aufruf ist abhängig von der Frame-Rate.
    /// </summary>
	void Update () 
	{
		float y = transform.position.y;
		if( y < MIN_Y )
			Destroy( gameObject );	
	}

}
