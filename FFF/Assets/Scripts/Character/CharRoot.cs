using UnityEngine;
using System.Collections;
using System;

public class CharRoot : MonoBehaviour
{
	public CharMovment CharMovment;
	public CharHealth CharHealth;

	public void Init()
	{
		CharHealth.OnHpZero += OnHpZero;
	}

	public void OnTriggerEnter(Collider other)
	{
		
	}

	public void Release()
	{
		CharHealth.OnHpZero -= OnHpZero;
	}

	private void OnHpZero()
	{
		//TODO DEAD
	}
}
