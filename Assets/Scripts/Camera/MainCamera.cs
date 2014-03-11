using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	
		
	public Transform target;
	public Vector3 distance;
	public float height = 4.5f;
	public float damping = 0.0f;
	public bool followBehind = true;
	private bool playerIsFound;

	void Start() {

		target = null;	
	}


	void Update () 
	{
		if (playerIsFound == false) {
						target = GameObject.FindWithTag ("Player").transform;
			playerIsFound = true;
				}
		//Make sure target is set.
					if (!target)
					return;

					float wantedHeight = target.position.y * height;
					float currentHeight = transform.position.y;

						currentHeight = height;
					transform.position = target.position + distance;
					//transform.position -= Vector3.forward ;
					Vector3 positionY = transform.position;
					positionY.y = currentHeight;
					transform.position = positionY;
					transform.LookAt (target);
						

	}
	

}
