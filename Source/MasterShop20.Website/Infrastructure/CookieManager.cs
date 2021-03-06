﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MasterShop20.Website.Infrastructure
{
    public class CookieManager : Controller
    {

        public void RemoveCookies()
        {
            if (CheckArticlesCookie())
                Response.Cookies["articles"].Expires = DateTime.Now.AddDays(-1);

            if (CheckUserCookie())
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
        }


        public bool CheckUserCookie()
        {
            if (Request != null && Request.Cookies["user"] != null)
                return true;

            return false;
        }

        public void SetUserCookie(int idUser)
        {
            if (Response != null)
                Response.Cookies.Add(new HttpCookie("user") { Value = idUser.ToString() });
        }

        public int LoadUserCookie()
        {
            var id = 0;

            if (!CheckUserCookie())
                return id;

            int.TryParse(Request.Cookies["user"].Value, out id);
            return id;
        }



        public bool CheckArticlesCookie()
        {
            if (Request != null && Request.Cookies["articles"] != null)
                return true;

            return false;
        }

        public void SetArticleIdInCookie(int idArticle)
        {
            if (Response == null)
                return;

            var list = new List<string>();

            if (Response.Cookies["articles"] != null) // wenn != null, dann gibt es breits eine Liste
                list = JsonConvert.DeserializeObject<List<string>>(Response.Cookies["articles"].Value);

            list.Add(idArticle.ToString());

            var listString = JsonConvert.SerializeObject(list);
            Response.Cookies.Add(new HttpCookie("articles") { Value = listString });
        }

        public List<string> LoadArticlesIds()
        {
            if (!CheckArticlesCookie())
                return null;

            return JsonConvert.DeserializeObject<List<string>>(Request.Cookies["articles"].Value);
        }

        public void RemoveArticleIdInCookie(int articleId)
        {
            var stringList = LoadArticlesIds();
            stringList.Remove(articleId.ToString());
        }
    }
}