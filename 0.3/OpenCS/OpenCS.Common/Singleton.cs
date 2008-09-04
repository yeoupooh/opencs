using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;

namespace OpenCS.Common
{
    /// <summary>
    /// A Generic Singleton Pattern in C#
    /// ref: http://sanity-free.org/132/generic_singleton_pattern_in_csharp.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Singleton<T> where T : new()
    {
        static Mutex mutex = new Mutex();
        static T instance;

        /// <summary>
        /// Get Singleton Class Instance
        /// </summary>
        public static T Instance
        {
            get
            {
                mutex.WaitOne();
                if (instance == null)
                {
                    instance = new T();
                }
                mutex.ReleaseMutex();
                return instance;
            }
        }
    }
}
