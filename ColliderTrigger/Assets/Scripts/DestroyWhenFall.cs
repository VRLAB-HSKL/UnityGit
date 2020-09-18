using UnityEngine;

/// <summary>
/// L�schen einer Instanz, falls die y-Koordinate zu klein ist.
/// </summary>
public class DestroyWhenFall : MonoBehaviour 
{
    /// <summary>
    /// y-Wert, ab dem das GameObjekt gel�scht werden soll
    /// </summary>
	private const float MIN_Y = -1;

    /// <summary>
    /// Wird einmal pro Frame aufgerufen.
    /// 
    /// Der Aufruf ist abh�ngig von der Frame-Rate.
    /// </summary>
	void Update () 
	{
		float y = transform.position.y;
		if( y < MIN_Y )
			Destroy( gameObject );	
	}

}
