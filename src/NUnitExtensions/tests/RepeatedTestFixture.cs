using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.TestData.RepeatedTestFixture;

namespace NUnit.Core.Extensions.Tests
{
	[TestFixture, Ignore("Update in Progress")]
	public class RepeatedTestFixture
	{
		private MethodInfo successMethod;
		private MethodInfo failOnFirstMethod;
		private MethodInfo failOnThirdMethod;

		[SetUp]
		public void SetUp()
		{
			Type testType = typeof(RepeatSuccessFixture);
			successMethod = testType.GetMethod ("RepeatSuccess");
			testType = typeof(RepeatFailOnFirstFixture);
			failOnFirstMethod = testType.GetMethod("RepeatFailOnFirst");
			testType = typeof(RepeatFailOnThirdFixture);
			failOnThirdMethod = testType.GetMethod("RepeatFailOnThird");
		}

		private TestResult RunTestOnFixture( object fixture )
		{
			TestSuite suite = TestFixtureBuilder.Make( fixture );
			Assert.AreEqual( 1, suite.Tests.Count, "Test case count" );
			Assert.AreEqual( "NUnit.Core.Extensions.RepeatedTestCase", suite.Tests[0].GetType().FullName );
			return suite.Run( NullListener.NULL );
		}

		[Test]
		public void RepeatedTestIsBuiltCorrectly()
		{
			TestSuite suite = TestFixtureBuilder.Make( typeof( RepeatSuccessFixture ) );
			Assert.IsNotNull( suite, "Unable to build suite" );
			Assert.AreEqual( 1, suite.Tests.Count );
			Assert.AreEqual( "RepeatedTestCase", suite.Tests[0].GetType().Name );
			TestCase repeatedTestCase = suite.Tests[0] as TestCase;
			Assert.IsNotNull( repeatedTestCase, "Test case is not a RepeatedTestCase" );
			Assert.AreSame( suite, repeatedTestCase.Parent );
//			Assert.AreEqual( "NUnit.TestData.RepeatedTestFixture.RepeatSuccessFixture", repeatedTestCase.FixtureType.FullName );
		}

		[Test]
		public void RepeatSuccess()
		{
			Assert.IsNotNull (successMethod);
			RepeatSuccessFixture fixture = new RepeatSuccessFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(3, fixture.SetupCount);
			Assert.AreEqual(3, fixture.TeardownCount);
			Assert.AreEqual(3, fixture.Count);
		}

		[Test]
		public void RepeatFailOnFirst()
		{
			Assert.IsNotNull (failOnFirstMethod);
			RepeatFailOnFirstFixture fixture = new RepeatFailOnFirstFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(1, fixture.SetupCount);
			Assert.AreEqual(1, fixture.TeardownCount);
			Assert.AreEqual(1, fixture.Count);
		}

		[Test]
		public void RepeatFailOnThird()
		{
			Assert.IsNotNull (failOnThirdMethod);
			RepeatFailOnThirdFixture fixture = new RepeatFailOnThirdFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(3, fixture.SetupCount);
			Assert.AreEqual(3, fixture.TeardownCount);
			Assert.AreEqual(3, fixture.Count);
		}

		[Test]
		public void IgnoreWorksWithRepeatedTest()
		{
			RepeatedTestWithIgnore fixture = new RepeatedTestWithIgnore();
			TestResult result = RunTestOnFixture( fixture );

			Assert.AreEqual( 0, fixture.SetupCount );
			Assert.AreEqual( 0, fixture.TeardownCount );
			Assert.AreEqual( 0, fixture.Count );
		}
	}
}
