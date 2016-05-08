using UnityEngine;
using System.Collections;



// keeps peeps from getting too close
public class InvalidRing : MonoBehaviour {
	
	public int invalidPeeps = 0;
	public Peep peep;	
	
	protected virtual void Awake(){
		renderer.enabled = false;	
		iTween.RotateBy(gameObject,iTween.Hash("y",1,"time",10,"looptype",iTween.LoopType.loop,"easetype",iTween.EaseType.linear));
		peep = transform.parent.GetComponent<Peep>();
	}
	
	protected virtual void OnTriggerEnter( Collider other ){
						
		
		if( other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("invalid_ring") ){
			
			// ignore pink/pink invalid
			if( other.GetComponent<InvalidRing>().peep.GetTeam() == Peep.Team.PINK && peep.GetTeam() == Peep.Team.PINK ) return;
			
			invalidPeeps ++;
			renderer.enabled = true;
			
			Game.Instance.invalidRings ++;

		}
		
		
		
	}
	
	protected virtual void OnTriggerExit( Collider other ){
		
		if( other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("invalid_ring") ){
			
			// ignore pink/pink invalid
			if( other.GetComponent<InvalidRing>().peep.GetTeam() == Peep.Team.PINK && peep.GetTeam() == Peep.Team.PINK ) return;			
			
			invalidPeeps --;
			Game.Instance.invalidRings --;
			
			if( invalidPeeps == 0 ){
				renderer.enabled = false;			
			}
		}
	}
	
}
