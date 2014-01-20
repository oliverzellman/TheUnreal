using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		Debug.Log ("Hello im triggered");
		renderer.material.color = new Color (1, 0, 0, 1);
		}

	void Update () {
	
	}
}
