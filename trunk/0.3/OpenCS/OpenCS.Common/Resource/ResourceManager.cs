using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Logging;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 기본 리소스 관리자
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        /// <summary>
        /// 리소스 제공자
        /// </summary>
        private IResourceProvider mRp;

        /// <summary>
        /// 리소스 변경자
        /// </summary>
        private IResourceChanger mRc;

        /// <summary>
        /// 생성자
        /// Singleton을 쓰려면 인자 없는 생성자가 필요하다.
        /// </summary>
        public ResourceManager()
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="rc">리소스 변경자</param>
        public ResourceManager(IResourceProvider rp, IResourceChanger rc)
        {
            mRp = rp;
            mRc = rc;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        public ResourceManager(IResourceProvider rp, ILogger logger)
        {
            mRp = rp;
            mRc = new ResourceChanger(rp, logger);
        }

        #region IResourceManager 멤버

        /// <summary>
        /// 리소스 제공자를 가져온다.
        /// </summary>
        public IResourceProvider ResourceProvider
        {
            get
            {
                return mRp;
            }
        }

        /// <summary>
        /// 리소스 변경자를 가져온다.
        /// </summary>
        public IResourceChanger ResourceChanger
        {
            get
            {
                return mRc;
            }
        }

        /// <summary>
        /// 리소스 관리자를 초기화한다.
        /// 리소스 관리자를 사용하기 전에 반드시 초기화해야 한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        public void Init(IResourceProvider rp, ILogger logger)
        {
            mRp = rp;
            mRc = new ResourceChanger(rp, logger);
        }

        #endregion
    }
}
