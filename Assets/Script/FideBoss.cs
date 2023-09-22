using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FideBoss : MonoBehaviour
{
    private AudioSource AttackAudio;

    public Animator FideBossAnim;
    public Rigidbody2D EnemyRigid;
    public float chaseDistance; // Khoảng cách để bắt đầu đuổi theo player
    private Transform player;

    public AudioClip hitSound;
    public Transform AttackPos;
    public LayerMask WhatIsPlayer;

    public int health;
    public float speed = 3;

    /// Enemy Attack
    /// 
    /// 

    public float attackRangeX;
    public float attackRangeY;
    public int EnemyDamage;
    private float AttackCooldown_J = 1f;
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
       FideBossAnim.SetFloat("Move", 0);

        if (distanceToPlayer < chaseDistance)
        {
            // Tính toán hướng vector tới player
            Vector3 directionfollow = (player.position - transform.position).normalized;

            // Kiểm tra hướng và điều chỉnh scale
            if (directionfollow.x < 0)
            {
                // Player ở bên trái enemy, quay về bên trái
                transform.localScale = new Vector2(-1, 1);
            }
            else if (directionfollow.x > 0)
            {
                // Player ở bên phải enemy, quay về bên phải
                transform.localScale = new Vector2(1, 1);
            }

            // Di chuyển enemy

            FideBossAnim.SetFloat("Move", 1);
            transform.Translate(directionfollow * speed * Time.deltaTime);

            if (Mathf.Abs(player.position.x - transform.position.x) < 0.5f)
            {
                // Nếu player ở gần enemy theo trục X
                EnemyAttack();
            }
        }
        //---------------------------


        StartCoroutine(EnemyDie());
    }
    public void EnemyAttack()
    {

        if (CooldownTimer_J > AttackCooldown_J)
        {

            int skillDamage = 1;
            SoundManager.instance.playSound(hitSound);
            FideBossAnim.SetTrigger("Attack");
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


    IEnumerator EnemyDie()
    {
        if (health <= 0)
        {
           FideBossAnim.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f); // Wait for 1 second
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
       FideBossAnim.SetTrigger("Takehit");
        health -= damage;
        Debug.Log("Damage Taken!!!!!" + damage);
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
