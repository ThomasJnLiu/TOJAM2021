using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy: MonoBehaviour, IEasyListener
{

    public Transform upTranslate, downTranslate;
    private Transform startTranslate, moveTranslate;

	private bool _movingUp;

	public void OnBeat(EasyEvent audioEvent)
    {
        if (audioEvent.CurrentBeat == 1 || audioEvent.CurrentBeat == 3)
        {
            StartCoroutine(MoveEnemy(audioEvent));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
		_movingUp = (Random.value > 0.5f);
		if (_movingUp)
		{
			gameObject.transform.position = downTranslate.position;
			startTranslate = downTranslate;
			moveTranslate = upTranslate;
		}
		else
		{
			gameObject.transform.position = upTranslate.position;
			startTranslate = upTranslate;
			moveTranslate = downTranslate;
		}
	}

	private IEnumerator MoveEnemy(EasyEvent audioEvent)
	{
		var journeyTime = audioEvent.BeatLength();

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
