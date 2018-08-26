using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovelSetting : SingletonMono<GlovelSetting>
{
	public List<StageEvent> StageEvents = new List<StageEvent>();

	public bool IsEnterGame;
	public float GameWinS = 5000f;
	public float OrignalSpd;
	public float AddSpeedofTime = 1 ;
}
