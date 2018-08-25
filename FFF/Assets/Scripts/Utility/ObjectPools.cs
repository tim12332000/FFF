using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Utility;

public abstract class ObjectPools<T, TObjectPools> where T : Component where TObjectPools : new()
{
	private Dictionary<string, ObjectPool<T>> _dict = new Dictionary<string, ObjectPool<T>>();

	private static TObjectPools _instance;

	public static TObjectPools Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new TObjectPools();
			}

			return _instance;
		}
	}

	public T Spawn(string name)
	{
		T t = Load(name);
		return Spawn(t);
	}

	public T Spawn(T obj)
	{
		string name = obj.name;
		if (_dict.ContainsKey(name) == false)
		{
			GameObject parent = new GameObject();
			parent.name = "[pool]" + name;
			_dict[name] = new ObjectPool<T>(obj, parent.transform);
		}

		T result = _dict[name].Spawn();
		result.name = obj.name;
		return result;
	}

	public void Despawn(T t)
	{
		if (_dict.ContainsKey(t.name) == false)
		{
			Debug.LogErrorFormat("[ObjectPools][Despwan] not Found {0}", t.name);
			return;
		}

		_dict[t.name].Despwan(t);
	}

	protected T Load(string name)
	{
		GameObject g = Resources.Load<GameObject>(GetPath() + name);
		return g.GetComponent<T>();
	}

	protected abstract string GetPath();
}
