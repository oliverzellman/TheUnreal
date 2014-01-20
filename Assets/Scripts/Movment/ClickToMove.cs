using UnityEngine;
using System.Collections;
using Pathfinding;
public class ClickToMove : MonoBehaviour {
	private Vector3 targetPosition;
	
	private Seeker seeker;
	private CharacterController controller;
	
	private Path path;
	
	public float speed = 100;
	
	public float nextWaypointDistance = 1;
	
	private int currentWaypoint = 0;
	
	// Use this for initialization
	public void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

	}


	
	
	void LocatePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 10000)) {
			targetPosition = new Vector3(hit.point.x, hit.point.y,hit.point.z);
			Debug.Log (targetPosition);
			
		}
	}


	// Update is called once per frame
	public void Update () {
		if (networkView.isMine) {
						if (Input.GetMouseButtonDown (0)) {
								LocatePosition ();
								path = null;
								seeker.StartPath (transform.position, targetPosition, OnPathComplete);
						}
				} else {
			enabled = false;
		}
	}
	
	public void OnPathComplete(Path p){
		Debug.Log ("Eyy, back on track. Error? " + p.error);
		if (path == null) {
			path = p;
			currentWaypoint = 0;
		}
	}
	
	public void FixedUpdate(){
		if (path == null) {
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log("End of Path Reached");
			return;
		}
		
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.SimpleMove (dir);
		Quaternion direction = Quaternion.LookRotation (dir);
		direction.x = 0.0f;
		direction.z = 0.0f;
		transform.rotation = direction;
		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}





