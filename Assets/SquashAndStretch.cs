using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Vector3 squashSize;
    [SerializeField] private Vector3 stretchSize;
    [SerializeField] private float transformRate = 10;


    private Vector3 originalSize;
    private Vector3 targetSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
        targetSize = originalSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //squash and stretch
        transform.localScale = Vector3.Lerp(transform.localScale, targetSize, transformRate * Time.deltaTime);
    }

    public void SetToSquash() {
        targetSize = squashSize;
    }

    public void SetToSquash(float duration)
    {
        StartCoroutine(SquashTimer(duration));
    }

    public void customSquish(Vector3 newSize)
    {
        targetSize = newSize;
    }

    public void customSquish(Vector3 newSize, float duration)
    {
        StartCoroutine(SquishTimer(newSize, duration));
    }

    public void SetToStretch()
    {
        targetSize = stretchSize;
    }

    public void SetToStretch(float duration)
    {
        StartCoroutine(StretchTimer(duration));
    }





    public void resetScale()
    {
        targetSize = originalSize;
    }

    IEnumerator SquashTimer(float duration)
    {
        targetSize = squashSize;
        yield return new WaitForSeconds(duration);
        targetSize = originalSize;
    }

    IEnumerator StretchTimer(float duration)
    {
        targetSize = stretchSize;
        yield return new WaitForSeconds(duration);
        targetSize = originalSize;
    }

    IEnumerator SquishTimer(Vector3 newSize, float duration)
    {
        targetSize = newSize;
        yield return new WaitForSeconds(duration);
        targetSize = originalSize;
    }


}
