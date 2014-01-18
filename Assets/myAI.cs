using UnityEngine;
using System.Collections;
using Pathfinding;
public class myAI : MonoBehaviour {
	public Vector3 targetPosition;

	private Seeker seeker;
	private CharacterController controller;

	public Path path;

	public float speed = 10;

	public float nextWaypointDistance = 1;

	private int currentWaypoint = 0;

	// Use this for initialization
	public void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

	
	// Update is called once per frame
	public void Update () {
	
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

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

}
