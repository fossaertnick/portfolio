using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class JwtHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await SecureStorage.GetAsync("jwt");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
