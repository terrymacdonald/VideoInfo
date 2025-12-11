using System;

namespace ADLXWrapper
{
	/// <summary>
	/// Lightweight lifetime wrapper for raw ADLX interface pointers.
	/// </summary>
	public readonly unsafe struct AdlxInterfaceHandle : IDisposable
	{
		private readonly IntPtr _ptr;
		private readonly bool _owns;

		private AdlxInterfaceHandle(IntPtr ptr, bool owns)
		{
			_ptr = ptr;
			_owns = owns;
		}

		public bool IsInvalid => _ptr == IntPtr.Zero;

		public static AdlxInterfaceHandle From(void* ptr, bool addRef = true)
		{
			if (ptr == null) throw new ArgumentNullException(nameof(ptr));
			var intPtr = (IntPtr)ptr;
			if (addRef)
			{
				ADLXHelpers.AddRefInterface(intPtr);
			}
			return new AdlxInterfaceHandle(intPtr, owns: true);
		}

		public T* As<T>() where T : unmanaged => (T*)_ptr;

		public void Dispose()
		{
			if (_owns && _ptr != IntPtr.Zero)
			{
				ADLXHelpers.ReleaseInterface(_ptr);
			}
		}

		public static implicit operator IntPtr(AdlxInterfaceHandle handle) => handle._ptr;
		public static implicit operator IADLXInterface*(AdlxInterfaceHandle handle) => (IADLXInterface*)handle._ptr;
	}
}
