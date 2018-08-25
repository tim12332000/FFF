using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config<T> : ScriptableObject where T : Config<T>
{
	private const string Path = "Configs/";
	private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				string configPath = typeof(T).Name;
				T configInstance = Resources.Load<T>(Path + configPath);
				_instance = configInstance;
			}
			return _instance;
		}
	}
}

public class TestConfig : Config<TestConfig>
{
	public int Data01 = 123456789;
}
