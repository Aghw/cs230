using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningCenter.Models;
using System.Collections.Generic;
using LearningCenter.Controllers;
using System.Linq;
using Moq;

namespace LearningCenter.Tests
{
    [TestClass]
    public class ClassesTests
    {
        [TestMethod]
        public void TestMethodWithFakeClass()
        {
            //// Arrange
            //var controller = new HomeController(new FakeClassEnrollmentRepository());

            //// Act
            //var result = controller.ClassList();

            //// Assert
            //var classList = (ClassMaster[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            //Assert.AreEqual(3, classList.Length, "Length is invalid");
        }


        [TestMethod]
        public void TestMethodWithMoq()
        {
            //var mockClassRepository = new Mock<IClassRepository>();
            //mockClassRepository
            //    .SetupGet(t => t.Classes)
            //    .Returns(() =>
            //    {
            //        return new ClassMaster[]{
            //            new ClassMaster{ClassName="Advanced SQL Server", ClassPrice=300.50m},
            //            new ClassMaster{ClassName="C# Basics",  ClassPrice=350.50m}
            //        };
            //    });

            //// Arrange
            //var controller = new HomeController(mockClassRepository.Object);

            //// Act
            //var result = controller.ClassList();

            //// Assert
            //var classesOffered = (ClassMaster[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            //Assert.AreEqual(2, classesOffered.Length, "Length is invalid");

            //Assert.AreEqual(0, classesOffered.Where(c => c.ClassPrice < 300).Count(), "Price is invalid");
            //Assert.AreEqual(1, classesOffered.Where(c => c.ClassPrice < 350).Count(), "Price is invalid");
            //Assert.AreEqual(0, classesOffered.Where(c => c.ClassPrice > 400).Count(), "Price is invalid");
        }
    }
}
