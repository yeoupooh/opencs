using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OpenCS.Common.Xml
{
    /// <summary>
    /// XmlDocument를 핸들링하기 좋게 하긴 위한 도움 클래스
    /// </summary>
    public class XmlDocumenter
    {
        private XmlDocument m_xmlDoc = new XmlDocument();

        /// <summary>
        /// XmlDocument 형식을 설정하거나 가져온다.
        /// </summary>
        public XmlDocument Document
        {
            get { return m_xmlDoc; }
            set { m_xmlDoc = value; }
        }

        /// <summary>
        /// 최상위 엘리먼트를 가져온다.
        /// </summary>
        public XmlElement RootElement
        {
            get
            {
                if (m_xmlDoc.ChildNodes.Count < 2)
                {
                    return null;
                }
                else
                {
                    return m_xmlDoc.ChildNodes[1] as XmlElement;// 0은 pi, 1은 root
                }
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public XmlDocumenter()
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="encoding">인코딩 문자열</param>
        /// <param name="name">최상위 엘리먼트 이름</param>
        public XmlDocumenter(string encoding, string name)
        {
            // ref: http://msdn2.microsoft.com/en-us/library/system.xml.xmldocument.createprocessinginstruction.aspx
            XmlProcessingInstruction xpi = m_xmlDoc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"" + encoding + "\"");
            m_xmlDoc.AppendChild(xpi);

            XmlElement xe = m_xmlDoc.CreateElement(name);
            m_xmlDoc.AppendChild(xe);
        }

        /// <summary>
        /// 자식 엘리먼트를 추가한다.
        /// </summary>
        /// <param name="parent">부모 엘리먼트</param>
        /// <param name="name">엘리먼트 이름</param>
        /// <returns></returns>
        public XmlElement AppendChild(XmlElement parent, string name)
        {
            XmlElement xe = m_xmlDoc.CreateElement(name);
            parent.AppendChild(xe);

            return xe;
        }

        /// <summary>
        /// 텍스트 값을 가진 자식 엘리먼트를 추가한다.
        /// </summary>
        /// <param name="parent">부모 노드</param>
        /// <param name="name">엘리먼트 이름</param>
        /// <param name="name">텍스트 값</param>
        /// <returns>추가된 엘리먼트</returns>
        public XmlElement AppendTextChild(XmlNode parent, string name, string text)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            XmlElement xe = m_xmlDoc.CreateElement(name);
            XmlText xtText = m_xmlDoc.CreateTextNode(text);
            xe.AppendChild(xtText);
            if (parent != null)
            {
                parent.AppendChild(xe);
            }
            else
            {
                m_xmlDoc.AppendChild(xe);
            }

            return xe;
        }

        /// <summary>
        /// 속성을 추가한다.
        /// </summary>
        /// <param name="element">속성을 추가할 엘리먼트</param>
        /// <param name="name">속성 이름</param>
        /// <param name="value">속성 값</param>
        /// <returns></returns>
        public XmlAttribute AppendAttribute(XmlElement element, string name, string value)
        {
            XmlAttribute xa = m_xmlDoc.CreateAttribute(name);
            XmlText xt = m_xmlDoc.CreateTextNode(value);
            xa.AppendChild(xt);
            element.Attributes.Append(xa);

            return xa;
        }

        /// <summary>
        /// 속성 값을 가져온다.
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <param name="name">속성 이름</param>
        /// <returns></returns>
        public string GetAttribute(XmlNode node, string name)
        {
            if (node != null)
            {
                XmlNode xn = node.Attributes.GetNamedItem(name);
                if (xn != null)
                {
                    return xn.Value;
                }
            }

            return "";
        }

        /// <summary>
        /// Boolean형식의 속성을 가져온다.
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <param name="name">속성 이름</param>
        /// <returns></returns>
        public bool GetBooleanAttribute(XmlNode node, string name)
        {
            XmlNode xn = node.Attributes.GetNamedItem(name);
            if (xn != null)
            {
                return Convert.ToBoolean(xn.Value);
            }

            return false;
        }
    }
}
