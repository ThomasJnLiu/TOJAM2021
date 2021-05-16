using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		// Create sphere and check if it collides with the ground layer.
		isGrounded = (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask) != null);
		movement = Input.GetAxisRaw("Horizontal") * movementSpeed;
		if (movement != 0)
        {
			gameObject.GetComponent<Animator>().SetBool("moving", true);
        } else
        {
			gameObject.GetComponent<Animator>().SetBool("moving", false);
		}

		if (isGrounded && rb.velocity.y <= 0f)
		{
			Jump();
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
		Animator anim = gameObject.GetComponent<Animator>();
		SquashAndStretch squash = gameObject.GetComponent<SquashAndStretch>();
		squash.SetToSquash(.1f);
		anim.Play("Jump");


		// Apply Jump
		Vector2 curVelocity = rb.velocity;
		curVelocity.y = jumpForce;
		rb.velocity = curVelocity;
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);// Change to always see transform.position
	}
}
