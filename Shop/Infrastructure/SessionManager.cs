﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace Shop.Infrastructure
{

    public class SessionManager : ISessionManager
    {
        private HttpSessionState session;

        public SessionManager()
        {
          session = HttpContext.Current.Session;
        }

        public T Get<T>(string key)
        {
            return (T)session[key];
        }

        public void Set<T>(string name, T value)
        {
            session[name] = value;
        }
        public void Abandon()
        {
            session.Abandon();
        }

        public T TryGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException)
            {

                return default(T);
            }
        }
    }
}