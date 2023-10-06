using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public float DelaySecond = 2f;
    public string nameScene;
    public Playerplay Player;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.PlayerNextMap();
            ModeSelect();
        }
    }

    public void ModeSelect()
    {

        StartCoroutine(LoadAfterDelay());
    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(DelaySecond);
        SceneManager.LoadScene(nameScene);
    }


}
