using UnityEngine;
using System.Collections;
public class clickedMove : MonoBehaviour {
	
	private GameObject player;
	private Animator anim;
	private Vector3 playerPos;
	// Use this for initialization

	void Start () {
		player = GameObject.Find ("TaichiB");
		playerPos = player.transform.position;
		anim = player.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		Plane targetPlane = new Plane(transform.up, transform.position);
		foreach (Touch touch in Input.touches) {
			anim.SetBool("isWalking",true);
			//Gets the ray at position where the screen is touched
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			//Gets the position of ray along plane
			float dist = 0.0f;
			//Intersects ray with the plane. Sets dist to distance along the ray where intersects
			targetPlane.Raycast(ray, out dist);
			//Returns point dist along the ray.
			Vector3 planePoint = ray.GetPoint(dist);
			//Debug.Log("Point=" + planePoint);
			//True if finger touch began. If ray intersects collider, set pickedObject to transform 
			//of collider object
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary) {
				//anim.SetBool("isWalking",true);
				player.transform.LookAt(planePoint);
				player.transform.localPosition = Vector3.MoveTowards(playerPos, planePoint, 0.5F * Time.deltaTime);
				//playerPos = player.transform.position;
				playerPos = player.transform.localPosition;
			} else if (touch.phase == TouchPhase.Ended){
				anim.SetBool("isWalking",false);
			}
		}
	}
}