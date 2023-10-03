using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_6_Script : MonoBehaviour
{
    public GameObject Star_6_UI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Star_6_UI.SetActive(true);
        }
    }
}
