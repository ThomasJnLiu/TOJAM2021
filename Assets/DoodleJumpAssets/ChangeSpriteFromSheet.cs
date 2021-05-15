using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteFromSheet : MonoBehaviour
{
    public enum Face { Neutral, Moving, Jump, Falling, Hurt }
    public Sprite[] spriteArray;

    public void ChangeSprite(Face ind)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[(int)ind];
    }
}
