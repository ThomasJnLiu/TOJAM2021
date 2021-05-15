using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;

	public int numberOfPlatforms = 200;
	public float levelWidth = 3f;
	public float minY = .2f;
	public float maxY = 1.5f;

	[SerializeField]
	Sprite[] spriteArray;

	// Use this for initialization
	void Awake () {

		Vector3 spawnPosition = new Vector3();

		for (int i = 0; i < numberOfPlatforms; i++)
		{
			spawnPosition.y += Random.Range(minY, maxY);
			spawnPosition.x = Random.Range(-levelWidth, levelWidth);
			GameObject tmp = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
			int randInd = Random.Range(0, 3);
			if (randInd == 0)
			{
				Vector2[] points = new Vector2[2];
				points[0] = new Vector2(-1.2f, 0);
				points[1] = new Vector2(1.2f, 0);
				tmp.GetComponentInChildren<EdgeCollider2D>().points = points;
			}
			else if (randInd == 1)
			{
				Vector2[] points = new Vector2[2];
				points[0] = new Vector2(-0.9f, 0);
				points[1] = new Vector2(0.9f, 0);
				tmp.GetComponentInChildren<EdgeCollider2D>().points = points;
			}
			else if (randInd == 2)
            {
				Vector2[] points = new Vector2[2];
				points[0] = new Vector2(-1.2f, 0);
				points[1] = new Vector2(1.4f, 0);
				tmp.GetComponentInChildren<EdgeCollider2D>().points = points;
			}
			tmp.GetComponentInChildren<SpriteRenderer>().sprite = spriteArray[randInd];
		}
	}
}
