﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectApi.Filters

{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;

    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if (context.HttpContext.Request.Method == "GET")
                {
                    var result = new StatusCodeResult((int)HttpStatusCode.BadRequest);
                    context.Result = result;
                }
                else
                {
                    var result = new ContentResult();
                    string content = JsonConvert.SerializeObject(context.ModelState,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    result.Content = content;
                    result.ContentType = "application/json";

                    context.HttpContext.Response.StatusCode = 400;
                    context.Result = result;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}