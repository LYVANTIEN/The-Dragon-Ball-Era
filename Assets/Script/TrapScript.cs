using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private AudioSource AttackAudio;
    public Animator TrapAnim;
    public float chaseDistance; // Khoảng cách để bắt đầu đuổi theo player
    private Transform player;

    public AudioClip hitSound;



    /// Enemy Attack
    /// 
    /// 
    public Transform AttackPos;
    public LayerMask WhatIsPlayer;
    public float attackRangeX;
    public float attackRangeY;
    public int EnemyDamage;
    public float AttackCooldown_J;
    private float CooldownTimer_J = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {


        //----------------------
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Tính toán hướng vector tới player
            Vector3 directionfollow = (player.position - transform.position).normalized;

            if (Mathf.Abs(player.position.x - transform.position.x) < 2f)
            {
                // Nếu player ở gần enemy theo trục X
                EnemyAttack();
            }
        }


    }
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         EnemyAttack();
    //     }
    // }
    public void EnemyAttack()
    {

        if (CooldownTimer_J > AttackCooldown_J)
        {

            int skillDamage = 1;
            SoundManager.instance.playSound(hitSound);
            TrapAnim.SetTrigger("Attack");
            AttackDamage(skillDamage);
            CooldownTimer_J = 0;

        }
        CooldownTimer_J += Time.deltaTime;
    }
    public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));



    }
    public void AttackDamage(int skillDamage)
    {

        Collider2D[] playerToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsPlayer);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].GetComponent<Playerplay>().TakeDamage(EnemyDamage * skillDamage);
        }
    }



}
