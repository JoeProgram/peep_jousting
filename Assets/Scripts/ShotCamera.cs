using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a camera that moves to different shots
public class ShotCamera : MonoBehaviour {
	
	public List<Vector3> positions;
	public List<Vector3> angles;
	
	public float transitionTime = 2f;
	
	public virtual void SetShot( int index ){
		
		// make sure we're moving to a valid position
		if( index < 0 || index > positions.Count ) return;
		
		iTween.MoveTo( gameObject, positions[index], transitionTime );
		iTween.RotateTo( gameObject, angles[index], transitionTime );
								
	}
	
}
