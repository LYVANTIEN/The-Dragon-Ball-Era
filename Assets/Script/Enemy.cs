using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public Animator EnemyAnim;
    public Rigidbody2D EnemyRigid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 ){
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken!!!!!"+ damage);
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
