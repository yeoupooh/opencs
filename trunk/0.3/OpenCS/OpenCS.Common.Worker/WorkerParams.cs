using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 인자.
    /// 일꾼 간의 데이터를 주고 받을 때 사용한다.
    /// </summary>
    public class WorkerParams
    {
        private Dictionary<string, object> m_params = new Dictionary<string, object>();

        /// <summary>
        /// 인자를 설정한다.
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">값</param>
        public void SetParam(string key, object value)
        {
            m_params[key] = value;
        }

        /// <summary>
        /// 인자를 가져온다.
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>값</returns>
        public object GetParam(string key)
        {
            if (m_params.ContainsKey(key) == true)
            {
                return m_params[key];
            }

            return null;
        }

        /// <summary>
        /// 키의 존재여부를 가져온다.
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>있으면 true</returns>
        public bool ContainsKey(string key)
        {
            return m_params.ContainsKey(key);
        }

        /// <summary>
        /// 값의 존재여부를 가져온다.
        /// </summary>
        /// <param name="value">값</param>
        /// <returns>있으면 true</returns>
        public bool ContainsValue(object value)
        {
            return m_params.ContainsValue(value);
        }

    }
}
