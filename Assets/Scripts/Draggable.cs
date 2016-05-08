using UnityEngine;
using System.Collections;

// object which can be dragged
public class Draggable : MonoBehaviour {
	
	public enum State {WAITING, DRAGGING, ROTATING};
	protected State state = State.WAITING;
	
	public Collider rotator; // touch this to rotate instead of drag
	
	protected int fingerId = 0;
	protected float dragDist = 18.5f;
	
	// is this being dragged
	public virtual bool IsTouched(){
		return state != State.WAITING;
	}
	
	// Update is called once per frame
	void Update () {
		
		// see if we've started a drag
		if( !IsTouched() ){
			foreach( Touch touch in Input.touches ){
				if( touch.phase == TouchPhase.Began ){
					RaycastHit hitInfo;
					if( GetComponent<Collider>().Raycast( Camera.main.ScreenPointToRay(touch.position), out hitInfo, Mathf.Infinity)){
						fingerId = touch.fingerId;
						state = State.DRAGGING;
						break;
					} else if( rotator.Raycast( Camera.main.ScreenPointToRay(touch.position), out hitInfo, Mathf.Infinity)){
						fingerId = touch.fingerId;
						state = State.ROTATING;
					}
				}
			}
		}
		
		// update the drag, see if its stopped
		if( IsTouched() ){
			Touch touch;
			if( Helper.GetTouchById(fingerId, out touch)){
				if( touch.phase == TouchPhase.Moved ){
					if( state == State.DRAGGING ) Drag( touch.position );
					else if (state == State.ROTATING ) Rotate( touch.position );
				} else if( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled ){
					EndTouch();
				}
			} else {
				EndTouch();
			}		
		}
	}
	
	// move the object
	protected virtual void Drag( Vector2 screenPoint ){
		
		transform.position = Camera.main.ScreenToWorldPoint( new Vector3( screenPoint.x, screenPoint.y, dragDist));
	}
	
	protected virtual void Rotate( Vector2 screenPoint ){	
		Vector3 point = Camera.main.ScreenToWorldPoint( new Vector3( screenPoint.x, screenPoint.y, dragDist));
		point = new Vector3( point.x, transform.position.y, point.z);
		transform.LookAt( point );
	}
	
	protected virtual void EndTouch(){
		state = State.WAITING;
		fingerId = -1;
	}
}
