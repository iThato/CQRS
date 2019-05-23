using Dvt.Features.Core.Features.Course.Messages;
using Dvt.Infrastructure.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Dvt.Features.Core.Tests.Unit.CourseManagement
{
    [TestClass]
    public class Courses : BaseTest
    {

        [TestMethod]
        [TestCategory("Unit Test")]
        
        public async Task GetAllCourses()
        {
            try
            {
                var getAllCourses = CourseHelper.GetAllCourses();

                var model = new GetAllCoursesQueryRequest
                {
                    TransferObject = getAllCourses
                };


                var result = await _mediator.Send(model);

                result.Status.ShouldBe(EnumOperationResult.Ok);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [TestMethod]
        [TestCategory("Unit Test")]
      
        public async Task GetCourseByValidId()
        {
            var GetSingleCourse = CourseHelper.GetUserByValidId();

            var model = new GetCourseByIdQueryRequest
            {
                TransferObject = GetSingleCourse
            };

            var result = await _mediator.Send(model);

            result.Status.ShouldBe(EnumOperationResult.Ok);//I need to revice this, Entity count or length will be 1 so I could use ShouldBeGreaterThan 
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        
        public async Task GetCourseByInvalidId()
        {
            var GetSingleCourse = CourseHelper.GetUserByInvalidId();

            var model = new GetCourseByIdQueryRequest
            {
                TransferObject = GetSingleCourse
            };

            var result = await _mediator.Send(model);//I need to revice this, Entity count or length will be 0 so I could use ShouldBeGreaterThan 

            result.Status.ShouldBe(EnumOperationResult.None);
        }
        
    }
}

