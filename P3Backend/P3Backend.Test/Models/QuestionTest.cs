using NUnit.Framework;
using P3Backend.Model.Questions;
using System;

namespace P3Backend.Test.Models {
	public class QuestionTest {
		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void InitializeConstructorYesNoWithInvalidParamsThrowsException(string questionString) {
			Assert.Throws<ArgumentException>(() => { new YesNoQuestion(questionString); });
		}

		[Test]
		[TestCase("test vraag")]
		[TestCase("test")]
		[TestCase("Yes or no, that's the question")]
		public void CheckQuestionStringWasCreatedSuccessfullyPasses(string questionString) {
			YesNoQuestion yesNo = new YesNoQuestion(questionString);
			Assert.AreEqual(yesNo.QuestionString, questionString);
		}
	}
}