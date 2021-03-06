﻿using UnityEngine;
using System.Collections;
using Pathfinding;
public class ClickToMove : MonoBehaviour {
	private Vector3 targetPosition;
	
	private Seeker seeker;
	private CharacterController controller;
	
	private Path path;
	
	public float speed = 100;
	
	public float nextWaypointDistance = 0;
	
	private int currentWaypoint = 0;


	//Animation
	public string[] animationList = {"idle","run","walk","attack","die"};
	public AnimationClip run;
	public AnimationClip idle;

	public static bool attack;


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

		
			
		}
	}


	// Update is called once per frame
	public void Update () {
		if (networkView.isMine) {
						if (Input.GetMouseButtonDown (0)) {
					LocatePosition ();
					
								
								//seeker.StartPath (transform.position, targetPosition, OnPathComplete);
				networkView.RPC ("NewPath",RPCMode.All, targetPosition);

						}
				} 
	}
	
	public void OnPathComplete(Path p){

		if (path == null) {
			path = p;
			currentWaypoint = 0;
			Debug.Log("PATH! COMPLEATOO");
		}
	}
	
	public void FixedUpdate(){
		if (path == null) {
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {

			if(!attack)
			SetAnimation("idle");
			return;
		}

		//Animate Run
		if (!attack)
		SetAnimation (run.name);

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


	//Network Methods

	public void SetAnimation(string animation)
	{
		for(int i=0; i<animationList.Length; i++)
		{
			if(animation == animationList[i])
			{
				networkView.RPC ("SendAnimationState",RPCMode.All, i);
			}
		}
	}





	//RPC:S
	[RPC]
	void NewPath(Vector3 targetPos)
	{
		path = null;
		seeker.StartPath (transform.position,targetPos,OnPathComplete);
		Debug.Log ("I am running");
	

	}


	[RPC]
	void SendAnimationState(int i)
	{

		Debug.Log ("I am animating");

		animation.CrossFade (animationList [i].ToString());
	}
}





