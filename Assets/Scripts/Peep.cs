using UnityEngine;
using System.Collections;

public delegate void PeepGrowHandler();
public delegate void PeepPunctureHandler(Peep peep);

// a marshmellow peep warrior
public class Peep : MonoBehaviour {
	
	public enum State {SETUP,ATTACK,DEFEAT};
	protected State state;
	
	public enum Team {PINK, BLUE}
	public Team team;
	
	public InvalidRing invalidRing;
	
	// dimensions of a melted peep
	protected float meltedWidth = 5;
	protected float meltedHeight = 0.4f;
	
	protected float punctureHeight = 0.1f;
	protected float punctureTime = 0.25f;
	
	public int points; // how many points this peep is worth
	
	public event PeepGrowHandler PeepGrowListener;
	public event PeepPunctureHandler PeepPunctureListener;
	
	// Use this for initialization
	protected virtual void Start () {
		Game.Instance.RegisterPeep( this );
		Game.Instance.GameStateListener += OnGameStateChanged;
	}
	
	public Team GetTeam(){ return team; }
	
	protected virtual void OnGameStateChanged( Game.State state ){
		switch( state ){
			case Game.State.SETUP:
				Reset();
				break;
			case Game.State.ATTACK:
				SetState( State.ATTACK );
				break;
			case Game.State.OVER:
				iTween.StopByName(gameObject,"peep_attack");
				break;
		}
	}
	
	protected virtual void SetState( State state ){
		
		if( state == State.ATTACK ){
			StartAttack();
			rigidbody.isKinematic = false; // turn on real physics
			if( GetComponent<Draggable>() != null ) GetComponent<Draggable>().enabled = false;

		}
		
		this.state = state;	
	}
	
	
	
	protected virtual void StartAttack(){
		invalidRing.active = false;
		iTween.ScaleTo(gameObject,iTween.Hash("name","peep_attack","x", transform.localScale.x * meltedWidth,"y",transform.localScale.y * meltedHeight,"z", transform.localScale.z * meltedWidth, "time", Game.Instance.microwaveTime,"easetype",iTween.EaseType.linear,"onupdate","DuringAttack"));
	}	
	
	protected virtual void DuringAttack(){
		if( PeepGrowListener != null ) PeepGrowListener();
	}
	
	// resets this peep
	protected virtual void Reset(){
		
	}
	
	// defeat this peep
	public virtual void Puncture(){
		
		// don't do anything if we're not in attack mode
		if( state != State.ATTACK ) return;
		
		iTween.Stop(gameObject);
		
		Helper.SetLayerRecursively(gameObject, "DeadPeeps");
					
		if( PeepPunctureListener != null ) PeepPunctureListener(this);
		
		StartCoroutine(PunctureThread());
		
	}
	
	// for changing the itween we're using
	public virtual IEnumerator PunctureThread(){
		yield return null;
		iTween.ScaleTo(gameObject, iTween.Hash("y",punctureHeight,"time",punctureTime));
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		switch( state ){
			case State.SETUP:
				break;
			case State.ATTACK:
				break;
			case State.DEFEAT:
				break;
		}
	}
	

	
	
}
