using UnityEngine;
using System.Collections;

public class HpHealItem : Item
{
	public int HealVal;

	public override void Excute(CharRoot cr)
	{
		cr.CharHealth.TakeHeal(HealVal);
	}
}