using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Utility;
using UnityEngine;
using VacuumShaders.CurvedWorld;

[Serializable]
public class StageEvent
{
	public float TriggerTime;
	public float BlendXTo;
	public float BlendYTo;
	public float DurationTime;
}

public class StageManager : MonoBehaviour
{
	public static StageManager Instance;

	public GroundScrollController StageGround;
	public CurvedWorld_Controller CurvedWorldController;

	private float _timer;

	private void SetSpeed(float f)
	{
		StageGround.ScrollSpeed = f;
	}
	private void SetBlendY(float f)
	{
		CurvedWorldController._V_CW_Bend_Y = f;
	}
	private void SetBlendX(float f)
	{
		CurvedWorldController._V_CW_Bend_X = f;
	}

	private float GetBlendY()
	{
		return CurvedWorldController._V_CW_Bend_Y;
	}
	private float GetBlendX()
	{
		return CurvedWorldController._V_CW_Bend_X;
	}
	private float GetSpeed()
	{
		return StageGround.ScrollSpeed;
	}

	public void Update()
	{
		
	}

	public void Go()
	{
		foreach (StageEvent se in GlovelSetting.Instance.StageEvents)
		{
			ScheduleHelper.Instance.DelayDo(() =>
			 {
				 DOTween.To(GetBlendX, SetBlendX, se.BlendXTo, se.DurationTime);
				 DOTween.To(GetBlendY, SetBlendY, se.BlendYTo, se.DurationTime);
			 }, se.TriggerTime);
		}

		StageGround.ScrollStart();
	}

	private void Awake()
	{
		CurvedWorldController = GetComponent<CurvedWorld_Controller>();
		Instance = this;
	}
}
