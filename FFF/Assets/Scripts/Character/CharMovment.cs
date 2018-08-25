using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CharMovment : MonoBehaviour
{
	public float MaxMove = 3f;

	public float Speed = 1f;
	public float JumpPower = 1f;
	public KeyCode JumpKey = KeyCode.UpArrow;
	public KeyCode LeftKey = KeyCode.LeftArrow;
	public KeyCode RightKey = KeyCode.RightArrow;
	public float Gav = -9.8f;

	private Tween _tween;

	bool _isInJump;
	bool _isMoveLeftOrRight;
	private bool _controling;
	private float _jumpStartY;
	private float _timer;
	private bool _inJump;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(JumpKey))
		{
			_inJump = true;
			_timer = 0f;
			_jumpStartY = transform.position.y;
		}

		if (_inJump)
		{
			_timer += Time.deltaTime;
			double s = _jumpStartY + (JumpPower * _timer) + (1f / 2f) * Gav * Math.Pow(_timer, 2);
			transform.position = new Vector3(transform.position.x, (float)s, transform.position.z);
		}

		if (_inJump && transform.position.y < 0f)
		{
			transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
			_inJump = false;
		}

		if (Input.GetKey(LeftKey))
		{
			Left();
		}

		if (Input.GetKey(RightKey))
		{
			Right();
		}
	}

	public void Left()
	{
		float movePos = transform.position.x - Time.deltaTime * Speed;
		if (movePos < -MaxMove)
		{
			movePos = -MaxMove;
		}

		transform.position = new Vector3(movePos, transform.position.y, transform.position.z);
	}

	private void Right()
	{
		float movePos = transform.position.x + Time.deltaTime * Speed;
		if (movePos > MaxMove)
		{
			movePos = MaxMove;
		}

		transform.position = new Vector3(movePos, transform.position.y, transform.position.z);
	}

	public void RightStop()
	{

	}

	public void LeftStop()
	{

	}

	private void Awake()
	{
	}
}



