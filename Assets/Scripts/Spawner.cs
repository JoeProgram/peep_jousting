using UnityEngine;
using System.Collections;

// creates resource prefabs a little quicker
public class Spawner : MonoBehaviour {

	public static GameObject Create(string prefab, Vector3 position){
		GameObject obj = Instantiate( Resources.Load(prefab) ) as GameObject;
		obj.transform.position = position;
		return obj;
	}
	
}
