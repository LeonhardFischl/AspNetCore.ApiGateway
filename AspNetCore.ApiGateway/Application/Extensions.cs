﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace AspNetCore.ApiGateway
{
    public static class Extensions
    {
        public static void AddApiGateway(this IServiceCollection services)
        {
            var apis = new ApiOrchestrator();
            
            services.AddTransient<IApiOrchestrator>(x => apis);
        }

        public static void UseApiGateway(this IApplicationBuilder app, Action<IApiOrchestrator> setApis)
        {
            var serviceProvider = app.ApplicationServices;
            setApis(serviceProvider.GetService<IApiOrchestrator>());
        }

        internal static void AddRequestHeaders (this IHeaderDictionary requestHeaders, HttpRequestHeaders headers)
        {
            foreach (var item in requestHeaders)
            {
                if (!headers.Contains(item.Key))
                    headers.Add(item.Key, item.Value.ToString());
            }
        }
    }
}