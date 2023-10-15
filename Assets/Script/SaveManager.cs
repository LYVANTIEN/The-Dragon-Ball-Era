using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //public GameObject PlayerTransform;
    public Playerplay Player;

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerHP", Player.CurrentHP);
        PlayerPrefs.SetFloat("PlayerMP", Player.CurrentMP);

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
