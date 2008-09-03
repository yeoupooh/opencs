using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Action
{
    /// <summary>
    /// 속성값을 가져오거나 설정하는 액션
    /// </summary>
    /// <typeparam name="T">속성값 형태</typeparam>
    public class PropertyAction<T> : IAction
    {
        private T m_property;

        /// <summary>
        /// 속성값ㅇ르 가져오거나 설정한다.
        /// </summary>
        public T Property
        {
            get { return m_property; }
            set { m_property = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public PropertyAction()
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="property">속성값</param>
        public PropertyAction(T property)
        {
            m_property = property;
        }
    }
}
