using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.ValueObjects;
using System.Net;

namespace User.Core.Models.User.Image
{
    public class ImageModel : Entity
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public virtual UserModel UserMaster { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
    }
}
