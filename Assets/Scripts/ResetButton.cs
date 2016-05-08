using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	
	protected virtual void Update(){
		foreach( Touch touch in Input.touches ){
			if( touch.phase == TouchPhase.Began ){
				RaycastHit hitInfo;
				if( collider.Raycast( Camera.main.ScreenPointToRay(touch.position), out hitInfo, Mathf.Infinity)){
					Application.LoadLevel( Application.loadedLevel );
					break;
				}
			}
		}
	}
}
