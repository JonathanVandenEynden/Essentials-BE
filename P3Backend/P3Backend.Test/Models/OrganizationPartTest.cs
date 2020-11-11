using System;
using NUnit.Framework;
using P3Backend.Model.OrganizationParts;

namespace P3Backend.Test.Models
{
    public class OrganizationPartTest
    {
        [Test]
        [TestCase("", OrganizationPartType.TEAM)]
        [TestCase(" ", OrganizationPartType.OFFICE)]
        [TestCase(null, OrganizationPartType.COUNTRY)]
        public void InitializeConstructorWithInvalidNamesThrowsException(string name, OrganizationPartType type)
        {
            Assert.Throws<ArgumentException>(() => { new OrganizationPart(name, type); });
        }
    }
}