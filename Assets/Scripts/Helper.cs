using UnityEngine;
using System.Collections;

// A static collection of behaviors
// that Unity should provide for you
public static class Helper{
	
	// returns a touch by its fingerId
	// returns false if the touch does not exist
	public static bool GetTouchById( int id, out Touch touch ){
		foreach( Touch t in Input.touches ){
			if( t.fingerId == id ){
				touch = t;
				return true;
			}
		}
		touch = new Touch();
		return false;
	}

	public static Vector3 DivideVector3( Vector3 a, Vector3 b ){
		return new Vector3(a.x / b.x, a.y / b.y, a.z/ b.z);		
	}
	
	public static Vector3 MultiplyVector3( Vector3 a, Vector3 b ){
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);		
	}
	
	public static void SetLayerRecursively( GameObject obj, int layer ){
		obj.layer = layer;
		foreach( Transform t in obj.transform ) SetLayerRecursively( t.gameObject, layer );
	}
	
	public static void SetLayerRecursively( GameObject obj, string layer ){
		Helper.SetLayerRecursively( obj, LayerMask.NameToLayer( layer ) );	
	}
}
