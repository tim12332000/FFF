using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction : int
{
	IDLE = 0,

	RUNNING = 1,
	JUMP = 2,

	ATTAKC = 3,
	DAMAGED = 4,

	DEAD = 5,
}


public class GamePlayer
{
	public static int LifePoint = 0;
	public static int Shield = 0;

	public static void Initialize()
	{
		LifePoint = 1;
		Shield = 3;

		

	}
}
