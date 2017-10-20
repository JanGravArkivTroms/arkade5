﻿using System;
using System.IO;
using Arkivverket.Arkade.Core;
using Arkivverket.Arkade.Util;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Test.Core
{
    public class ArkadeProcessingAreaTest
    {
        [Fact]
        public void ProcessingAreaIsEstablished()
        {
            string location = Path.Combine(Environment.CurrentDirectory, "TestData");

            ArkadeProcessingArea.Establish(location);

            ArkadeProcessingArea.Location.FullName.Should().Be(location);
            ArkadeProcessingArea.RootDirectory.FullName.Should().Be(location + "\\Arkade");
            ArkadeProcessingArea.WorkDirectory.FullName.Should().Be(location + "\\Arkade\\work");
            ArkadeProcessingArea.LogsDirectory.FullName.Should().Be(location + "\\Arkade\\logs");
        }

        [Fact (Skip="Fails on build server ...")]
        public void ProcessingAreaIsEstablishedWithInvalidLocation()
        {
            string nonExistingLocation = Path.Combine(Environment.CurrentDirectory, "NonExistingDirectory");

            ArkadeProcessingArea.Establish(nonExistingLocation);

            ProcessingAreaIsSetupWithTemporaryLogsDirectoryOnly();
        }

        [Fact (Skip = "Fails on build server ...")]
        public void ProcessingAreaIsEstablishedWithMissingLocation()
        {
            ArkadeProcessingArea.Establish("");

            ProcessingAreaIsSetupWithTemporaryLogsDirectoryOnly();
        }

        private static void ProcessingAreaIsSetupWithTemporaryLogsDirectoryOnly()
        {
            string temporaryLogsDirectoryPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ArkadeConstants.DirectoryNameTemporaryLogsLocation
            );

            ArkadeProcessingArea.LogsDirectory.FullName.Should().Be(temporaryLogsDirectoryPath);

            ArkadeProcessingArea.Location.Should().BeNull();
            ArkadeProcessingArea.RootDirectory.Should().BeNull();
            ArkadeProcessingArea.WorkDirectory.Should().BeNull();
        }
    }
}