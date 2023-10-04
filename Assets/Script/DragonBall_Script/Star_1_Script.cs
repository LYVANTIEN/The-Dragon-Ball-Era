using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_1_Script : MonoBehaviour
{
    public GameObject Star_1_UI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Star_1_UI.SetActive(true);
        }
    }
}
