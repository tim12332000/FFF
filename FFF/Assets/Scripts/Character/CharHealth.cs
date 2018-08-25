using UnityEngine;
using System.Collections;
using System;

public class CharHealth : MonoBehaviour
{
	public event Action OnHpZero = delegate { };

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

	public void TakeDamage(int damge)
	{
	}

	public void TakeHeal(int heal)
	{
	}
}
