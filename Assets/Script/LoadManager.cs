using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    public Button buttonContinue;

    private void Start()
    {
        buttonContinue.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("SceneName", "Map2"));
        });
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
    }
}
