﻿using System.Reflection;
using System.IO;
using Arkivverket.Arkade.Core;
using Arkivverket.Arkade.Metadata;
using Arkivverket.Arkade.Test.Core;
using Arkivverket.Arkade.Util;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Test.Metadata
{
    public class DiasMetsCreatorTest : MetsCreatorTest
    {
        [Fact]
        public void ShouldSaveCreatedDiasMetsFileToDisk()
        {
            string pathToMetsFile = CreateMetsFile(ArchiveMetadata);

            File.Exists(pathToMetsFile).Should().BeTrue();
        }

        [Fact]
        public void DiasMetsFileForAipShouldReferenceEadXmlAndEacCpfXml()
        {
            ArchiveMetadata.PackageType = PackageType.ArchivalInformationPackage;

            string pathToMetsFileForAip = CreateMetsFile(ArchiveMetadata);

            IsReferencingEadXmlAndEacCpfXml(pathToMetsFileForAip).Should().BeTrue();
        }

        [Fact]
        public void DiasMetsFileForSipShouldNotReferenceEadXmlOrEacCpfXml()
        {
            string pathToMetsFileForSip = CreateMetsFile(ArchiveMetadata); // Fake metadata package type default is SIP

            IsReferencingEadXmlOrEacCpfXml(pathToMetsFileForSip).Should().BeFalse();
        }

        private string CreateMetsFile(ArchiveMetadata metadata)
        {
            string workingDirectory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\TestData\\Metadata\\DiasMetsCreator";

            Archive archive = new ArchiveBuilder()
                .WithArchiveType(ArchiveType.Noark5)
                .WithWorkingDirectoryRoot(workingDirectory)
                .Build();

            new DiasMetsCreator().CreateAndSaveFile(archive, metadata);

            string metsFilePath = Path.Combine(workingDirectory, "dias-mets.xml");

            return metsFilePath;
        }

        private bool IsReferencingEadXmlAndEacCpfXml(string pathToMetsFile)
        {
            var metsXmlString = File.ReadAllText(pathToMetsFile);

            return metsXmlString.Contains(ArkadeConstants.EadXmlFileName) &&
                   metsXmlString.Contains(ArkadeConstants.EacCpfXmlFileName);
        }

        private static bool IsReferencingEadXmlOrEacCpfXml(string pathToMetsFile)
        {
            var metsXmlString = File.ReadAllText(pathToMetsFile);

            return metsXmlString.Contains(ArkadeConstants.EadXmlFileName) ||
                   metsXmlString.Contains(ArkadeConstants.EacCpfXmlFileName);
        }
    }
}
