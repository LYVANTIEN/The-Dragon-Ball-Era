using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
   

    public void SaveGame()
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        Debug.Log("Save!!! " + SceneManager.GetActiveScene().name);

        // Lưu các giá trị khác tương tự
        PlayerPrefs.Save();
    }

}
