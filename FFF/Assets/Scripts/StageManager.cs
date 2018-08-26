using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

	public static StageManager Instance;

	public GroundScrollController StageGround;

	public void Go()
	{
		StageGround.ScrollStart();
	}

	private void Awake()
	{
		Instance = this;
	}
}
