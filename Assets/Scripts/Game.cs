using UnityEngine;
using System.Collections;

// define a delegate for listeners to use
public delegate void GameStateHandler( Game.State state );

// handles the logic of the game as a whole
public class Game : MonoBehaviour {
	
	public GUIStyle styleFightButton;
	
	public enum State { SETUP, ATTACK, OVER };
	public State state;
	public event GameStateHandler GameStateListener;
	
	public int invalidRings = 0;
	
	public float microwaveTime = 10;
	protected float scoreMultiplier;
	protected float minScoreMultiplier = 1;
	protected float maxScoreMultiplier = 10;
	
	public enum GameResult { NONE, WON, LOST };
	protected GameResult gameResult = GameResult.NONE;
		
	protected int pinkPeeps;
	protected int bluePeeps;
	
	public TextMesh scoreText;
	protected int score;
	
	public TextMesh resultText;
	public GameObject nextLevelButton;
	
	public TextMesh levelText;
	
	public ShotCamera shotCam; // reference to the custom cam script
	
	// static referincing for easy access
	public static Game Instance = null;
	protected virtual void Awake(){
		Instance = this;
		
		state = State.SETUP;	
		score = 0;
		
		// don't allow dead peeps to affect live peeps
		Physics.IgnoreLayerCollision( LayerMask.NameToLayer("LivePeeps"), LayerMask.NameToLayer("DeadPeeps"));
		Physics.IgnoreLayerCollision( LayerMask.NameToLayer("DeadPeeps"), LayerMask.NameToLayer("DeadPeeps"));
		
		levelText.text = "Level " + (Application.loadedLevel + 1);

	}		
	
	public virtual void RegisterPeep( Peep peep ){
		
		peep.PeepPunctureListener += OnPeepPunctured;
		
		if( peep.GetTeam() == Peep.Team.BLUE ) bluePeeps++;
		else pinkPeeps++;
	}

	
	// set the state and do state transitions
	protected virtual void SetState( State state ){
		
		switch( state ){
			case State.SETUP:	
				shotCam.SetShot( (int)State.SETUP );
				break;
			case State.ATTACK:
				iTween.ValueTo(gameObject,iTween.Hash("from",minScoreMultiplier,"to",maxScoreMultiplier,"easetype",iTween.EaseType.linear,"time",microwaveTime,"onupdate","UpdateScoreMultiplier"));
				shotCam.SetShot( (int)State.ATTACK );
				break;
		}
		
		// send the event to anyone listening
		if( GameStateListener != null ) GameStateListener( state );
			
		this.state = state;
		
	}
	
	protected virtual void UpdateScoreMultiplier( float val ){
		scoreMultiplier = val;
	}
	
	public int GetScoreMultiplier(){
		return (int)scoreMultiplier;	
	} 
	
	protected virtual void OnGUI(){
		switch( state ){
			case State.SETUP:
			
				// button to start the fight
				GUI.enabled = invalidRings == 0;	
				if( GUI.Button( new Rect( 345,240,130,70 ), "", styleFightButton )){
					SetState( State.ATTACK );
				}
				GUI.enabled = true;
			
			
				break;
		}
	}
	
	public virtual void OnPeepPunctured(Peep peep){
		ChangeScore(peep.points * GetScoreMultiplier());
		
		
		if( peep.points > 0 ) ScorePopup.Create( peep.transform.position, "+" + peep.points * GetScoreMultiplier());
		
		if( peep.GetTeam() == Peep.Team.PINK ) pinkPeeps --;
		else bluePeeps --;
		
		if( pinkPeeps == 0 ){
			Win();
		} else if( bluePeeps == 0 ){
			Lose();
		}

	}
	
	protected virtual void Win(){
		gameResult = GameResult.WON;
		resultText.GetComponent<Renderer>().enabled = true;
		SetState( State.OVER );
		nextLevelButton.SetActiveRecursively( true );
	}
	
	protected virtual void Lose(){
		gameResult = GameResult.LOST;
		resultText.text = "You've lost";	
		resultText.GetComponent<Renderer>().enabled = true;
		SetState( State.OVER );	
	}
	
	public virtual void ChangeScore( int delta ){
		score += delta;
		scoreText.text = score.ToString();
	}

	
	
	
}
