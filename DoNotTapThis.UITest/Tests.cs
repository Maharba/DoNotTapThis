using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace DoNotTapThis.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void RunTestConsole()
        {
            app.Repl();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void ReachTapLimitTest()
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                app.Tap(i == 0 ? "0" : i.ToString("##,###"));
            }
        }

        [Test]
        public void ShowAboutPage()
        {
            app.Tap("OK");
            app.Tap("About");
            var appResult = app.Query("aboutme").First(a => a.Text == "About");
            Assert.IsTrue(appResult != null, "The page shown is not the About Page");
        }
    }
}

