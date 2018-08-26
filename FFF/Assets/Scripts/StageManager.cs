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
	public float GetSpeed()
	{
		return StageGround.ScrollSpeed;
	}

	private float _nowS;
	private float _newSpd;
	private bool _gameOver;

	public bool _stopUpdateSpd = false;

	public void ChangeSpeed(float spdOff)
	{
		float newSpd = GetSpeed() + spdOff;

		if (newSpd > -5)
			newSpd = -5;

		if (newSpd < -200)
			newSpd = -200;

		SetSpeed(newSpd);
	}

	private void OnGameReStart()
	{
		_stopUpdateSpd = false;
		_gameOver = false;
		SetSpeed(GlovelSetting.Instance.OrignalSpd);
	}

	public void OnGameWin()
	{
		_stopUpdateSpd = true;
		SetSpeed(0);

		Debug.LogError("gamewin");

		_gameOver = true;
	}

	public void OnGameOver()
	{
		_stopUpdateSpd = true;
		SetSpeed(0);

		Debug.LogError("gameover");

		_gameOver = true;
	}

	public void Update()
	{
		if (_stopUpdateSpd)
			return;

		float newSpd = GetSpeed() + Time.deltaTime * -GlovelSetting.Instance.AddSpeedofTime;
		SetSpeed(newSpd);

		_newSpd = newSpd;

		GameUIValue.Instance.Speed = Mathf.Floor(-newSpd);
	}

	public void Go()
	{
		Debug.LogError("go");

		_nowS = 0;
		_newSpd = 0;

		GlovelSetting.Instance.IsEnterGame = true;
		SetSpeed(GlovelSetting.Instance.OrignalSpd);

		// setting x 3
		GlovelSetting.Instance.StageEvents.AddRange(GlovelSetting.Instance.StageEvents);
		GlovelSetting.Instance.StageEvents.AddRange(GlovelSetting.Instance.StageEvents);
		GlovelSetting.Instance.StageEvents.AddRange(GlovelSetting.Instance.StageEvents);

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

		GlobelEvents.Instance.GameWin.AddListener(OnGameWin);
		GlobelEvents.Instance.GameOver.AddListener(OnGameOver);
		GlobelEvents.Instance.GameReStart.AddListener(OnGameReStart);

		ScheduleHelper.Instance.RepeatForver(RefreshS, 1f);
	}

	private void RefreshS()
	{
		if (_gameOver)
			return;

		_nowS += _newSpd;
		GameUIValue.Instance.SetProgress(-_nowS, GlovelSetting.Instance.GameWinS);
		GameUIValue.Instance.Far = Mathf.Floor(-_nowS);

		if (-_nowS > GlovelSetting.Instance.GameWinS)
		{
			GlobelEvents.Instance.GameWin.Invoke();
		}
	}
}
