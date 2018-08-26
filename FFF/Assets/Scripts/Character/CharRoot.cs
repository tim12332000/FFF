using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class CharRoot : MonoBehaviour
{
	public CharMovment CharMovment;
	public CharHealth CharHealth;

	public void Init()
	{
		CharHealth.OnDamage.AddListener(OnDamage);
		CharHealth.OnHpZero += OnHpZero;
	}

	private void OnDamage()
	{
		CharMovment.ChangeToFaill(1.2f);
	}

	public void OnTriggerEnter(Collider other)
	{

	}

	public void Release()
	{
		CharHealth.OnDamage.RemoveListener(OnDamage);
		CharHealth.OnHpZero -= OnHpZero;
	}

	private void OnHpZero()
	{
		//TODO DEAD
	}

	private void Awake()
	{
		Init();
	}
}
