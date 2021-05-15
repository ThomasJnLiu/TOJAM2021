using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTiming : MonoBehaviour
{
    [SerializeField]
    int _firstBeatDrop;
    public bool firstBeatHasDropped = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstBeatDrop(_firstBeatDrop));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FirstBeatDrop(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        firstBeatHasDropped = true;
    }
}
