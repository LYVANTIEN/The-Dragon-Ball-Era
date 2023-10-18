using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGoku : MonoBehaviour
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
    public float AttackCooldown_Tele;
    private float CooldownTimer_Tele = Mathf.Infinity;
    public float AttackCooldown_PU;
    private float CooldownTimer_PU = Mathf.Infinity;
    public Transform Player;
    // Start is called before the first frame update
    public GameObject Bullet_Boss;
    public FideBoss BossInfo;
    private bool isAttacking = false;


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
            EnemyAttack_PowerUp();
            EnemyAttack_Tele();
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

    public void EnemyAttack_Tele()
    {

        if (CooldownTimer_Tele > AttackCooldown_Tele)
        {

            if (player != null) // Kiểm tra player có tồn tại không
            {
                EnemyAnim.SetTrigger("Tele");
                transform.position = player.position;

            }


            CooldownTimer_Tele = 0;

        }
        CooldownTimer_Tele += Time.deltaTime;

    }

    public void EnemyAttack_PowerUp()
    {

        if (CooldownTimer_PU > AttackCooldown_PU && BossInfo.CurrentHP < 60)
        {

            EnemyAnim.SetTrigger("PowerUp");
            BossInfo.TakeDamage(-150);

            CooldownTimer_PU = 0;

        }
        CooldownTimer_PU += Time.deltaTime;

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

        if (!isAttacking && CooldownTimer_2 > AttackCooldown_2)
        {


            StartCoroutine(ShootBullets());
            CooldownTimer_2 = 0;


        }
        CooldownTimer_2 += Time.deltaTime;

    }
    private IEnumerator ShootBullets()
    {
        isAttacking = true;

        EnemyAnim.SetTrigger("Attack2");

        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(Bullet_Boss, AttackPos.position, transform.rotation);

            BossBullet bulletScript = bullet.GetComponent<BossBullet>();
            bulletScript.Initialize(isFacingRight);

            yield return new WaitForSeconds(0.5f); // Chờ 0.3 giây trước khi bắn viên đạn tiếp theo
        }

        isAttacking = false;
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
