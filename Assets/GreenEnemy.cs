using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : MonoBehaviour, IEasyListener
{

    public Transform leftTranslate, rightTranslate;
    private Transform startTranslate, moveTranslate;

	private bool _movingLeft;

	public void OnBeat(EasyEvent audioEvent)
    {
        if (audioEvent.CurrentBeat == 1)
        {
			if (gameObject.activeSelf)
            {
				StartCoroutine(MoveEnemy(audioEvent));
			}
        }
    }
    // Start is called before the first frame update
    void Start()
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

	private IEnumerator MoveEnemy(EasyEvent audioEvent)
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
