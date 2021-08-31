using Microsoft.AspNetCore.Mvc;
using Modalmais.Core.Controller.Shared;
using System;

namespace Modalmais.Core.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomResponseAttribute : ProducesResponseTypeAttribute
    {
        public CustomResponseAttribute(int statusCode) : base(typeof(CustomResult), statusCode) { }
    }
}
