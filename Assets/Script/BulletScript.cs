using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BoxCollider2D BulletCollider;
    public Animator BulletAnim;
    public float BulletSpeed;
    public bool hit;
    public float direction;
    private float lifeTime;


    public float chaseDistance; // Khoảng cách để bắt đầu đuổi theo player



    /// Enemy Attack
    /// 
    /// 
    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int BulletDamage;

    void Start()
    {

    }

    void Update()
    {
        //---------------------
        if (hit)
        {
            return;
        }

        float movementSpeed = BulletSpeed * Time.deltaTime;
        // Adjust the movement direction based on the 'direction' variable
        transform.Translate(movementSpeed * direction, 0, 0);
        lifeTime += Time.deltaTime;

        if (lifeTime > 3f)
        {
            gameObject.SetActive(false);
        }


    }
    public void BulletAttack()
    {

        int skillDamage = 1;
        AttackDamage(skillDamage);

    }
    public void AttackDamage(int skillDamage)
    {

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(BulletDamage * skillDamage);
        }
    }

    public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));



    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        hit = true;
        BulletCollider.enabled = false;
        BulletAttack();
        //gameObject.SetActive(false);
        BulletAnim.SetTrigger("Destroy");
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        BulletCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    public void Deactitive()
    {
        gameObject.SetActive(false);
    }
}
