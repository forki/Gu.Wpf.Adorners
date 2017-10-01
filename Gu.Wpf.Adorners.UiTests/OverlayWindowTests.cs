﻿namespace Gu.Wpf.Adorners.UiTests
{
    using System;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public class OverlayWindowTests
    {
        private const string WindowName = "OverlayWindow";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ImageAssert.OnFail = OnFail.SaveImageToTemp;
        }

        [TestCase("No overlay", ".\\Images\\No overlay.png")]
        [TestCase("Default visibility", ".\\Images\\Default visibility.png")]
        [TestCase("With content template", ".\\Images\\With content template.png")]
        public void Overlay(string name, string imageFileName)
        {
            using (var app = Application.Launch(Info.ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                Wait.For(TimeSpan.FromMilliseconds(200));
                var button = window.FindButton(name);
                ImageAssert.AreEqual(imageFileName, button);
            }
        }

        [Test]
        public void BoundVisibility()
        {
            using (var app = Application.Launch(Info.ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                Wait.For(TimeSpan.FromMilliseconds(200));
                var button = window.FindButton("Bound visibility");
                ImageAssert.AreEqual(".\\Images\\Bound visibility_visible.png", button);

                window.FindButton("IsVisibleButton").Click();
                ImageAssert.AreEqual(".\\Images\\Bound visibility_not_visible.png", button);
            }
        }

        [Test]
        public void WithInheritedContentTemplate()
        {
            using (var app = Application.Launch(Info.ExeFileName, WindowName))
            {
                var window = app.MainWindow;
                Wait.For(TimeSpan.FromMilliseconds(200));
                var groupBox = window.FindGroupBox("Inherits");
                ImageAssert.AreEqual(".\\Images\\WithInheritedContentTemplate.png", groupBox);
            }
        }
    }
}