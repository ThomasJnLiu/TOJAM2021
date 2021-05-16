using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart(){
        StartCoroutine("LoadGame");
    }

    public IEnumerator LoadGame(){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main");
    }
}
