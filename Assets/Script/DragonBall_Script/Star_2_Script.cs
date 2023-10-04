using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_2_Script : MonoBehaviour
{
    public GameObject Star_2_UI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Star_2_UI.SetActive(true);
        }
    }
}
