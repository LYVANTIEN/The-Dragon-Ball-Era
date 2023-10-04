using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Playerplay : MonoBehaviour
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



    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    public LayerMask WhatIsMapItem;
    public float attackRangeX;
    public float attackRangeY;
    public int PlayerDamage;

    /// <summary>
    ///Cooldown all skill
    /// </summary>
    /// Button J
    public CooldownIcon cooldownIcon_J;
    public CooldownIcon cooldownIcon_K;
    public CooldownIcon cooldownIcon_I;
    public CooldownIcon cooldownIcon_U;
    public CooldownIcon cooldownIcon_O;
    public CooldownIcon cooldownIcon_L;
    private float AttackCooldown_J = 1f;
    private float CooldownTimer_J = Mathf.Infinity;
    // Button K
    private float AttackCooldown_K = 4f;
    private float CooldownTimer_K = Mathf.Infinity;

    private float AttackCooldown_L = 4f;
    private float CooldownTimer_L = Mathf.Infinity;
    // Button I
    private float AttackCooldown_I = 8f;
    private float CooldownTimer_I = Mathf.Infinity;
    // Button U
    private float AttackCooldown_U = 15f;
    private float CooldownTimer_U = Mathf.Infinity;
    private float AttackCooldown_O = 10f;
    private float CooldownTimer_O = Mathf.Infinity;

    private NPC_Controller npc;
    public CheckpointManager checkpointManager;
    // public BulletScript bulletScript;
    // public GameObject[] Vegito_Bullet;

    public GameObject Bullet_Vegito;
public Transform BulletPos;





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

        if (!inDialogue())
        {
            Move();
            //Debug.Log(leftright);
            Attack();
            Jump();
            StartCoroutine(PlayerDie());
        }
    }
    private bool inDialogue()
    {
        if (npc != null)
        { return npc.DialogueActive(); }
        else
        {
            return false;
        }
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
        //---------------------------------Button J
        if (Input.GetKeyDown(KeyCode.J) == true && CooldownTimer_J > AttackCooldown_J)
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
                CooldownTimer_J = 0;
            }
        }
        CooldownTimer_J += Time.deltaTime;
        cooldownIcon_J.updateCooldown_J(CooldownTimer_J, AttackCooldown_J);

        //-----------------------------------------
        if (Input.GetKeyDown(KeyCode.K) == true && CooldownTimer_K > AttackCooldown_K)
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
                CooldownTimer_K = 0;
            }
        }
        CooldownTimer_K += Time.deltaTime;
        cooldownIcon_K.updateCooldown_K(CooldownTimer_K, AttackCooldown_K);

        ///-------------------------------------------

        if (Input.GetKeyDown(KeyCode.I) == true && CooldownTimer_I > AttackCooldown_I)
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
                CooldownTimer_I = 0;
            }
        }
        CooldownTimer_I += Time.deltaTime;
        cooldownIcon_I.updateCooldown_I(CooldownTimer_I, AttackCooldown_I);
        // cooldownIcon.updateCooldown_I(CooldownTimer_I, AttackCooldown_I);
        ///-------------------------------------

        if (Input.GetKeyDown(KeyCode.U) == true && CooldownTimer_U > AttackCooldown_U)
        {
            StartCoroutine(PerformSuperAttack());
        }
        CooldownTimer_U += Time.deltaTime;
        cooldownIcon_U.updateCooldown_U(CooldownTimer_U, AttackCooldown_U);
        //-------------------------------------

        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            SoundManager.instance.playSound(hitSound);
            anim.SetTrigger("Guard");

        }
        if (Input.GetKey(KeyCode.O) == true && CooldownTimer_O > AttackCooldown_O)
        {

            if (CurrentHP >= MaxHP && CurrentMP >= MaxMP)
            {
                CooldownTimer_O = 0;
            }
            else if (CurrentHP >= MaxHP && CurrentMP <= MaxMP)
            {
                //khong tang hp nua nhung van tang mp 
            }
            else
            {
                ///CurrentMP += 0.015f;
                CurrentHP += 0.1f;
                HPbox.updateHP(CurrentHP, MaxHP);
                anim.SetBool("Manaup", true);

            }

            if (CurrentMP >= MaxMP && CurrentHP >= MaxHP)
            {
                CooldownTimer_O = 0;
            }
            else if (CurrentMP >= MaxMP && CurrentHP <= MaxHP)
            {
                //khong tang mp nua nhung van tang hp 
            }
            else
            {
                ///CurrentMP += 0.015f;   
                CurrentMP += 0.1f;
                MPbox.updateMP(CurrentMP, MaxMP);
                anim.SetBool("Manaup", true);

            }
        }
        else
        {
            anim.SetBool("Manaup", false);
        }
        CooldownTimer_O += Time.deltaTime;
        cooldownIcon_O.updateCooldown_O(CooldownTimer_O, AttackCooldown_O);

        //--------------------------------------------Bullet skill
        Skill_L_bullet();




    }

    public void Skill_L_bullet()
    {
        if (Input.GetKeyDown(KeyCode.L) == true && CooldownTimer_L > AttackCooldown_L)
        {
            float ManaUse = 10;
            if (ManaUse <= CurrentMP)
            {
                CurrentMP -= ManaUse;
                MPbox.updateMP(CurrentMP, MaxMP);
                SoundManager.instance.playSound(hitSound);
                anim.SetTrigger("Bullet");
                //goi object bullet
                //--------------------------------------------------


                // Gọi Instantiate và lưu lại đối tượng viên đạn được tạo ra
                GameObject bullet = Instantiate(Bullet_Vegito, BulletPos.position, BulletPos.rotation);

                // Lấy ra script của viên đạn
                Bullet bulletScript = bullet.GetComponent<Bullet>();

                // Truyền giá trị isFacingRight vào script của viên đạn
                bulletScript.Initialize(isFacingRight);


                // int skillDamage = 3;
                // AttackDamage(skillDamage);
                CooldownTimer_L = 0;
            }
        }
        CooldownTimer_L += Time.deltaTime;
        cooldownIcon_L.updateCooldown_L(CooldownTimer_L, AttackCooldown_L);
    }

    // public int FindVegito_Bullet()
    // {
    //     for (int i = 0; i < Vegito_Bullet.Length; i++)
    //     {
    //         if (!Vegito_Bullet[i].activeInHierarchy)

    //         {
    //             return i;
    //         }
    //     }

    //     return 0;
    // }
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
            Collider2D[] MapItemToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(7, attackRangeY), 0, WhatIsMapItem);
            for (int i = 0; i < MapItemToDamage.Length; i++)
            {
                MapItemToDamage[i].GetComponent<MapItem>().TakeDamage(PlayerDamage * skillDamage);
            }
            CooldownTimer_U = 0;
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
        /// 

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(PlayerDamage * skillDamage);
        }

        //
        Collider2D[] MapItemToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsMapItem);
        for (int i = 0; i < MapItemToDamage.Length; i++)
        {
            MapItemToDamage[i].GetComponent<MapItem>().TakeDamage(PlayerDamage * skillDamage);
        }


    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Takehit");
        CurrentHP -= damage;
        HPbox.updateHP(CurrentHP, MaxHP);
        Debug.Log("Player Damage Taken!!!!!" + damage);
    }

    IEnumerator PlayerDie()
    {
        if (CurrentHP <= 0)
        {
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f); // Wait for 1 second
            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
            HPbox.updateHP(CurrentHP, MaxHP);
            MPbox.updateMP(CurrentMP, MaxMP);
            transform.position = checkpointManager.lastCheckpointPosition;
        }
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
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
    public void PlayerNextMap()
    {
        anim.SetTrigger("NextMap");
    }

    private void OnTriggerEnter2D(Collider2D otherhitbox)
    {
        if (otherhitbox.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            canJump = true;
        }
    }

  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Controller>();
           
            if (Input.GetKey(KeyCode.E))
            //anim.SetTrigger("Idle");
               npc.ActivateDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D otherhitbox)
    {
        npc = null;
        if (otherhitbox.gameObject.tag == "Ground")
        {
            canJump = false;
        }

    }

   
}
