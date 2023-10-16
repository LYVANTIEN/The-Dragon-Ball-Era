using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    //public GameObject PlayerTransform;
    public Playerplay Player;

    //public Button buttonContinue;

    // private void Start() {
    //     buttonContinue.onClick.AddListener(() => {
    //        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName", "Map2"))      ;
    //     });
    // }

    public void SaveGame()
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlayerPosX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerHP", Player.CurrentHP);
        PlayerPrefs.SetFloat("PlayerMP", Player.CurrentMP);
        Debug.Log("Save!!! " + Player.transform.position.x);

        // Lưu các giá trị khác tương tự
        PlayerPrefs.Save();
    }

    public void LoadGame() {
    if(PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY")) {
        float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
        float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
        Vector3 playerPosition = new Vector3(playerPosX, playerPosY, Player.transform.position.z);
        Player.transform.position = playerPosition;
    }

    if(PlayerPrefs.HasKey("PlayerHP")) {
        float playerHP = PlayerPrefs.GetFloat("PlayerHP");
        // Gán giá trị HP cho player
    }

    if(PlayerPrefs.HasKey("PlayerMP")) {
        float playerMP = PlayerPrefs.GetFloat("PlayerMP");
        // Gán giá trị MP cho player
    }

    // Kiểm tra và gán các giá trị khác tương tự
}
}
