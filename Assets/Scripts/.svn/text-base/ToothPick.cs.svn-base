using UnityEngine;
using System.Collections;

// the weapon of peep warriors
public class ToothPick : MonoBehaviour {
		
	protected Vector3 oldParentScale;
	protected Vector3 originalScale;
	protected Peep peep; // the peep this belongs to
	
	protected virtual void Start(){
		
		peep = transform.parent.GetComponent<Peep>();
		peep.PeepGrowListener += OnPeepGrow;
		
		oldParentScale = transform.parent.localScale;
	}

	// make sure our toothpick stays the same size
	protected virtual void OnPeepGrow(){
		transform.localScale = Helper.DivideVector3( Helper.MultiplyVector3(transform.localScale, oldParentScale), transform.parent.localScale);
		oldParentScale = transform.parent.localScale;
	}
	
	protected virtual void OnTriggerEnter( Collider other ){
			
		if( peep == null || Game.Instance.state == Game.State.SETUP ) return; // make sure we're attached to a peep
		
		if( other.CompareTag("peep") ){
			
			Peep otherPeep = other.GetComponent<Peep>();
			
			// when a pink peep stabs another pink peep, double the points
			if( peep.GetTeam() == Peep.Team.PINK && otherPeep.GetTeam() == Peep.Team.PINK ) otherPeep.points *= 2;
			
			if( otherPeep.GetTeam() == Peep.Team.PINK ) Spawner.Create( "PinkPuff", transform.position );
			else Spawner.Create( "BluePuff", transform.position );
			
			otherPeep.Puncture();
		}
	}
	
}
