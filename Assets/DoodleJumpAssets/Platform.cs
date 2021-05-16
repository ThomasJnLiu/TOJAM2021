using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IEasyListener
{
	// We will move the platform using callbacks from the FMOD event (myAudioEvent)



	[SerializeField]
	float timeToReachTarget;

	float t = 0;

	public float jumpForce = 10f;

	public Transform leftTranslate, rightTranslate;
	private Transform startTranslate, moveTranslate;
	public float speed = 5f;
	public bool isMoving;
	private bool _movingLeft;
	public bool isGrounded = false;

	GameObject _player;
	public Rigidbody2D rb;

	public void OnBeat(EasyEvent audioEvent)
	{
		// Resets the cube animation on beats 1 and 3
		/*        if (audioEvent.CurrentBeat == 1 || audioEvent.CurrentBeat == 3)
				{
					ToggleCubeColor();
					StartCoroutine(AnimateCube());

				}*/

		if (audioEvent.CurrentBeat == 1 )
		{
			StartCoroutine(MovePlatform(audioEvent));
		}
	}



	private void Start()
	{
		_player = GameObject.FindObjectOfType<Player>().gameObject;
		rb = _player.GetComponent<Rigidbody2D>();
		if (isMoving)
		{
			_movingLeft = (Random.value > 0.5f);
			if (_movingLeft)
			{
				gameObject.transform.position = rightTranslate.position;
				startTranslate = rightTranslate;
				moveTranslate = leftTranslate;
			}
			else
			{
				gameObject.transform.position = leftTranslate.position;
				startTranslate = leftTranslate;
				moveTranslate = rightTranslate;
			}
		}


	}

	private IEnumerator MovePlatform(EasyEvent audioEvent)
	{
		var journeyTime = audioEvent.BeatLength() * 2;

		float startTime = Time.time;

		float timeSinceStarted = Time.time - startTime;
		float percentageComplete = timeSinceStarted / journeyTime;




		while (percentageComplete <= 1)
		{
			timeSinceStarted = Time.time - startTime;
			percentageComplete = timeSinceStarted / journeyTime;

			var newPos = Vector2.LerpUnclamped(startTranslate.position, moveTranslate.position, percentageComplete);

			transform.position = newPos;
			yield return null;
		}

		startTime = Time.time;
		percentageComplete = 0;

		while (percentageComplete <= 1)
		{
			timeSinceStarted = Time.time - startTime;
			percentageComplete = timeSinceStarted / journeyTime;

			var newPos = Vector2.LerpUnclamped(moveTranslate.position, startTranslate.position, percentageComplete);

			transform.position = newPos;
			yield return null;
		}
	}
}

 /*   private void FixedUpdate()
    {
		if (isMoving && _musicTiming.firstBeatHasDropped)
        {
			t += Time.deltaTime / timeToReachTarget;
			
			if (_movingLeft)
			{
				transform.position = Vector2.Lerp(_startPosition, leftTranslate.position, t);
				if (Vector2.Distance(gameObject.transform.position, leftTranslate.transform.position) == 0)
				{
					_movingLeft = false;
					t = 0;
					_startPosition = transform.position;
				}

			}
			else if (!_movingLeft)
			{
				transform.position = Vector2.Lerp(_startPosition, rightTranslate.position, t);
				if (Vector2.Distance(gameObject.transform.position, rightTranslate.transform.position) == 0)
				{
					_movingLeft = true;
					t = 0;
					_startPosition = transform.position;
				}
			}

			print(Vector2.Distance(gameObject.transform.position, leftTranslate.transform.position));
			
			
		
		}
*//*		if (isGrounded)
		{
			Vector2 velocity = rb.velocity;
			velocity.y = jumpForce;
			rb.velocity = velocity;
		}*//*
	}

*//*    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f)
		{
			Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
			Animator anim = collision.collider.GetComponent<Animator>();
			anim.Play("Jump");
			collision.collider.GetComponent<Player>().createDust();
			isGrounded = true;
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.name == "Player")
        {
			isGrounded = false;
		}
		
	}*//*

}*/
