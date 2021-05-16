using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GoombaStomp : MonoBehaviour {

	public List<AudioClip> soundEffects;
	public float jumpForce = 10f;
	public ParticleSystem dustPS;

	Rigidbody2D rb;


	public bool isGrounded = false;

	public Transform groundCheck;
	public float groundCheckRadius = 0.4f;
	public LayerMask enemyMask;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		// Create sphere and check if it collides with the ground layer.
		Collider2D col = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, enemyMask);
		isGrounded = col != null;
		if (isGrounded && rb.velocity.y <= 0f)
		{
			col.gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().enabled = false;
			col.gameObject.transform.parent.GetComponentInChildren<CircleCollider2D>().enabled = false;
			if (col.gameObject.transform.parent.GetComponentInChildren<GreenEnemy>() != null)
            {
				col.gameObject.transform.parent.GetComponentInChildren<GreenEnemy>().enabled = false;
			}
			if (col.gameObject.transform.parent.GetComponentInChildren<RedEnemy>() != null)
			{
				col.gameObject.transform.parent.GetComponentInChildren<RedEnemy>().enabled = false;
			}
			col.gameObject.transform.parent.GetComponentInChildren<EdgeCollider2D>().enabled = false;
			col.gameObject.transform.parent.GetComponentInChildren<PlatformEffector2D>().enabled = false;
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
		rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
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
