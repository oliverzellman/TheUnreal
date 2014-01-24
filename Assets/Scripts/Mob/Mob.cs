using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
	public float speed;
	public float reach;
	public float range;
	public Transform player;
	public CharacterController controller;
	GameObject playerObject;

	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;


	bool playerIsFound;
	// Use this for initialization

	//Health
	private int health;
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIsFound == false) 
		{
			player = GameObject.FindWithTag ("Player").transform;
			playerIsFound = true;
		}
		Debug.Log(animation[die.name].time.ToString());
		Debug.Log(animation[die.name].length.ToString());
	
		if(InRange())
		ChasePlayer ();
		else
			animation.CrossFade(idle.name);

		Debug.Log (health);
		if (health <= 0) 
		{
			animation.CrossFade (die.name);
			if(animation[die.name].time > 1.80)
			{
				Debug.Log ("Destroy object");
				Object.Destroy(gameObject);
			}

		}
	}

	bool InRange()
	{
		//Om player avståndet är större än reach och mindre än range.
		if (Vector3.Distance (transform.position, player.position) < range)
		{
			if(Vector3.Distance (transform.position, player.position) < reach)
				return false;
			else
			return true;		
		}	
		else 
		{
			
			return false;	
			
		}
	
	}

	void OnMouseOver(){
		player.GetComponent<Combat>().opponent = this.gameObject;
	}

	void ChasePlayer()
	{
		animation.CrossFade (run.name);
		playerObject = GameObject.FindGameObjectWithTag("Player");
		Quaternion lookAtPlayer = Quaternion.LookRotation (playerObject.transform.position - transform.position);
		lookAtPlayer.x = 0.0f;
		lookAtPlayer.z = 0.0f;
		transform.rotation = lookAtPlayer;

			//transform.LookAt (playerObject.transform.position);
		

		controller.SimpleMove (transform.forward * speed);

	}

	public void getHit(int damage)
	{
		health -= damage;
		if (health <= 0) 
		{


			//health = 0;

		}
	}
}
