using System.IO;
using System.Linq;
using Arkivverket.Arkade.Core;
using Arkivverket.Arkade.Tests.Noark5;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Test.Tests.Noark5
{
    public class NumberOfFoldersTest
    {
        [Fact]
        public void ShouldFindOneCaseFolderAndOneMeetingfolder()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_1")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper())
                            .Add("mappe", new[] {"xsi:type", "m�temappe"}, new XmlElementHelper()))));

            Archive testArchive = TestUtil.CreateArchiveExtraction(
                Path.Combine("TestData", "Noark5", "FolderControl", "TwoFolders")
            );
            TestRun testRun = helper.RunEventsOnTest(new NumberOfFolders(testArchive));

            testRun.Results.Count.Should().Be(2);

            testRun.Results.Should().Contain(r => r.Message.Equals("Antall saksmapper: 1"));
            testRun.Results.Should().Contain(r => r.Message.Equals("Antall m�temapper: 1"));
        }

        [Fact]
        public void ShouldFindTwoCaseFoldersAndOneMeetingFolderInBothOfTwoArchiveparts()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_1")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper()
                                .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper()))
                            .Add("mappe", new[] {"xsi:type", "m�temappe"}, new XmlElementHelper()
                            )))
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_2")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper()
                                .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper()))
                            .Add("mappe", new[] {"xsi:type", "m�temappe"}, new XmlElementHelper()
                            ))));

            Archive testArchive = TestUtil.CreateArchiveExtraction(
                Path.Combine("TestData", "Noark5", "FolderControl", "SixFolders")
            );
            TestRun testRun = helper.RunEventsOnTest(new NumberOfFolders(testArchive));

            testRun.Results.Count.Should().Be(8);

            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_1 - Antall saksmapper: 2"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_1 - Antall saksmapper p� niv� 1: 1"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_1 - Antall saksmapper p� niv� 2: 1"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_1 - Antall m�temapper: 1"));

            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_2 - Antall saksmapper: 2"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_2 - Antall saksmapper p� niv� 1: 1"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_2 - Antall saksmapper p� niv� 2: 1"));
            testRun.Results.Should().Contain(r =>
                r.Message.Equals("Arkivdel (systemID): someSystemId_2 - Antall m�temapper: 1"));
        }

        [Fact]
        public void DocumentedAndFoundNumberOfFoldersMismatchShouldTriggerWarning()
        {
            XmlElementHelper helper = new XmlElementHelper().Add("arkiv",
                new XmlElementHelper()
                    .Add("arkivdel", new XmlElementHelper()
                        .Add("systemID", "someSystemId_1")
                        .Add("klassifikasjonssystem", new XmlElementHelper()
                            .Add("mappe", new[] {"xsi:type", "saksmappe"}, new XmlElementHelper()))));

            Archive testArchive = TestUtil.CreateArchiveExtraction(
                Path.Combine("TestData", "Noark5", "FolderControl", "TwoFolders")
            );
            TestRun testRun = helper.RunEventsOnTest(new NumberOfFolders(testArchive));

            testRun.Results.Should().Contain(r => r.Message.Equals(
                "Det er angitt at arkivstrukturen skal innholde 2 mapper men 1 ble funnet"
            ));
        }
    }
}
