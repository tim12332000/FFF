using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class SpeedItem : Item
{
	public float duration;
	public float SpeedChangeTo;

	public override void Excute(CharRoot cr)
	{
	}
}

public class DmamageImmune : Item
{
	public override void Excute(CharRoot cr)
	{
	}
}

public abstract class Item : MonoBehaviour
{
	public UnityEvent OnTrigger;

	public void OnTriggerEnter(Collider other)
	{
		CharRoot cr = other.GetComponent<CharRoot>();
		if (cr == null)
		{
			return;
		}

		Debug.LogFormat("[Item][OnTriggerEnter] OnTrigger");

		Trigger(cr);
	}

	public void Trigger(CharRoot cr)
	{
		OnTrigger.Invoke();
		Excute(cr);
	}

	public abstract void Excute(CharRoot cr);
}
