using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{    // Start is called before the first frame update
    public BoxCollider2D BulletCollider;
    public Rigidbody2D bulletRigid;
    public Animator BulletAnim;
    public float BulletSpeed;


    /// Enemy Attack
    /// 
    /// 
    public Transform AttackPos;
    public LayerMask WhatIsPlayer;
    public float attackRangeX;
    public float attackRangeY;
    public int BossBulletDamage;
    private bool isFacingRight;
    private bool hit = false;

    public float DestroyTime;
    void Start()
    {

    }

    void Update()
    {


    }
    public void Initialize(bool facingRight)
    {
        isFacingRight = facingRight;
        // Đặt hướng viên đạn dựa trên giá trị của isFacingRight
        BulletScale(isFacingRight);
        StartCoroutine(DestroyAfterDelay(DestroyTime));

    }
    public void BulletScale(bool isFacingRight)
    {
        if (isFacingRight == true)
        {
            transform.localScale = new Vector2(1, 1);
            bulletRigid.velocity = transform.right * BulletSpeed;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            bulletRigid.velocity = -transform.right * BulletSpeed;
        }
    }


    IEnumerator DestroyAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void BulletAttack()
    {

        int skillDamage = 1;
        AttackDamage(skillDamage);

    }
    public void AttackDamage(int skillDamage)
    {

        Collider2D[] playerToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, WhatIsPlayer);
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].GetComponent<Playerplay>().TakeDamage(BossBulletDamage * skillDamage);
        }

    }

    public void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit && collision.CompareTag("Player")) // Kiểm tra xem đã va chạm và collider là của đối tượng Enemy
        {
            hit = true; // Đánh dấu rằng viên đạn đã va chạm

            // Giữ yên vị của viên đạn tại vị trí va chạm
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            // Gọi hàm tấn công
            BulletAttack();
            BulletAnim.SetTrigger("Destroy");
            // Chạy animation và destroy viên đạn sau khoảng thời gian
            StartCoroutine(DestroyAfterDelay(0.5f)); // Thay thế 0.5f bằng thời gian bạn muốn
        }

    }
}
