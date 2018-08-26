using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class CharRoot : MonoBehaviour
{
	public CharMovment CharMovment;
	public CharHealth CharHealth;

	[SerializeField]
	private Animator RunningAnim;

	[SerializeField]
	private float _animSpeed;

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
		GlobelEvents.Instance.GameOver.Invoke();
	}

	private void Update()
	{
		if (GlovelSetting.Instance.IsEnterGame == false)
			return;

		_animSpeed = (-1 * StageManager.Instance.GetSpeed()) / 20;
		RunningAnim.speed = _animSpeed;
	}

	private void Awake()
	{
		Init();
	}
}
