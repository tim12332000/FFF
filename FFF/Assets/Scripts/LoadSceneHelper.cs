using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
	public List<string> SceneNames = new List<string>();

	public static UnityEvent OnLoadAllDone;

	private List<AsyncOperation> _loadScenes = new List<AsyncOperation>();

	private ITask _loadSecneTask;

	public void Load()
	{
		foreach (string sceneName in SceneNames)
		{
			AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			_loadScenes.Add(async);
		}

		_loadSecneTask = ScheduleHelper.Instance.RepeatForver(CheckLoadDone, 0.1f);
	}

	private void CheckLoadDone()
	{
		bool notDone = false;
		foreach (AsyncOperation a in _loadScenes)
		{
			if (!a.isDone)
			{
				notDone = true;
				break;
			}
		}

		if (notDone)
		{
			return;
		}
		else
		{
			Debug.LogFormat("[LoadSceneHelper][OnLoadone]");
			_loadSecneTask.Dispose();
			_loadSecneTask = null;
			OnLoadAllDone.Invoke();
		}
	}

	private void Awake()
	{
		Load();
	}

}
