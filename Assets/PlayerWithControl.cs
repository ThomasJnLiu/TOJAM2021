using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWithControl : MonoBehaviour
{

	public List<AudioClip> soundEffects;
	public float movementSpeed = 10f;
	public float jumpForce = 10f;
	public ParticleSystem dustPS;

	Rigidbody2D rb;

	float movement = 0f;

	public bool isGrounded = false;

	public Transform groundCheck;
	public float groundCheckRadius = 0.4f;
	public LayerMask groundMask;

	Animator anim;
	SquashAndStretch squash;

	// Use this for initialization
	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		squash = gameObject.GetComponent<SquashAndStretch>();
	}

	// Update is called once per frame
	void Update()
	{
		// Create sphere and check if it collides with the ground layer.
		/*isGrounded = (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask) != null);*/
		movement = Input.GetAxisRaw("Horizontal") * movementSpeed;
		if (movement != 0)
		{
			gameObject.GetComponent<Animator>().SetBool("moving", true);
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("moving", false);
		}
		FlipSprite(movement);
		/*		if (isGrounded)
				{
					gameObject.transform.SetParent(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask).gameObject.transform);
				} else
				{
					gameObject.transform.SetParent(null);

				}*/

		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded && rb.velocity.y <= 0f)
			{
				Jump();
			}
		}

		// Check if player falling, play falling animation
		if(rb.velocity.y < 0){
			anim.SetBool("falling", true);
		}
		if(isGrounded){
			anim.SetBool("falling", false);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "MovingPlatform" || collision.gameObject.tag == "FirstPlatform" && rb.velocity.y <= 0f)
		{
			isGrounded = true;
			gameObject.transform.SetParent(collision.gameObject.transform);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "MovingPlatform" || collision.gameObject.tag == "FirstPlatform")
		{
			isGrounded = false;
			gameObject.transform.SetParent(null);
		}
	}

	void Jump()
	{
		// Play Sound
		gameObject.GetComponent<AudioSource>().clip = soundEffects[Random.Range(0, 3)];
		/*		gameObject.GetComponent<AudioSource>().volume = Random.Range(0.05f, 0.1f);
				gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 0.8f);*/
		gameObject.GetComponent<AudioSource>().Play();


		// Visualize Jump
		createDust();
		squash.SetToSquash(.1f);
		anim.Play("Jump");


		// Apply Jump
		// Vector2 curVelocity = rb.velocity;
		// curVelocity.y = jumpForce;
		// rb.velocity = curVelocity;
		rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}

	public void createDust()
	{
		dustPS.Play();

	}

	private void FlipSprite(float direction){
		Debug.Log(direction);
		if(direction > 0){
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}else if(direction < 0){
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}

	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);// Change to always see transform.position
	}
}
