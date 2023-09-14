using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SamuraiMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private AudioSource AttackAudio;
    public Transform Player;
    //Hp va Mp
    public HP_MP HPbox;
    public HP_MP MPbox;
    public float CurrentHP;
    public float MaxHP = 100;
    public float CurrentMP;
    public float MaxMP = 100;

    public float speed = 5;
    public float leftright;
    public bool isFacingRight = true;
    public float flapchange;
    public float dashchange;
    public bool canJump;
    public bool canDoubleJump;

    public AudioClip hitSound;
    public AudioClip jumpSound;


    public float TimeToAttack;
    public float startTimeToAttack;
    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int PlayerDamage;



    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
        HPbox.updateHP(CurrentHP, MaxHP);
        MPbox.updateMP(CurrentMP, MaxMP);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Jump();


    }
    public void Move()
    {
        leftright = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * leftright, rb.velocity.y);
        if (isFacingRight == true && leftright == -1)
        {
            transform.localScale = new Vector2(-1, 1);
            isFacingRight = false;
        }
        if (isFacingRight == false && leftright == 1)
        {
            transform.localScale = new Vector2(1, 1);
            isFacingRight = true;
        }
        /// Animation
        anim.SetFloat("Move", Mathf.Abs(leftright));


    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) == true)
        {
            float ManaUse = 5;
            if (ManaUse <= CurrentMP)
            {
                CurrentMP -= ManaUse;
                MPbox.updateMP(CurrentMP, MaxMP);
                int skillDamage = 1;
                SoundManager.instance.playSound(hitSound);
                anim.SetTrigger("Attack");
                AttackDamage(skillDamage);
            }

        }
        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            float ManaUse = 8;
            if (ManaUse <= CurrentMP)
            {
                CurrentMP -= ManaUse;
                MPbox.updateMP(CurrentMP, MaxMP);
                int skillDamage = 2;
                SoundManager.instance.playSound(hitSound);
                anim.SetTrigger("Attack2");
                AttackDamage(skillDamage);
            }

        }

        if (Input.GetKeyDown(KeyCode.I) == true)
        {
            float ManaUse = 12;
            if (ManaUse <= CurrentMP)
            {
                CurrentMP -= ManaUse;
                MPbox.updateMP(CurrentMP, MaxMP);
                int skillDamage = 3;
                SoundManager.instance.playSound(hitSound);
                anim.SetTrigger("Attack3");
                AttackDamage(skillDamage);
            }
        }

        if (Input.GetKeyDown(KeyCode.U) == true)
        {
            StartCoroutine(PerformSuperAttack());
        }
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            SoundManager.instance.playSound(hitSound);
            anim.SetTrigger("Guard");

        }
        if (Input.GetKey(KeyCode.O) == true)
        {
            if (CurrentMP >= MaxHP)
            {
            }
            else
            {
                ///CurrentMP += 0.015f;
                CurrentMP += 0.05f;
                MPbox.updateMP(CurrentMP, MaxMP);
                anim.SetBool("Manaup", true);
            }
        }
        else
        {
            anim.SetBool("Manaup", false);
        }


    }
    IEnumerator PerformSuperAttack()
    {
        float ManaUse = 85;
        if (ManaUse <= CurrentMP)
        {
            CurrentMP -= ManaUse;
            MPbox.updateMP(CurrentMP, MaxMP);
            int skillDamage = 10;
            anim.SetTrigger("SuperAttack");
            SoundManager.instance.playSound(hitSound);

            yield return new WaitForSeconds(1.0f); // Wait for 1 second

            // Continue with the rest of the code
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(7, attackRangeY), 0, WhatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(PlayerDamage * skillDamage);
            }
            // AttackDamage(skillDamage);
        }
    }

    public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
        Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));



    }
    public void AttackDamage(int skillDamage)
    {
        /// <summary>
        /// Dame nhan he so sat thuong skill
        /// </summary>

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(PlayerDamage * skillDamage);
        }
    }

    public void Jump()
    {
        //jump
        if (canJump && !Input.GetKey(KeyCode.Space))
        {
            canDoubleJump = false;
        }
        if ((Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.W)) && (canJump || canDoubleJump))
        {
            SoundManager.instance.playSound(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, flapchange);
            anim.SetBool("isJumping", true);

            canDoubleJump = !canDoubleJump;
        }

    }


    private void OnTriggerEnter2D(Collider2D otherhitbox)
    {
        if (otherhitbox.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherhitbox)
    {
        if (otherhitbox.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
}
