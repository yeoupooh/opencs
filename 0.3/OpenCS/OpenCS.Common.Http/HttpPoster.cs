using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace OpenCS.Common.Http
{
    public class HttpPoster : BaseHttpPoster
    {
        public HttpPoster(string url, Encoding encoding, string userAgent)
            : base(url, encoding, userAgent)
        {
        }

        public HttpPoster(string url, Encoding encoding, string userAgent, CookieContainer cc)
            : base(url, encoding, userAgent, cc)
        {
        }

        #region Auto Redirect

        // ref: http://softwaredevscott.spaces.live.com/blog/cns!1A9E939F7373F3B7!553.entry
        private static bool IsRedirectCode(HttpStatusCode code)
        {
            if (code == HttpStatusCode.Redirect ||
                code == HttpStatusCode.Moved ||
                code == HttpStatusCode.MovedPermanently ||
                code == HttpStatusCode.Ambiguous ||
                code == HttpStatusCode.RedirectKeepVerb ||
                code == HttpStatusCode.RedirectMethod ||
                code == HttpStatusCode.TemporaryRedirect ||
                code == HttpStatusCode.Found)
            {
                return true;
            }

            return false;
        }

        // ref: http://softwaredevscott.spaces.live.com/blog/cns!1A9E939F7373F3B7!553.entry
        private static Uri FindNextLocation(Uri currentTarget, string location)
        {
            Uri redirectTarget = null;

            if (location != null)
            {
                // The location header could be absolute or relative.
                // First we try to create an absolute uri out of it.
                // If that fails, we resolve relative to the uri that we see the redirect from.
                bool isUri = Uri.TryCreate(location, UriKind.Absolute, out redirectTarget);
                if (!isUri)
                {
                    isUri = Uri.TryCreate(currentTarget, location, out redirectTarget);
                    if (!isUri)
                    {
                        throw new Exception("Unable to resolve a redirect uri for " + location);
                    }
                }
            }
            return redirectTarget;
        }

        #endregion

        private string MakeContentString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Contents.Count; i++)
            {
                PostContent content = Contents[i];
                if (content.Value != null)
                {
                    sb.Append(content.Name);
                    sb.Append("=");
                    sb.Append(content.Value);
                    if (i < Contents.Count - 1)
                    {
                        sb.Append("&");
                    }
                }
            }

            return sb.ToString();
        }

        public override HttpResult Post()
        {
            HttpResult result = new HttpResult();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            req.CookieContainer = Cc;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] reqDataBytes = Encoding.GetBytes(MakeContentString());
            req.ContentLength = reqDataBytes.Length;
            using (Stream stream = req.GetRequestStream())
            {
                stream.Write(reqDataBytes, 0, reqDataBytes.Length);
            }
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Debug.Print("--------------------");
            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                result.Content = sr.ReadToEnd();
                Debug.Print(result.Content);
            }
            Debug.Print("====================");

            AddCookies(Cc, req.RequestUri, res);

            if (IsRedirectCode(res.StatusCode) == true)
            {
                result.Redirect = FindNextLocation(req.RequestUri, res.Headers.Get("Location"));
            }

            return result;
        }

        public override int GetContentLength()
        {
            return Encoding.GetByteCount(MakeContentString());
        }
    }
}
