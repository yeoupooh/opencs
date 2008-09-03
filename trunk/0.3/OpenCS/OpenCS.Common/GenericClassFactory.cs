using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common
{
    /// <summary>
    /// 일반화된 공장
    /// </summary>
    /// <typeparam name="T">클래스 형태</typeparam>
    public interface GenericClassFactory<T>
    {
        /// <summary>
        /// 클래스를 생성한다.
        /// </summary>
        /// <returns>생성된 클래스</returns>
        T CreateClass();
    }
}
