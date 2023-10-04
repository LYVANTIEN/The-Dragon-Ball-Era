using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_3_Script : MonoBehaviour
{
 public GameObject Star_3_UI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Star_3_UI.SetActive(true);
        }
    }
}
