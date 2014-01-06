using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	private Transform target;
	private float trackSpeed = 10;

	public void SetTarget(Transform t) {
		target = t;
	}

	void LateUpdate(){
		if (target) {
			//Target camera pos. 
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y +10, trackSpeed);
			transform.position = new Vector3(x,y,transform.position.z);
		}
	}

	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;
		} else {
			float dir = Mathf.Sign (target - n); // Must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir; 
			return(dir == Mathf.Sign (target - n)) ? n : target; //If n has now passed target then return target otherwise return n
		}
	}
}
