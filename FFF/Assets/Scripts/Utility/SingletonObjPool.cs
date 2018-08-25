using System.Collections;
using UnityEngine;

namespace Gameplay.Utility
{
	public interface IRelease
	{
		void Release();
	}

	public abstract class SingletonObjPool<TComponent, TSingleObjPool> : IRelease
		where TComponent : Component
		where TSingleObjPool : class, new()
	{
		private static TSingleObjPool _instance;
		protected ObjectPool<TComponent> _objectPool;
		private Vector3 Farway = new Vector3(99999, 99999, 99999);

		public static TSingleObjPool Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new TSingleObjPool();
				}

				return _instance;
			}
		}

		protected SingletonObjPool()
		{
			_objectPool = InitObjPool();
		}

		public IEnumerator Preload(int num, int numOfFrame = 1)
		{
			yield return _objectPool.AsyncNewBuffer(num, numOfFrame);
		}

		protected abstract ObjectPool<TComponent> InitObjPool();

		public abstract TComponent Spawn();

		public abstract void Despawn(TComponent component);

		public void Release()
		{
			_instance = null;
		}
	}
}