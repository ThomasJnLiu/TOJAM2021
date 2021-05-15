using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this only works for sprite objects
public class SineWaveHover : MonoBehaviour
{
    [SerializeField] float effectSpeed = 1;
    [SerializeField] float effectMultiplier = 1;

    float yPos;
    Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        yPos = Mathf.Sin(Time.time * effectSpeed) * effectMultiplier;
        transform.localPosition = originalPos + new Vector3(0, yPos, 0);
    }
}
