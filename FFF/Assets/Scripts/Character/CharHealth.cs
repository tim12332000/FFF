using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using Gameplay.Utility;

public class CharHealth : MonoBehaviour
{
    public event Action OnHpZero = delegate { };
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;

    public int HpMax = 3;

    [SerializeField]
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = Hp;
        }
    }

    public bool IsInvincible = false;

    [ContextMenu("TestDmage")]
    public void TestDmage()
    {
        TakeDamage(1);
    }
    [ContextMenu("TestHeal")]
    public void TestHeal()
    {
        TakeHeal(1);
    }

    public void TakeDamage(int damage)
    {
        if (IsInvincible)
        {
            return;
        }

        _hp -= damage;

        if (_hp < 0)
        {
            _hp = 0;
            OnHpZero();
        }

        GameUIValue.Instance.Life = _hp;

        OnDamage.Invoke();
    }

    public void TakeHeal(int heal)
    {
        _hp += heal;
        if (_hp >= HpMax)
        {
            _hp = HpMax;
        }

        GameUIValue.Instance.Life = _hp;

        OnHeal.Invoke();
    }

    public void TakeInvincible()
    {
        IsInvincible = true;
        ScheduleHelper.Instance.DelayDo(CloseInvincible, 3);
    }

    public void CloseInvincible()
    {
        IsInvincible = false;
    }

    private void Awake()
    {
        _hp = HpMax;
    }
}
