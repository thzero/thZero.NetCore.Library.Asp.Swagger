/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Asp.Swagger
Copyright (C) 2016-2018 thZero.com

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
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Authorization;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace thZero.AspNetCore
{
	public sealed class SwaggerAuthorizationOperationFilter : IOperationFilter
	{
		#region Public Methods
		public void Apply(Operation operation, OperationFilterContext context)
		{
            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#operation-filters
            System.Reflection.MethodInfo methodInfo = null;
            context.ApiDescription.TryGetMethodInfo(out methodInfo);
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
				operation.Responses.Add(HttpStatusCode.Unauthorized.ToString(), new Response { Description = Status401 });
		}
		#endregion

		#region Constants
		private const string Status401 = "Unauthorized";
		#endregion
	}
}