using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class SamuraiMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private AudioSource AttackAudio;
    public Transform Player;

    public float speed = 5;
    public float leftright;
    public bool isFacingRight = true;
    public float flapchange;
    public bool canJump;
    public bool canDoubleJump;

    public AudioClip hitSound;
    public AudioClip jumpSound;


    public float TimeToAttack;
    public float startTimeToAttack;
    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    public float attackRange;
    public int PlayerDamage;



    // Start is called before the first frame update
    void Start()
    {

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
            SoundManager.instance.playSound(hitSound);
            anim.SetTrigger("Attack");

        }
        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            SoundManager.instance.playSound(hitSound);
            anim.SetTrigger("Attack2");

        }
        ///
        if (TimeToAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.I) == true)
            {
                SoundManager.instance.playSound(hitSound);
                anim.SetTrigger("Attack3");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(AttackPos.position, attackRange, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(PlayerDamage);
                }
            }

            TimeToAttack = startTimeToAttack;


        }
        else
        {
            TimeToAttack -= Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.U) == true)
        {
            anim.SetTrigger("SuperAttack");
            SoundManager.instance.playSound(hitSound);

        }
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            SoundManager.instance.playSound(hitSound);
            anim.SetTrigger("Guard");

        }
        if (Input.GetKey(KeyCode.O) == true)
        {
            // if (Input.GetKeyDown(KeyCode.O) == true)
            // {
            //     Player.position = new Vector3(Player.position.x, Player.position.y + 0.3f, Player.position.z);
            // }
            anim.SetBool("Manaup", true);

        }
        else
        {
            anim.SetBool("Manaup", false);
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, attackRange);
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
