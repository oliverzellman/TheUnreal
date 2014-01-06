using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;

	private float skin = .005f;

	[HideInInspector]
	public bool grounded;

	Ray ray;
	RaycastHit hit;


	void Start(){
		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;
	}
	public void Move(Vector2 moveAmount){

	
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;
		grounded = false;
		for (int i=0; i<3; i++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 *i; //left, center and then rightmost ContactPoint OffMeshLink collider
			float y = p.y + c.y + s.y/2 * dir; //Bottom of collider 

			ray = new Ray(new Vector2(x,y), new Vector2(0,dir));
			Debug.DrawRay(ray.origin,ray.direction);

			if(Physics.Raycast(ray, out hit,Mathf.Abs(deltaY),collisionMask)){
				//Get Distance between player and ground
				float distance = Vector3.Distance(ray.origin, hit.point);

				// Stop player.s downwards movment after coming within skin width of a collider
				if(distance > skin) {
					deltaY = -distance + skin; 
				}
				else {
					deltaY= 0;
				}
				grounded = true;
				break;
			}
		}

		Vector2 finalTransofrm = new Vector2(deltaX, deltaY);

		transform.Translate (finalTransofrm);

	}
}
