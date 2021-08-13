using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class ProductHistory : EntityBase
	{

		public Guid ProductId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] Picture { get; set; }
		public string LocationTitle { get; set; }
		public string LocationAddress { get; set; }
		public DateTime Date { get; set; }


		public Product Product { get; set; }


		public static IReadOnlyCollection<string> CanCreate (Guid productId, DateTime date, string title, string content, string locationTitle, string locationAddress)
		{
			var errors = new List<string>();

			if (productId == Guid.Empty)
				errors.Add("product id cannot be null or empty");

			if (string.IsNullOrWhiteSpace(title))
				errors.Add("title is required");

			if (string.IsNullOrWhiteSpace(content))
				errors.Add("content is requred");

			if (date.Date > DateTime.Now.Date)
				errors.Add("product can not be made in future!");

			return errors;

		}
		public static ProductHistory Create (Guid productId, DateTime date, string title, string content, string locationTitle, string locationAddress)
		{
			if (CanCreate(productId, date, title, content, locationTitle, locationAddress).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(ProductHistory)));
			}

			var item = new ProductHistory ()
			{
				ProductId = productId,
				Title = title,
				Content = content,
				LocationTitle = locationTitle,
				LocationAddress = locationAddress,
				Date = date
			};
			return item;
		}



		public static IReadOnlyCollection<string> CanUpdate (Guid productId, DateTime date, string title, string content, string locationTitle, string locationAddress)
		{
			var errors = new List<string>();

			if (productId == Guid.Empty)
				errors.Add("product id cannot be null or empty");

			if (string.IsNullOrWhiteSpace(title))
				errors.Add("title is required");

			if (string.IsNullOrWhiteSpace(content))
				errors.Add("content is requred");

			if (date.Date > DateTime.Now.Date)
				errors.Add("product can not be made in future!");

			return errors;

		}
		public void Update (Guid productId, DateTime date, string title, string content, string locationTitle, string locationAddress)
		{

			if (CanUpdate(productId, date, title, content, locationTitle, locationAddress).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when updating {0}", nameof(ProductHistory)));
			}


			this.ProductId = productId;
			this.Title = title;
			this.Content = content;
			this.LocationTitle = locationTitle;
			this.LocationAddress = locationAddress;
			this.Date = date;
		}



		public void SetPicture (byte[] picture)
		{
			this.Picture = picture;
		}

	}
}
