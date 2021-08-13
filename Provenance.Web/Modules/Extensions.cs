using Microsoft.AspNetCore.Mvc;
using Provenance.Common.Responses;

namespace Provenance.Web.Modules
{
    public static class Extensions
    {
        public static IActionResult ToHttpResponse (this Result response)
        {
            var result = new ObjectResult(response);
            result.StatusCode = response.StatusCode;
            return result;
        }
        public static IActionResult ToHttpResponse (this PageableResult response)
        {
            var result = new ObjectResult(response);
            result.StatusCode = response.StatusCode;
            return result;
        }
    }
}
