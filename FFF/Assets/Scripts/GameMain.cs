using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState : int
{
	NONE = 0,

	TITLE = 1,
	PLAYING = 2,
	PAUSE = 3,
	GAMEOVER = 4,
	ENDING = 5,

	MAX = 9
}

public class GameMain
{
	public static GameState GameState;

	public static void Initialize()
	{
		GameState = GameState.TITLE;
	}

	public static void BackToTitle()
	{
		GameState = GameState.TITLE;
	}

	public static void GameStart()
	{
		GamePlayer.Initialize();
		GameState = GameState.PLAYING;
	}

	public static void GameOver()
	{
		GameState = GameState.GAMEOVER;
	}

	public static bool IsGameOver()
	{
		return GameState == GameState.GAMEOVER;
	}

	public static void PlayerRevive()
	{
		GamePlayer.Initialize();
		GameState = GameState.PLAYING;
	}

	public static void PlayerDamage(int value)
	{
		if (GamePlayer.Shield > 0) {
			GamePlayer.Shield -= value;
			return;
		}

		GamePlayer.LifePoint -= value;

		if (GamePlayer.LifePoint <= 0) {
			GameOver();
		}

	}

}
