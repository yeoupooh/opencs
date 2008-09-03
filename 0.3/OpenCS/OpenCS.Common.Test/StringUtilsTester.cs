using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace OpenCS.Common.Test
{
    [TestFixture]
    public class StringUtilsTester
    {
        [Test]
        public void TestStripTags()
        {
            Assert.AreEqual(StringUtils.StripTags("raw text"), "raw text");
            Assert.AreEqual(StringUtils.StripTags("<a><img src=aaa><p>raw text</p></a>"), "raw text");
            Assert.AreEqual(StringUtils.StripTags("<a>111<img src=aaa><p>raw text</p>222</a>"), "111raw text222");
        }

        [Test]
        public void TestGrabString()
        {
            Assert.AreEqual(StringUtils.GrabString("aaa<start>bbb<end>ccc", "<start>", "<end>"), "bbb");
            Assert.AreEqual(StringUtils.GrabString("aaa<start>bbb<end>ccc", "<start>", "<2>"), "aaa<start>bbb<end>ccc");
            Assert.AreEqual(StringUtils.GrabString("aaa<start>bbb<end>ccc", "<1>", "<end>"), "aaa<start>bbb<end>ccc");
            Assert.AreEqual(StringUtils.GrabString("aaa<start>bbb<end>ccc", "<1>", "<2>"), "aaa<start>bbb<end>ccc");
        }
    }
}
