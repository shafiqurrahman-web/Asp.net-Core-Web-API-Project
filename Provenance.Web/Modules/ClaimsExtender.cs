using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Account;
using Provenance.ServiceLayer.DTOs.Provider;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Provenance.Web.Modules
{
	public class ClaimsExtender : IClaimsTransformation
	{
		private readonly IAccountService _accountService;
		private readonly IProviderService _providerService;
		//private readonly IHttpContextAccessor _httpContextAccessor;
		//private readonly IOptions<IdentitySetting> _options;

		public ClaimsExtender (IAccountService accountService, 
			IProviderService providerService)
			//IHttpContextAccessor httpContextAccessor,
			//IOptions<IdentitySetting> options)
		{
			_accountService = accountService;
			_providerService = providerService;
			//_httpContextAccessor = httpContextAccessor;
			//_options = options;
		}

		public Task<ClaimsPrincipal> TransformAsync (ClaimsPrincipal principal)
		{
			var userClaims = (ClaimsIdentity)principal.Identity;

			Guid.TryParse(userClaims.FindFirst(e => e.Type.Equals(CustomClaims.PROVIDER_ID))?.Value, out Guid providerId);
			var error = userClaims.FindFirst(e => e.Type.Equals("error"))?.Value;

			//if provider is exists in the token so the token claims generated before just return
			if (providerId != Guid.Empty)
			{
				return Task.FromResult(principal);
			}


			//if error claim exist return !
			if (!string.IsNullOrEmpty(error))
			{
				return Task.FromResult(principal);
			}


			Guid.TryParse(userClaims.FindFirst(e => e.Type.Equals("sub"))?.Value, out Guid userId);
			var account = _accountService.GetUserById(userId);

			//var client = new HttpClient();
			//client.BaseAddress = new Uri("https://localhost:5001/");
			//var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjA4NTMzNmFmZTY0Yzg2ZWQ3NDU5YzE5YzQ4ZjQzNzI3IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NzkyODM0NTksImV4cCI6MTU3OTI4NzA1OSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo1MDAxL3Jlc291cmNlcyIsInByb3ZlbmFuY2Vfc3lzdGVtIl0sImNsaWVudF9pZCI6InByb3ZlbmFuY2Vfc3lzdGVtIiwic3ViIjoiODI0MDUxNjUtZDFlMC00N2JjLWJhM2YtZjEzNDlhOTIxOGMxIiwiYXV0aF90aW1lIjoxNTc5Mjc2ODk0LCJpZHAiOiJsb2NhbCIsInNjb3BlIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJlbWFpbCIsInJvbGVzIiwicHJvdmVuYW5jZV9zeXN0ZW0iXSwiYW1yIjpbInB3ZCJdfQ.pRXrEls50dWIys0j5xps7u9S-MP5Rcyl5dAuoI1J8rCwylZvxexmgOMcpK56ovvhNvXyz_ZqoI-6lok2zi0lUdBXxtyIDBwN56StkRrk7lTnYMNgU9S3Z-Af0LonJ3aEgk6g35qWOGbguBvoJsUm6JeSdiE8AFIPka6mw_fseIpz8a8iZQXjcDir92RVwh9lVQFfOx-AHA8zP7OCwq2pmSwLQDH2odSGhsbCeFLUkorQSnPomIbnzvd5fas4mHv47cOWfY60b_0_5mmEyFuwLxNnIMPfaPQO_M4cojVOs99F-3jv-uo8fbipBDPf7vThtV4oPhpqG6mv2twAT_W2Yw";
			//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
			//var response = client.GetAsync(new Uri("connect/userinfo")).Result;
			//var resultToken = response.Content.ReadAsStringAsync().Result;
			//JsonConvert.DeserializeObject(resultToken);

			//if user does not exists in database, create it
			if (account != null && account.Data == null)
			{
				var result = _accountService.AddUserFromToken(new AddUserFromTokenDTO
				{
					Id = userId,
					Email = userClaims.FindFirst(e => e.Type == "email")?.Value,
					Firstname = userClaims.FindFirst(e => e.Type == "name")?.Value,
					Lastname = userClaims.FindFirst(e => e.Type == "name")?.Value,
					Role = userClaims.FindAll(e => e.Type == "role").Select(e => e.Value).FirstOrDefault()
				});
				var processSucceed = ((bool)result.Data);
				if (!processSucceed)
				{
					var appIdentity = new ClaimsIdentity(userClaims);
					principal.AddIdentity(appIdentity);
					throw new AggregateException(result.Errors.Select(e => new Exception(e)));
					return Task.FromResult(principal);
				}
			}


			var provider = _providerService.GetByUserId(userId);


			//if user is a provider so add detail to claims
			if (provider != null && !provider.HasError)
			{
				var data = (GetProviderDTO) provider.Data;
				userClaims.AddClaim(new Claim(CustomClaims.PROVIDER_NAME, data.Name, ClaimValueTypes.String, "https://www.wtxhub.com"));
				userClaims.AddClaim(new Claim(CustomClaims.PROVIDER_ID, data.Id.ToString(), ClaimValueTypes.String, "https://www.wtxhub.com"));
				userClaims.AddClaim(new Claim(CustomClaims.IS_PROVIDER, "true", ClaimValueTypes.String, "https://www.wtxhub.com"));
				var appIdentity = new ClaimsIdentity(userClaims);
				principal.AddIdentity(appIdentity);
			}


			//return the principal
			return Task.FromResult(principal);

		}



	}

}
