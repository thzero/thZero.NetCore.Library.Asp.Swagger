/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Asp.Swagger
Copyright (C) 2016-2021 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace thZero.AspNetCore
{
    public abstract class SwaggerStartupExtension : BaseStartupExtension
    {
        #region Public Methods
        public override void ConfigureInitializePost(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
        {
            base.ConfigureInitializePost(app, env, loggerFactory, svp);

            app.UseSwagger();
            app.UseSwaggerUI(options => ConfigureInitializeSwaggerUI(options));
        }

        public override void ConfigureServicesInitializeMvcBuilderPre(IMvcCoreBuilder builder)
        {
            base.ConfigureServicesInitializeMvcBuilderPre(builder);

            builder.AddApiExplorer();
        }

        public override void ConfigureServicesInitializeMvcPost(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            base.ConfigureServicesInitializeMvcPost(services, env, configuration);

            services.AddSwaggerGen(options => ConfigureServicesInitializeSwaggerGen(options));
        }
        #endregion

        #region Protected Methods
        protected abstract void ConfigureServicesInitializeSwaggerGen(SwaggerGenOptions options);
        protected abstract void ConfigureInitializeSwaggerUI(SwaggerUIOptions options);

        protected void SwaggerEndpoint(SwaggerUIOptions options, string name, string type, string root = "swagger", string document = "swagger.json")
        {
            options.SwaggerEndpoint(string.Concat(Seperator, root, Seperator, type, Seperator, document), name);
        }
        #endregion

        #region Constants
        private const string Seperator = "/";
        #endregion
    }
}