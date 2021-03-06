using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    // FMOD
    [EventRef] public string myEventPath; // A reference to the FMOD event we want to use
    public EasyEvent myAudioEvent; // EasyEvent is the object that will play the FMOD audio event, and provide our callbacks and other related info

    // You can pass an array of IEasyListeners through to the FMOD event, but we have to serialize them as objects.
    // You have to drag the COMPONENT that implements the IEasyListener into the object, or it won't work properly.
    [RequireInterface(typeof(IEasyListener))]
    public Object[] listeners;

    // Each platform listens to the song
    GameObject[] platforms;
    GameObject[] greenEnemies;
    GameObject[] redEnemies;

    [SerializeField]
    int _firstBeatDrop, _loadTime;

    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
        greenEnemies = GameObject.FindGameObjectsWithTag("GreenEnemy");
        redEnemies = GameObject.FindGameObjectsWithTag("RedEnemy");
        // Passes the EventReference so EasyEvent can create the FMOD Event instance
        // Passes an array of listeners through (IEasyListener) so the audio event knows which objects want to listen to the callbacks


        // You could also pass in a single listener, even if it's referenced as something other than IEasyListener.
        // As long as it implements IEasyListner, it will be passed through as if it IS IEasyListener.
        // For example:


        PauseGame();
        StartCoroutine(LoadGame(_loadTime));
    }



    IEnumerator LoadGame(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        ResumeGame();
        //GameObject.FindObjectOfType<AudioManager>().myAudioEvent.start();

        myAudioEvent = new EasyEvent(myEventPath, listeners);
        myAudioEvent.start();
        



        foreach (GameObject platform in platforms)
        {
            myAudioEvent.AddListener(platform.GetComponent<Platform>());
        }

        foreach (GameObject greenEnemy in greenEnemies)
        {
            myAudioEvent.AddListener(greenEnemy.GetComponent<GreenEnemy>());
        }

        foreach (GameObject redEnemy in redEnemies)
        {
            myAudioEvent.AddListener(redEnemy.GetComponent<RedEnemy>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    /*    public void Update()
        {
            // Press space bar to start and stop the audio event
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!myAudioEvent.IsPlaying())
                {
                    myAudioEvent.start();
                }

                else
                {
                    myAudioEvent.stop();
                }

            }
        }*/
}