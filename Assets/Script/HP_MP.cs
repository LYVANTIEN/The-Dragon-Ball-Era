using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_MP : MonoBehaviour
{
    public Image _HP;
    public Image _MP;
    // Start is called before the first frame update
    public void updateHP(float CurrentHP, float MaxHP)
    {
        _HP.fillAmount = CurrentHP / MaxHP;

    }
    public void updateMP(float CurrentMP, float MaxMP)
    {
        _MP.fillAmount = CurrentMP / MaxMP;

    }
}
