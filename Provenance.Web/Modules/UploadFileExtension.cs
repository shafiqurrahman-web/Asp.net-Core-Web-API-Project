using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Provenance.Web.Modules
{
	public static class UploadFileExtension
	{
		public static async Task<byte[]> ToByteArrayAsync (this IFormFile PictureFile, int height = 350, int width = 350)
		{

			if (PictureFile != null && PictureFile.Length > 0)
			{
				Image img = Image.FromStream(PictureFile.OpenReadStream());
				Stream ms = new MemoryStream(img.Resize(height, width).ToByteArray());

				using (var memoryStream = new MemoryStream())
				{
					await ms.CopyToAsync(memoryStream);
					var result = memoryStream.ToArray();
					return result;
				}
			}

			return new byte[0];

		}
	}
}
