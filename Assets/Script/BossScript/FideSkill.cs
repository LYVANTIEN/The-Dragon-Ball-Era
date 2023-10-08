using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FideSkill : MonoBehaviour
{
    private AudioSource AttackAudio;

    public float speed = 3;
    public Animator EnemyAnim;
    public Rigidbody2D EnemyRigid;
    public float chaseDistance; // Khoảng cách để bắt đầu đuổi theo player
    private Transform player;

    public AudioClip hitSound;

    public bool isFacingRight = false;

    /// Enemy Attack
    /// 
    /// 
    /// 

    public Transform AttackPos;
    public LayerMask WhatIsPlayer;
    public float attackRangeX;
    public float attackRangeY;
    public int EnemyDamage;
    public float AttackCooldown_J;
    private float CooldownTimer_J = Mathf.Infinity;
    public float AttackCooldown_1;
    private float CooldownTimer_1 = Mathf.Infinity;
    public float AttackCooldown_2;
    private float CooldownTimer_2 = Mathf.Infinity;
    // Start is called before the first frame update
    public GameObject Bullet_Boss;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {

        //----------------------
        StartCoroutine(DelayedMove());
        //---------------------------
    }

    IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(1.5f);
        BossMove();
    }
    public void BossMove()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        EnemyAnim.SetFloat("Move", 0);

        if (distanceToPlayer < chaseDistance)
        {
            // Tính toán hướng vector tới player
            Vector3 directionfollow = (player.position - transform.position).normalized;

            // Kiểm tra hướng và điều chỉnh scale
            if (directionfollow.x < 0)
            {
                // Player ở bên trái enemy, quay về bên trái
                isFacingRight = false;
                transform.localScale = new Vector2(-1, 1);

            }
            else if (directionfollow.x > 0)
            {
                // Player ở bên phải enemy, quay về bên phải
                isFacingRight = true;
                transform.localScale = new Vector2(1, 1);

            }

            StartCoroutine(DelayedEnemyAttack_2());
            // Di chuyển enemy

            EnemyAnim.SetFloat("Move", 1);
            transform.Translate(directionfollow * speed * Time.deltaTime);

            if (Mathf.Abs(player.position.x - transform.position.x) < 1f)
            {
                // Nếu player ở gần enemy theo trục X
                EnemyAttack_J();
                StartCoroutine(DelayedEnemyAttack());
            }
        }
    }
    public void EnemyAttack_J()
    {

        if (CooldownTimer_J > AttackCooldown_J)
        {

            int skillDamage = 1;
            SoundManager.instance.playSound(hitSound);
            EnemyAnim.SetTrigger("Attack");
            AttackDamage(skillDamage);
            CooldownTimer_J = 0;

        }
        CooldownTimer_J += Time.deltaTime;

    }
    IEnumerator DelayedEnemyAttack()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyAttack_1();
    }
    public void EnemyAttack_1()
    {

        if (CooldownTimer_1 > AttackCooldown_1)
        {

            int skillDamage = 2;
            SoundManager.instance.playSound(hitSound);
            EnemyAnim.SetTrigger("Attack1");
            AttackDamage(skillDamage);
            CooldownTimer_1 = 0;


        }
        CooldownTimer_1 += Time.deltaTime;

    }
    IEnumerator DelayedEnemyAttack_2()
    {
        yield return new WaitForSeconds(1f);
        EnemyAttack_2();
    }

    public void EnemyAttack_2()
    {

        if (CooldownTimer_2 > AttackCooldown_2)
        {


            EnemyAnim.SetTrigger("Attack2");
            GameObject bullet = Instantiate(Bullet_Boss, AttackPos.position, transform.rotation);

            // Lấy ra script của viên đạn
            BossBullet bulletScript = bullet.GetComponent<BossBullet>();

            // Truyền giá trị isFacingRight vào script của viên đạn
            bulletScript.Initialize(isFacingRight);
            CooldownTimer_2 = 0;


        }
        CooldownTimer_2 += Time.deltaTime;

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

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
