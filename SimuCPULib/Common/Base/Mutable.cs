using System;
using SimuCPULib.Common.Simulator;

namespace SimuCPULib.Common.Base
{
	public class MutableValueUpdatedEventArgs<T> : EventArgs
	{
		public T Status { get; set; }
	}

	public class MutableStateUpdatedEventArgs : EventArgs
	{
		public bool Active { get; set; }
	}

	/// <summary>
	/// 状态更新
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Mutable<T> : Markable, ISimulate
		where T : Status, new()
	{
		protected event EventHandler<MutableValueUpdatedEventArgs<T>> OnValueUpdated;
		protected event EventHandler<MutableStateUpdatedEventArgs> OnStateUpdated;

		/// <summary>
		/// 原状态
		/// </summary>
		private T _local = new T();

		public T Local
		{
			get { return _local; }
			set { _local = value; }
		}

		/// <summary>
		/// 新状态
		/// </summary>
		private T _next = new T();

		public T Next
		{
			get { return _next; }
			set { _next = value; }
		}

		/// <summary>
		/// 激活（去除Zombie/Freeze状态）
		/// </summary>
		private bool _active;

		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual void Update()
		{
			if (!_local.Equals(_next))
			{
                _active = true;
                //复制状态
                _local.CopyFrom(_next);
			    //触发状态更改事件
			    OnValueUpdated?.Invoke(this, new MutableValueUpdatedEventArgs<T> { Status = _local });
			}
			else
			{
				_active = false;
			    //触发状态激活事件
			    OnStateUpdated?.Invoke(this, new MutableStateUpdatedEventArgs { Active = false });
			}
		}

		public virtual void Activate()
		{
			_active = true;
		    OnStateUpdated?.Invoke(this, new MutableStateUpdatedEventArgs { Active = true });
		}

		public virtual void Advance()
		{

		}
	}
}
