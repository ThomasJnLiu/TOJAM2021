using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTiming : MonoBehaviour
{
    [SerializeField]
    int _firstBeatDrop, _loadTime;
    public bool firstBeatHasDropped = false;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        StartCoroutine(LoadGame(_loadTime));
        StartCoroutine(FirstBeatDrop(_firstBeatDrop));

    }

/*    // Update is called once per frame
    void Update()
    {
        
    }*/

    IEnumerator LoadGame(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        Time.timeScale = 1;
        //GameObject.FindObjectOfType<AudioManager>().myAudioEvent.start();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerator FirstBeatDrop(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        firstBeatHasDropped = true;
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
