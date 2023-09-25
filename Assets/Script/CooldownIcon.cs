using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownIcon : MonoBehaviour
{
    public Image Cooldown_J;
    public Image Cooldown_K;
    public Image Cooldown_I;
    public Image Cooldown_U;
    public Image Cooldown_O;
    // Start is called before the first frame update
  public void updateCooldown_J(float CooldownTimer_J, float AttackCooldown_J)
    {
        Cooldown_J.fillAmount = CooldownTimer_J / AttackCooldown_J;

    }
     public void updateCooldown_K(float CooldownTimer_K, float AttackCooldown_K)
    {
        Cooldown_K.fillAmount = CooldownTimer_K / AttackCooldown_K;

    }
       public void updateCooldown_I(float CooldownTimer_I, float AttackCooldown_I)
    {
        Cooldown_I.fillAmount = CooldownTimer_I / AttackCooldown_I;

    }
       public void updateCooldown_U(float CooldownTimer_U, float AttackCooldown_U)
    {
        Cooldown_U.fillAmount = CooldownTimer_U / AttackCooldown_U;

    }
       public void updateCooldown_O(float CooldownTimer_O, float AttackCooldown_O)
    {
        Cooldown_O.fillAmount = CooldownTimer_O / AttackCooldown_O;

    }
}
