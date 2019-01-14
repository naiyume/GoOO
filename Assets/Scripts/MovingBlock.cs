using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

	public float moveSpeed = .01f;
	public int range = 5;
	private float startPosx = 0f;
	private float startPosy = 0f;
	//private Rigidbody2D rb2d;
	private bool stopped = false;
	private bool up = false;


	// Use this for initialization
	void Start () {
		
		//rb2d = GetComponent<Rigidbody2D>();
		startPosx = gameObject.transform.position.x;
		startPosy = gameObject.transform.position.y;
		Debug.Log (startPosy);
		//rb2d.velocity = new Vector2 (0f, moveSpeed);

	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.transform.position.y >= startPosy + range || gameObject.transform.position.y <= startPosy - range) {
			
			//transform.position = new Vector3 (0, transform.position.y, 0);
			stopped = true;
			up = !up;

		}
		if (stopped) {
			
			stopped = false;

		}
		if (up) {

			gameObject.transform.position = new Vector3 (startPosx, transform.position.y + moveSpeed, 0);
				//rb2d.velocity = new Vector2 (0f, moveSpeed);

		} else {

			gameObject.transform.position = new Vector3 (startPosx, transform.position.y - moveSpeed, 0);
				//rb2d.velocity = new Vector2 (0f, -1 * moveSpeed);

		}





	}

}
