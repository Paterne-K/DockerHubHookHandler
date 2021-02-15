using DockerHubHookHandler.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerHubHookHandler.Refit
{
    public interface ICallbackApi
    {
        [Post("/")]
        Task SendCallback(Callback callback);
    }
}
