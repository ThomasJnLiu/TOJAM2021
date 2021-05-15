using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource _audioSource;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.time = time;
    }

    public void StartMusic()
    {
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
