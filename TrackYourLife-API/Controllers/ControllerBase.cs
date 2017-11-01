using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourLife.API.ViewModels.Common;

namespace TrackYourLife.API.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ResponseWrapper Execute(Action action)
        {
            var response = new ResponseWrapper();
            try
            {
                action();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        protected ResponseWrapper<T> ContentExecute<T>(Func<T> func)
        {
            var response = new ResponseWrapper<T>();
            try
            {
                response.Content = func();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        protected async Task<ResponseWrapper<T>> ContentExecuteAsync<T>(Func<Task<T>> func)
        {
            var response = new ResponseWrapper<T>();
            try
            {
                response.Content = await func();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
