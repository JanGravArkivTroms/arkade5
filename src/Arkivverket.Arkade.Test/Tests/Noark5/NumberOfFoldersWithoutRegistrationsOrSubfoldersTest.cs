﻿using System.Linq;
using Arkivverket.Arkade.Core;
using Arkivverket.Arkade.Tests.Noark5;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Test.Tests.Noark5
{
    public class NumberOfFoldersWithoutRegistrationsOrSubfoldersTest
    {
        [Fact]
        public void ResultIsNoFoldersWithoutRegistrationsOrSubfolders()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel",
                        new XmlElementHelper().Add("klassifikasjonssystem",
                            new XmlElementHelper().Add("klasse",
                                new XmlElementHelper()
                                    .Add("mappe",
                                        new XmlElementHelper().Add("registrering",
                                            new XmlElementHelper().Add("someelement", "some value")))
                                    .Add("mappe", // Folder has no registration but has a subfolder
                                        new XmlElementHelper()
                                            .Add("mappe",
                                                new XmlElementHelper().Add("registrering",
                                                    new XmlElementHelper().Add("someelement", "some value"))))))));

            TestRun testRun = helper.RunEventsOnTest(new NumberOfFoldersWithoutRegistrationsOrSubfolders());

            testRun.Results.First().Message.Should().Be("0");
        }

        [Fact]
        public void ResultIsSomeFoldersWithoutRegistrationsOrSubfolders()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("klasse", new XmlElementHelper()
                                .Add("mappe", new XmlElementHelper()
                                    .Add("registrering", new XmlElementHelper()
                                        .Add("someelement", "some value")))
                                .Add("mappe", new XmlElementHelper() // Folder has no registration but has a subfolder
                                    .Add("mappe",
                                        new XmlElementHelper())))))); // Folder has neither registration or subfolder

            TestRun testRun = helper.RunEventsOnTest(new NumberOfFoldersWithoutRegistrationsOrSubfolders());

            testRun.Results.First().Message.Should().Be("1");
        }

        [Fact]
        public void ResultIsTwoFoldersWithoutRegistrationsOrSubfolders()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel",
                        new XmlElementHelper().Add("klassifikasjonssystem",
                            new XmlElementHelper().Add("klasse",
                                new XmlElementHelper()
                                    .Add("mappe", // Folder has neither registration or subfolder
                                        new XmlElementHelper().Add("someelement", "some value"))
                                    .Add("mappe", // Folder has neither registration or subfolder
                                        new XmlElementHelper().Add("someelement", "some value"))))));

            TestRun testRun = helper.RunEventsOnTest(new NumberOfFoldersWithoutRegistrationsOrSubfolders());

            testRun.Results.First().Message.Should().Be("2");
        }

        [Fact]
        public void ResultIsTwoFoldersWithoutRegistrationsOrSubfoldersInOneOfTwoArchiveParts()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_1")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("klasse", new XmlElementHelper()
                                .Add("mappe", // Folder has neither registration or subfolder
                                    new XmlElementHelper())
                                .Add("mappe", // Folder has neither registration or subfolder
                                    new XmlElementHelper()))))
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_2")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("klasse", new XmlElementHelper()
                                .Add("mappe", new XmlElementHelper()
                                    .Add("registrering", new XmlElementHelper()))
                                .Add("mappe", new XmlElementHelper() // Folder has no registration but has a subfolder
                                    .Add("mappe", new XmlElementHelper()
                                        .Add("registrering", new XmlElementHelper())))))));


            TestRun testRun = helper.RunEventsOnTest(new NumberOfFoldersWithoutRegistrationsOrSubfolders());

            testRun.Results.Should().Contain(r => r.Message.Equals("2"));
            testRun.Results.Should().Contain(r => r.Message.Equals("Arkivdel (systemID) someSystemId_1: 2"));
        }
    }
}
