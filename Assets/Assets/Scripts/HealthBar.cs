using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpBar;
    public int hp = 50;

    private void Update()
    {
        hpBar.value = hp;
        if (hp > 100)
        {
            hp = 100;
        }

        if (hp < 0)
        {
            hp = 0;
        }
    }

    public void AddHp()
    {
        hp = hp + 5;
    }

    public void SubtractHp()
    {
        hp = hp - 10;
    }
}
