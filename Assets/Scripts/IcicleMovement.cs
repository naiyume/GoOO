using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcicleMovement : MonoBehaviour {

	public float range = 2f;
	public float downCheck = 10f;
	public int gravityScalar = 150;

	private bool falling = false;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake () {

		falling = false;
		rb2d = GetComponent<Rigidbody2D>();

	}

	bool checkForHuman(){

		if(Physics2D.Raycast (new Vector2 (rb2d.position.x - range, rb2d.position.y - 0.5001f), -Vector2.up, downCheck).collider != null){
			if(Physics2D.Raycast (new Vector2 (rb2d.position.x - range, rb2d.position.y - 0.5001f), -Vector2.up, downCheck).collider.tag == "Player"){
				//Debug.Log ("Found!");
				return true;
			}
		}
		if(Physics2D.Raycast (new Vector2 (rb2d.position.x + range, rb2d.position.y - 0.5001f), -Vector2.up, downCheck).collider != null){
			if(Physics2D.Raycast (new Vector2 (rb2d.position.x + range, rb2d.position.y - 0.5001f), -Vector2.up, downCheck).collider.tag == "Player"){
				//Debug.Log ("Found!");
				return true;

			}
		}
		return false;
	}



	// Update is called once per frame
	void Update () {
		
		if (checkForHuman ()) {

			//Debug.Log ("Falling is true");
			falling = true;

		}
		if (falling) {

			//Debug.Log (rb2d.velocity);
			//rb2d.velocity = new Vector2 (0f, 0f);
			//rb2d.velocity = new Vector2(0f, 0f);
			rb2d.AddForce (Vector2.down * 150);
			//Debug.Log (rb2d.velocity);
		} else {

			rb2d.velocity = new Vector2(0f, 0f);

		}



	}

	void OnTriggerEnter2D(Collider2D col){

		Debug.Log ("Collided");	
		Destroy (rb2d.gameObject);
		if (col.gameObject.tag == "Player") {

			//col.gameObject.transform.position = new Vector2 (0f, 0f);

			SceneManager.LoadScene (col.gameObject.scene.name);
		}


	}


}
