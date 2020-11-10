using System;
using NUnit.Framework;
using P3Backend.Model.Users;

namespace P3Backend.Test.Models
{
    public class EmployeeTest
    {
        [Test]
        public void InitializeConstructorWithValidParamsSucceeds()
        {
            new Employee("firstName", "lastName", "email@test");
        }

        [Test]
        [TestCase("", "lastName", "email@test")]
        [TestCase(" ", "lastName", "email@test")]
        [TestCase(null, "lastName", "email@test")]
        [TestCase("firstName", "", "email@test")]
        [TestCase("firstName", " ", "email@test")]
        [TestCase("firstName", null, "email@test")]
        [TestCase("firstName", "lastName", "")]
        [TestCase("firstName", "lastName", " ")]
        [TestCase("firstName", "lastName", null)]
        public void InitializeConstructorWithInvalidParamsThrowsException(string firstName, string lastName, string email)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                new Employee(firstName, lastName, email);
            }));
        }
    }
}