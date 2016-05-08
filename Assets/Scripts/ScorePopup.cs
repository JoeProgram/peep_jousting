using UnityEngine;
using System.Collections;

// score particle-like effect
public class ScorePopup : MonoBehaviour {
	
	protected float popupTime = 2;
	protected float popupY = 1;
	
	// alternate constructor
	public static ScorePopup Create( Vector3 position, string text ){
		ScorePopup popup = (Instantiate(Resources.Load("ScorePopup")) as GameObject).GetComponent<ScorePopup>();
		popup.GetComponent<TextMesh>().text = text;
		popup.transform.position = position;
		return popup;
	}
	
	// Use this for initialization
	void Start () {
		iTween.MoveAdd( gameObject, iTween.Hash("y",popupY,"time",popupTime,"oncomplete","OnComplete"));
	}
	
	protected virtual void OnComplete(){
		Destroy( gameObject );	
	}
}
