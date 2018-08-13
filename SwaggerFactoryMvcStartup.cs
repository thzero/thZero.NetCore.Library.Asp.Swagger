/* ------------------------------------------------------------------------- *
thZero.NetCore.Library
Copyright (C) 2016-2017 thZero.com

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

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

using thZero.Configuration;

namespace thZero.AspNetCore
{
	public abstract class SwaggerFactoryMvcStartup<TApplicationConfiguration, TApplicationConfigurationDefaults, TApplicationConfigurationEmail> :
		FactoryMvcStartup<TApplicationConfiguration, TApplicationConfigurationDefaults, TApplicationConfigurationEmail>
		where TApplicationConfiguration : Application<TApplicationConfigurationDefaults, TApplicationConfigurationEmail>, new()
		where TApplicationConfigurationDefaults : ApplicationDefaults
		where TApplicationConfigurationEmail : ApplicationEmail
	{
		protected SwaggerFactoryMvcStartup(IHostingEnvironment env, string copyrightDate) : base(env, copyrightDate)
		{
		}

		#region Protected Methods
		protected override void ConfigureInitialize(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
		{
			base.ConfigureInitialize(app, env, loggerFactory, svp);

			app.UseSwagger();
			app.UseSwaggerUI(options => InitializeSwaggerUI(options));
		}

		protected override void ConfigureServicesInitializeMvcPost(IServiceCollection services)
		{
			base.ConfigureServicesInitializeMvcPost(services);

			services.AddSwaggerGen(options => InitializeSwaggerGen(options));
		}

		protected abstract void InitializeSwaggerGen(SwaggerGenOptions options);
		protected abstract void InitializeSwaggerUI(SwaggerUIOptions options);
		#endregion
	}
}