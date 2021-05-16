using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;
	public GameObject greenMonsterPrefab;
	public GameObject redMonsterPrefab;

	public int numberOfPlatforms = 10;
	public float levelWidth = 3f;
	public float minY = .2f;
	public float maxY = 1.5f;

	private int greenEnemySpawnRate = 2;
	private int curGreenEnemy = 0;

	private int redEnemySpawnRate = 5;
	private int curRedEnemy = 0;

	[SerializeField]
	Sprite[] spriteArray;

	// Use this for initialization
	void Awake () {

		Vector3 spawnPosition = new Vector3();
		Vector3 greenMonsterPosition = new Vector3();
		Vector3 redMonsterPosition = new Vector3();

		for (int i = 0; i < numberOfPlatforms; i++)
		{
			spawnPosition.y += Random.Range(minY, maxY);
			spawnPosition.x = Random.Range(-levelWidth, levelWidth);

			greenMonsterPosition.y = spawnPosition.y + .75f;
			// Guarentee green monster is at somewhat faraway from same platform level
			greenMonsterPosition.x = -spawnPosition.x;
			if (spawnPosition.x > 0)
            {
				greenMonsterPosition.x -= 1.5f;

			} else
            {
				greenMonsterPosition.x += 1.5f;
			}
			
			

			redMonsterPosition.y += Random.Range(minY, maxY);
			redMonsterPosition.x = (greenMonsterPosition.x < 0) ? levelWidth + 1.2f  : -levelWidth - 1.2f;

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

			
			// Final platform dont spawn enemies
			if (i == numberOfPlatforms - 1)
            {
				tmp.GetComponentInChildren<Platform>().isLastSectionPlatform = true;
				tmp.GetComponentInChildren<Platform>().isMoving = false;
				tmp.GetComponentInChildren<Platform>().gameObject.tag = "FirstPlatform";
				tmp.transform.position = new Vector3(0, tmp.transform.position.y, 0);
				Vector2[] points = new Vector2[2];
				points[0] = new Vector2(4.125f, 0);
				points[1] = new Vector2(-4.125f, 0);
				tmp.GetComponentInChildren<EdgeCollider2D>().points = points;
				tmp.GetComponentInChildren<EdgeCollider2D>().offset = new Vector2(0, -0.7f);


				tmp.GetComponentInChildren<SpriteRenderer>().sprite = spriteArray[3];
			} else
            {
				// Start spawning green enemies every 5 platforms and start guitar track in FMOD
				if (i > numberOfPlatforms / 4)
				{
					tmp.GetComponentInChildren<Platform>().isGreenEnemyPlatform = true;
					if (curGreenEnemy >= greenEnemySpawnRate)
					{
						GameObject tmpGreen = Instantiate(greenMonsterPrefab, greenMonsterPosition, Quaternion.identity);
						curGreenEnemy = 0;
					}

				}
				// Start spawning red enemies every 10 platforms and start guitar track in FMOD
				if (i > numberOfPlatforms / 2)
				{
					tmp.GetComponentInChildren<Platform>().isRedEnemyPlatform = true;
					if (curRedEnemy >= redEnemySpawnRate)
					{
						GameObject tmpRed = Instantiate(redMonsterPrefab, redMonsterPosition, Quaternion.identity);
						curRedEnemy = 0;
					}
				}
			}

            curGreenEnemy += 1;
			curRedEnemy += 1;
		}
	}
}
