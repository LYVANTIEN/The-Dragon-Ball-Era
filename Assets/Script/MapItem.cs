using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public int health;
    public float speed;
    public Animator MapItemAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MapItemDie());
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    IEnumerator MapItemDie()
    {
        if (health <= 0)
        { 
            yield return new WaitForSeconds(1.0f); // Wait for 1 second
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken!!!!!" + damage);
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
