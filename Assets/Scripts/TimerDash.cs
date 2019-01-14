using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDash : MonoBehaviour {

	private float start_pos_x;
	private float start_pos_y;
	private float start_scale_x;
	private float start_scale_y;
	public float increment = 50f;
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		start_pos_x = rb2d.transform.position.x;
		start_pos_y = rb2d.transform.position.y;
		start_scale_x = rb2d.transform.localScale.x;
		start_scale_y = rb2d.transform.localScale.y;


	} 
	
	// Update is called once per frame
	void Update () {
		//rb2d.position = new Vector2(rb2d.position.x + increment, rb2d.position.y);
		//rb2d.transform.localScale = new Vector2(rb2d.transform.localScale.x + increment/2, rb2d.trabsform./localScale.y);
	} 
}
