using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {
	
	protected virtual void Update(){
		foreach( Touch touch in Input.touches ){
			if( touch.phase == TouchPhase.Began ){
				RaycastHit hitInfo;
				if( collider.Raycast( Camera.main.ScreenPointToRay(touch.position), out hitInfo, Mathf.Infinity)){
					
					
					if( Application.loadedLevel < 12 ){
						Application.LoadLevel( "PeepJousting" + (Application.loadedLevel + 1) );
					}else{
						Application.LoadLevel( "PeepJousting0" );		
					}
					break;
				}
			}
		}
	}
}
