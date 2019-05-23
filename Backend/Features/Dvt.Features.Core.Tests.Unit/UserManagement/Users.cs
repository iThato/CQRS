using System.Threading.Tasks;
using Dvt.Features.Core.Features.UserManagement.Messages;
using Dvt.Infrastructure.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Dvt.Features.Core.Tests.Unit.UserManagement
{
    [TestClass]
    public class Users : BaseTest
    {
        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
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

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task AddValidUser()
        {
            var userRequest = UserHelper.ValidUser();

            var model = new UserAddCommandRequest
            {
                TransferObject = userRequest
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task AddValidAlreadyExistingUser()
        {
            var userRequest = UserHelper.ValidAlreadyExistingUser();

            var model = new UserAddCommandRequest
            {
                TransferObject = userRequest
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Duplicate);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task AddInvalidAUserEmail()
        {
            var userRequest = UserHelper.InvalidUserEmail();

            var model = new UserAddCommandRequest
            {
                TransferObject = userRequest
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Error);
        }

        //[Fact]
        //public async Task AddInvalidAUserNameAndSurname()
        //{
        //    var userRequest = UserHelper.InvalidUserNameAndSurname();

        //    var model = new UserAddCommandRequest
        //    {
        //        TransferObject = userRequest
        //    };

        //    var result = await _mediator.Send(model);

        //    result.Status.ShouldBe(EnumOperationResult.Error);
        //}

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task AddInvalidAUserIdentityNumber()
        {
            var userRequest = UserHelper.InvalidUserIdentityNumber();

            var model = new UserAddCommandRequest
            {
                TransferObject = userRequest
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Error);
        }


        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task TestAddUserLeapYearIdentityNumber()
        {
            var userRequest = UserHelper.AddUserBornOnLeapYear();

            var model = new UserAddCommandRequest
            {
                TransferObject = userRequest
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);
        }


        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task GetAllUsers()
        {
            var getAllUsers = UserHelper.GetAllUsers();

            var model = new GetAllUsersQueryRequest
            {
                TransferObject = getAllUsers
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);
        }


        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task GetUserByValidId()
        {
            var GetSingleUser = UserHelper.GetUserByValidId();

            var model = new GetUserByIdQueryRequest
            {
                TransferObject = GetSingleUser
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);//I need to revice this, Entity count or length will be 1 so I could use ShouldBeGreaterThan 
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task GetUserByInvalidId()
        {
            var GetSingleUser = UserHelper.GetUserByInvalidId();

            var model = new GetUserByIdQueryRequest
            {
                TransferObject = GetSingleUser
            };

            var result = await _mediator.Send(model);//I need to revice this, Entity count or length will be 0 so I could use ShouldBeGreaterThan 

            result.Status.ShouldBe(EnumOperationResult.None);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task UpdateUser()
        {
            var user = UserHelper.UpdateUser();

            var model = new UserUpdateCommandRequest
            {
                TransferObject = user
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Updated);
        }


        [TestMethod]
        [TestCategory("Unit Test")]
        //[Ignore]
        public async Task RemoveUser()
        {
            var user = UserHelper.RemoveUser();

            var model = new DisableUserCommandRequest
            {
                TransferObject = user
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);
        }
    }
}
