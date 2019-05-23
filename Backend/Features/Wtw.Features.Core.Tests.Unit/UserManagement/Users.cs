using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Wtw.Features.Core.Tests.Unit;

namespace Wtw.Features.Core.Tests.Unit.Blob
{
    [TestClass]
    public class Users : BaseTest
    {
        [TestMethod]
        public async Task GetBlobTest()
        {
            //try
            //{
            //    var blobRequest = new InvoiceQueryRequest
            //    {
            //        TransferObject = new InvoiceRequest()
            //    };
            //    var response = await _mediator.Send(blobRequest);
            //    response.Entity.Url.Length.ShouldBeGreaterThan(5);
            //}
            //catch (Exception e)
            //{
            //    //build fails on vsts
            //}
        }
    }
}
