using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class CharMovment : MonoBehaviour
{
	public UnityEvent OnJumpEvent;

	public float RotaDuration = 0.3f;
	public float RotaionMax = 90f;
	public float MaxMove = 3f;

	public float Speed = 1f;
	public float JumpPower = 1f;
	public KeyCode JumpKey = KeyCode.UpArrow;
	public KeyCode LeftKey = KeyCode.LeftArrow;
	public KeyCode RightKey = KeyCode.RightArrow;
	public float Gav = -9.8f;

	private Tween _tween;

	bool _isMoveLeftOrRight;
	private bool _controling;
	private float _jumpStartY;
	private float _timer;
	private bool _inJump;

	private Quaternion _leftQ;
	private Quaternion _rightQ;
	private Tween _rotaionTw;
	private float _ogY;

	[SerializeField]
	private GameObject _running;
	[SerializeField]
	private GameObject _jumping;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(JumpKey))
		{
			_inJump = true;
			_timer = 0f;
			_jumpStartY = transform.position.y;
			OnJumpEvent.Invoke();
			ChangeToJump();
		}

		if (_inJump)
		{
			_timer += Time.deltaTime;
			double s = _jumpStartY + (JumpPower * _timer) + (1f / 2f) * Gav * Math.Pow(_timer, 2);
			transform.position = new Vector3(transform.position.x, (float)s, transform.position.z);
		}

		if (_inJump && transform.position.y < _ogY + 0.1f)
		{
			transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
			_inJump = false;
			ChangeToRunning();
		}

		if (Input.GetKey(LeftKey))
		{
			Left();
		}

		if (Input.GetKey(RightKey))
		{
			Right();
		}

		if (Input.GetKeyUp(LeftKey) || Input.GetKeyUp(RightKey))
		{
			ClearTweenAndRebackRotaion();
		}
	}

	public void Left()
	{
		if (_rotaionTw == null)
		{
			_rotaionTw = gameObject.transform.DORotate(new Vector3(0, 0, RotaionMax), RotaDuration)
				.OnComplete(() =>
				{
					ClearTweenAndRebackRotaion();
				});
		}

		float movePos = transform.position.x - Time.deltaTime * Speed;
		if (movePos < -MaxMove)
		{
			movePos = -MaxMove;
		}

		transform.position = new Vector3(movePos, transform.position.y, transform.position.z);
	}

	private void Right()
	{
		if (_rotaionTw == null)
		{
			_rotaionTw = gameObject.transform.DORotate(new Vector3(0, 0, -RotaionMax), RotaDuration)
				.OnComplete(() =>
				{
					ClearTweenAndRebackRotaion();
				});
		}

		float movePos = transform.position.x + Time.deltaTime * Speed;
		if (movePos > MaxMove)
		{
			movePos = MaxMove;
		}

		transform.position = new Vector3(movePos, transform.position.y, transform.position.z);
	}

	private void ClearTweenAndRebackRotaion()
	{
		_rotaionTw.Kill();
		_rotaionTw = null;
		gameObject.transform.DORotate(Vector3.zero, 0.1f);
	}

	public void RightStop()
	{

	}

	public void LeftStop()
	{

	}

	private void ChangeToJump()
	{
		if (_jumping == null)
			return;

		if (_running == null)
			return;

		_jumping.SetActive(true);
		_running.SetActive(false);
	}

	private void ChangeToRunning()
	{
		if (_jumping == null)
			return;

		if (_running == null)
			return;

		_jumping.SetActive(false);
		_running.SetActive(true);
	}

	private void Awake()
	{
		_leftQ = Quaternion.Euler(new Vector3(0f, 0f, RotaionMax));
		_rightQ = Quaternion.Euler(new Vector3(0f, 0f, -RotaionMax));
		_ogY = transform.position.y;
	}
}



