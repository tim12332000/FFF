using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
	public float SpdOff;

	public override void Excute(CharRoot cr)
	{
		StageManager.Instance.ChangeSpeed(SpdOff);
	}
}