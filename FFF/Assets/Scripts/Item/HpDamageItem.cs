using UnityEngine;
using System.Collections;

public class HpDamageItem : Item
{
	public int DamageVal;

	public override void Excute(CharRoot cr)
	{
		cr.CharHealth.TakeDamage(DamageVal);
	}
}
