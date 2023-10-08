using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FideBoss : MonoBehaviour
{

    public HP_MP HPbox;
    public float CurrentHP;
    public float MaxHP = 30;
    public Animator EnemyAnim;

    void Start()
    {
        CurrentHP = MaxHP;
        HPbox.updateHP(CurrentHP, MaxHP);
    }

    void Update()
    {
        StartCoroutine(EnemyDie());
    }


    IEnumerator EnemyDie()
    {
        if (CurrentHP <= 0)
        {
            EnemyAnim.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f); // Wait for 1 second
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        EnemyAnim.SetTrigger("Takehit");
        CurrentHP -= damage;
        HPbox.updateHP(CurrentHP, MaxHP);
        Debug.Log("Damage Taken!!!!!" + damage);
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
