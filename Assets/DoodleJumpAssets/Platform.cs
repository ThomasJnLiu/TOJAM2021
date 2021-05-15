using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	[SerializeField]
	float timeToReachTarget;

	float t = 0;

	public float jumpForce = 10f;
	public Transform leftTranslate, rightTranslate;
	public float speed = 5f;
	public bool isMoving;
	private bool _movingLeft;

	Vector2 _startPosition;

	MusicTiming _musicTiming;

    private void Start()
    {
		_musicTiming = GameObject.FindObjectOfType<MusicTiming>();
		if (isMoving)
        {
			_movingLeft = (Random.value > 0.5f);
			if (_movingLeft)
			{
				gameObject.transform.position = rightTranslate.position;

			}
			else
			{
				gameObject.transform.position = leftTranslate.position;
			}
			_startPosition = transform.position;
		}
		
		
	}

    private void FixedUpdate()
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
		
	

	}

    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.y <= 0f)
		{
			Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				Vector2 velocity = rb.velocity;
				velocity.y = jumpForce;
				rb.velocity = velocity;
			}
		}
	}

}
