using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	public GameObject opponent;
	public AnimationClip attack;
	// Use this for initialization

	public int damage;
	public double impactTime;
	public bool impacted;
	public float range;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (opponent);
		if (Input.GetKeyDown(KeyCode.Space) && InRange()) 
		{
			animation.CrossFade(attack.name);
			ClickToMove.attack = true;
			if(opponent !=null)
			{
			transform.LookAt(opponent.transform);
				//opponent.GetComponent<Mob>().getHit(damage);
			}
		}

		if (!animation.IsPlaying(attack.name)) 
		{
			ClickToMove.attack = false;
			impacted= false;
		}
		Impact ();
	}

	void Impact()
	{
		if (opponent != null && animation.IsPlaying(attack.name) && !impacted) 
		{
			if(animation[attack.name].time>animation[attack.name].length * impactTime)
			{
				opponent.GetComponent<Mob>().getHit(damage);
				impacted= true;
			}
		}
	}

	bool InRange()
	{
		float dist = Vector3.Distance (opponent.transform.position, transform.position);
		Debug.Log ("Distance to opponent "+dist);
		if (dist < range)
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}
}
