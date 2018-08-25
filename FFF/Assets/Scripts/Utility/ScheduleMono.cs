using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Gameplay.Utility;

public class ScheduleMono : MonoBehaviour
{
	public UnityEvent OnSchedule = new UnityEvent();

	public float Duation;
	public int Count;
	public bool Repeat = false;

	private ITask _task;

	public void OnEnable()
	{
		if (Repeat)
		{
			_task = ScheduleHelper.Instance.RepeatForver(OnSchedule.Invoke, Duation);
		}
		else
		{
			_task = ScheduleHelper.Instance.NewScheule(OnSchedule.Invoke, Duation, Count);
		}
	}

	public void OnDisable()
	{
		if (_task != null)
		{
			_task.Dispose();
			_task = null;
		}
	}
}
