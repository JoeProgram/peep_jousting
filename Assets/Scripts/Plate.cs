using UnityEngine;
using System.Collections;

// the plate the peeps battle upon
public class Plate : MonoBehaviour {
	
	protected float rotateSpeed = 30 ; // degrees per second
	
	protected virtual void Start(){
		Game.Instance.GameStateListener += OnGameStateChanged;	
	}
	
	protected virtual void OnGameStateChanged( Game.State state ){
		switch( state ){
			case Game.State.SETUP:
				Reset();
				break;
			case Game.State.ATTACK:
				StartTurning();
				break;
			case Game.State.OVER:
				iTween.Stop(gameObject);
				break;
		}
	}
	
	protected virtual void StartTurning(){
		iTween.RotateAdd( gameObject, iTween.Hash("y",Game.Instance.microwaveTime * rotateSpeed, "time",Game.Instance.microwaveTime,"easetype",iTween.EaseType.linear));	
	}
	
	protected virtual void Reset(){
		iTween.Stop( gameObject );	
	}
	
}
