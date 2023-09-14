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
        StartCoroutine(EnemyDie());
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
IEnumerator EnemyDie(){
    if (health <= 0)
        {
            EnemyAnim.SetTrigger("Die");
            yield return new WaitForSeconds(1.0f); // Wait for 1 second
            gameObject.SetActive(false);
        }
}
    public void TakeDamage(int damage)
    {
        EnemyAnim.SetTrigger("Takehit");
        health -= damage;
        Debug.Log("Damage Taken!!!!!" + damage);
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
