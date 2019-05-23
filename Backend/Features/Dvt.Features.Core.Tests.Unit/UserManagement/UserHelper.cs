using Dvt.Features.Messages.Request;
using System;

namespace Dvt.Features.Core.Tests.Unit.UserManagement
{
    public class UserHelper
    {
        public static AddUserRequest ValidUser()
        {
            var random = new Random();
            return new AddUserRequest
            {

                Email = Guid.NewGuid().ToString() + "@jhb.dvt.co.za",
                FirstName = "Shalom",
                LastName = "Marimi",
                ContactNumber = "071125336" + random.Next(9),
                KnownAs = "SK",
                SystemProfileId = 1

            };
        }

        public static AddUserRequest ValidAlreadyExistingUser()
        {
            return new AddUserRequest
            {
                Email = "marimi@jhb.dvt.co.za",
                FirstName = "James",
                LastName = "Bond",
                ContactNumber = "0794230621"

            };
        }

        public static AddUserRequest InvalidUserEmail()
        {
            return new AddUserRequest
            {
                Email = "brian@jhb.dvt.",
                FirstName = "Brian",
                LastName = "Adams",
                ContactNumber = "0724568746"

            };
        }

        public static AddUserRequest InvalidUserNameAndLastName()
        {
            return new AddUserRequest
            {

                Email = "brian@jhb.dvt.za",
                FirstName = "Brian002",
                LastName = "Adams3",
                ContactNumber = "0724568746"

            };
        }

        public static AddUserRequest InvalidUserIdentityNumber()
        {
            return new AddUserRequest
            {

                Email = "brians@jhb.dvt.za",
                FirstName = "Brian",
                LastName = "Adams",
                ContactNumber = "0724568746"
            };
        }

        public static AddUserRequest AddUserBornOnLeapYear()
        {
            return new AddUserRequest
            {

                Email = "brians@jhb.dvt.za",
                FirstName = "Brian",
                LastName = "Adams",
                ContactNumber = "0724568746"
            };
        }

        public static UserUpdateRequest UpdateUser()
        {
            return new UserUpdateRequest
            {
                //Id = 78,
                //Email = "newupdatedemail@jhb.dvt.za",
                //FirstName = "Updated Name",
                //LastName = "Updated LastName",
                //ContactNumber = "0724568746",
                //CompanyId = 25,
                //IdNumber = "9202299696180"
            };
        }

        public static GetAllUsersRequest GetAllUsers()
        {
            return new GetAllUsersRequest { };
        }

        public static GetUserByIdRequest GetUserByInvalidId()
        {
            return new GetUserByIdRequest
            {
                Id = 12
            };
        }

        public static GetUserByIdRequest GetUserByValidId()
        {
            return new GetUserByIdRequest
            {
                Id = 85
            };
        }

        public static DisableUserRequest RemoveUser()
        {
            return new DisableUserRequest
            {
                //  Id = null
            };
        }
    }


}
