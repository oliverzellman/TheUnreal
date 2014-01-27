using UnityEngine;
using System.Collections;

public class GlobalAnimationScript : MonoBehaviour {

	public string[] animationList = {"idle","run","walk","attack","die"};
	// Use this for initialization
	void Start () {

	}


	public void SetAnimation(string animation)
	{
		for(int i=0; i<animationList.Length; i++)
		{
			if(animation == animationList[i])
			{
				networkView.RPC ("SendAnimationState",RPCMode.Others, i);
			}
		}
	}

	[RPC]
	public void SendAnimationState(int i)
	{
		animation.CrossFade (animationList [i].ToString());
	}
}
