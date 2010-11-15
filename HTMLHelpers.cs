using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;

namespace OnlineStatus.Helpers
{
    public static class HTMLHelpers
    {

        public static string GravatarFor(this HtmlHelper html, string id, string email, object htmlAttributes)
        {
            int size = 40;
            
            string imageUrl = "http://www.gravatar.com/avatar.php?";

            if (!string.IsNullOrEmpty(email))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                UTF8Encoding encoder = new UTF8Encoding();
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

                byte[] hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(email));

                StringBuilder sb = new StringBuilder(hashedBytes.Length * 2);
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i].ToString("X2"));
                }

                imageUrl += "gravatar_id=" + sb.ToString().ToLower();
                imageUrl += "&rating=G";
                imageUrl += "&size=" + size.ToString();
            }

            var builder = new TagBuilder("img");

            builder.GenerateId(id);

            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", "Gravatar");
            builder.MergeAttribute("default", "~/Content/Images/default-gravatar.png");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}