﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MHA.UI.CustomFilter
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);
            var SessionControl = context.HttpContext.Session["AppUserEmail"];
            if (SessionControl == null)
            {
                context.Result = new RedirectToRouteResult
                    (new RouteValueDictionary
                        {
                            { "controller", "Account" }
                           ,{ "action", "login" }
                        }
                    );
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}