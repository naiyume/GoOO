using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimplePlatformController : MonoBehaviour {


	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	private float moveForce = 365f;
	private float maxSpeed = 5f;
	//private float jumpForce = 1000f;
	private float jumpSpeed = 25f;
	public float jumpSpeedWall = 35f;
	private float verticalWallJump = 15f;
	//private float stoppingForce = 300f;
	private Transform groundCheck;
	private float dashForce = 3f;
	private int numDashFrames = 4;
	private float dashCooldown = 1f;

	//private bool grounded = false;
	//private Animator anim;
	private Rigidbody2D rb2d;
	//private bool dash = false;
	//private int dashable = 1;
	private int jumpCounter = 2;
	private bool wasGrounded = false;
	private bool dashing = false;
	private float dashCooldownTimer = 0f;

	private int wallJumpFrames = 9;
	private int wallLag = 0;

	private int dashTime = 0;
	private float dashDir = 0;

	private static float loadX = 0f;
	private static float loadY = 0f;

	//private static AudioSource audio = GetComponent ();

	bool isGrounded(){
		
		return Physics2D.Raycast (new Vector2(rb2d.position.x-0.5f, rb2d.position.y-0.5001f), -Vector2.up, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x, rb2d.position.y-0.5001f), -Vector2.up, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x+0.5f, rb2d.position.y-0.5001f), -Vector2.up, 0.05f).collider != null;

	}

	bool isWalledRight(){

		return Physics2D.Raycast (new Vector2(rb2d.position.x+0.5001f, rb2d.position.y-0.5f), Vector2.right, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x+0.5001f, rb2d.position.y), Vector2.right, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x+0.5001f, rb2d.position.y+0.5f), Vector2.right, 0.05f).collider != null;

	}

	bool isWalledLeft(){

		return Physics2D.Raycast (new Vector2(rb2d.position.x-0.5001f, rb2d.position.y-0.5f), Vector2.left, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x-0.5001f, rb2d.position.y), Vector2.left, 0.05f).collider != null || Physics2D.Raycast (new Vector2(rb2d.position.x-0.5001f, rb2d.position.y+0.5f), Vector2.left, 0.05f).collider != null;

	}

	bool isIcy(){
		
		if (isGrounded ()) {
			if(Physics2D.Raycast (new Vector2 (rb2d.position.x - 0.51f, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider != null){
				if(Physics2D.Raycast (new Vector2 (rb2d.position.x - 0.51f, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider.tag == "Icy"){
					return true;
				}
			}
			if(Physics2D.Raycast (new Vector2 (rb2d.position.x , rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider!= null){
				if(Physics2D.Raycast (new Vector2 (rb2d.position.x, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider.tag == "Icy"){
					return true;
				}
			}
			if(Physics2D.Raycast (new Vector2 (rb2d.position.x + 0.51f, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider != null){
				if(Physics2D.Raycast (new Vector2 (rb2d.position.x + 0.51f, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider.tag == "Icy"){
					return true;
				}
			}
			//return Physics2D.Raycast (new Vector2 (rb2d.position.x - 0.49f, rb2d.position.y - 0.5003f), -Vector2.up, 0.05f).collider.tag == "Icy" || Physics2D.Raycast (new Vector2 (rb2d.position.x, rb2d.position.y - 0.5001f), -Vector2.up, 0.05f).collider.tag == "Icy" || Physics2D.Raycast (new Vector2 (rb2d.position.x + 0.49f, rb2d.position.y - 0.5003f), -Vector2.up, 0.05f).collider.tag == "Icy";
		}
		return false;

	}

	// Use this for initialization
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.freezeRotation = true;
		rb2d.velocity = new Vector2 (0, 0);
		jump = false;
		gameObject.transform.position = new Vector3 (loadX, loadY, 0);

	}

	// Update is called once per frame
	void Update () 
	{
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			if (rb2d.velocity.x != 0f && dashCooldownTimer <= 0f) {
				
				//dash = true;
				dashTime = numDashFrames;
				if (Input.GetAxis ("Horizontal") == 0) {

					dashDir = 0;

				} else {

					dashDir = Mathf.Sign (Input.GetAxis ("Horizontal"));
				
				}
				dashing = true;
				//rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

			}
		}
		if (dashCooldownTimer >= 0f) {

			dashCooldownTimer -= Time.deltaTime;

		}
		if (isGrounded ()) {
			jumpCounter = 2;
			wasGrounded = true;
			wallLag = 0;
		} else if (wasGrounded) {

			wasGrounded = false;
			jumpCounter = 1;

		}


			


	}

	void FixedUpdate()
	{
		
		float h = Input.GetAxis("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(h));
		if (!dashing && wallLag == 0) {
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			if (!Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow) && !isIcy () && isGrounded ()) {
			
				rb2d.velocity = new Vector2 (0f, rb2d.velocity.y);

			} /* else if (isIcy()){

				Debug.Log ("Slippery");

			}
			*/

			/*if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();*/

			if (jump) {

				if (isWalledLeft ()) {

					rb2d.velocity = new Vector2 (jumpSpeedWall, verticalWallJump);
					wallLag = wallJumpFrames;

				} else if (isWalledRight ()) {

					rb2d.velocity = new Vector2 (-jumpSpeedWall, verticalWallJump);
					wallLag = wallJumpFrames;
				
				} else if (jumpCounter > 0) {
					//anim.SetTrigger("Vertical");
					//rb2d.AddForce(new Vector2(0f, jumpForce));
					//Debug.Log(Physics2D.Raycast (new Vector2(rb2d.position.x, rb2d.position.y-0.52f), -Vector2.up, 0.1f).transform.position);
					rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
					jumpCounter -= 1;
					jump = false;
				}

				jump = false;

			} else {

				jump = false;

			}
		} else {

			if (dashTime > 0) {
				//Debug.Log (Vector2.right * dashDir * moveForce * dashForce);
				rb2d.AddForce (new Vector2(dashDir * moveForce * dashForce, 50f));
				dashTime -= 1;
				//dashable = 0;
				//dash = false;
				if (dashTime == 0) {

					dashing = false;
					dashCooldownTimer = dashCooldown;

				}

			}

		}

		if (wallLag > 0) {

			wallLag = wallLag - 1;

		}

	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Hazard") {
			
			//Destroy (rb2d.gameObject);
			//rb2d.transform.position = new Vector2(0f,0f);
			//rb2d.velocity = new Vector2 (0f, 0f);
			SceneManager.LoadScene (col.gameObject.scene.buildIndex);

		}

	}


	void OnTriggerEnter2D(Collider2D col){

		Debug.Log ("Collided");	
		if (col.gameObject.tag == "Door") {

			Destroy (col.gameObject);
			SceneManager.LoadScene (col.gameObject.scene.buildIndex + 1);
			//Application.LoadLevel  ("Testing2");
		} else if (col.gameObject.tag == "Hazard") {

			//Destroy (rb2d.gameObject);
			//rb2d.transform.position = new Vector2(0f,0f);
			//rb2d.velocity = new Vector2 (0f, 0f);
			SceneManager.LoadScene (col.gameObject.scene.buildIndex);
			gameObject.transform.position = new Vector3 (loadX, loadY, 0);

		} else if (col.gameObject.tag == "Checkpoint") {


			loadX = col.gameObject.transform.position.x;
			loadY = col.gameObject.transform.position.y;
			Destroy (col.gameObject);
		}
	}

	/*
	void OnCollisionEnter2D(Collision2D col){
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Door") {
			
			Destroy (col.gameObject);

		}


	}
	*/
	/*
	void OnCollisionExit2D(Collision2D col){
		Debug.Log (1.0f / Time.deltaTime);
		if (col.gameObject.transform.position.y + (col.gameObject.transform.localScale.y / 2) <= rb2d.transform.position.y - (rb2d.transform.localScale.y / 2 + rb2d.velocity.x)) {
			Debug.Log ("Floor" + (col.gameObject.transform.position.y + (col.gameObject.transform.localScale.y / 2)));
			Debug.Log ("square: " + (rb2d.transform.position.y- (rb2d.transform.localScale.y / 2) - (rb2d.velocity.y -  * Time.deltaTime));
			grounded = false;
			dashable = 1;

		}


		//grounded = false;
		//dashable = 1;
		//Debug.Log ("Floor" + (col.gameObject.transform.position.y + (col.gameObject.transform.localScale.y / 2)));
		//Debug.Log ("square: " + (rb2d.transform.position.y));

	}
*/
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}