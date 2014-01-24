using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	public AnimationClip run;

	public void AnimateRun(){
		animation.Play(run.name);

	}
}
