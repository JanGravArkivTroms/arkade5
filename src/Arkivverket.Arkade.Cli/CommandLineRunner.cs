﻿using System;
using System.IO;
using System.Reflection;
using Arkivverket.Arkade.Core;
using Newtonsoft.Json;
using Serilog;

namespace Arkivverket.Arkade.Cli
{
    internal class CommandLineRunner
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);

        public void Run(CommandLineOptions options)
        {
            try
            {
                var arkade = new Core.Arkade();

                var fileInfo = new FileInfo(options.Archive);
                Log.Information($"Processing archive: {fileInfo.FullName}");

                if (!Enum.TryParse(options.ArchiveType, true, out ArchiveType archiveType))
                {
                    Log.Error("Unknown archive type");
                    throw new ArgumentException("unknown archive type");
                }

                TestSession testSession = CreateTestSession(options, arkade, archiveType);

                var archiveMetadata = JsonConvert.DeserializeObject<ArchiveMetadata>(File.ReadAllText(options.MetadataFile));

                archiveMetadata.PackageType = PackageType.SubmissionInformationPackage;

                testSession.ArchiveMetadata = archiveMetadata;
                

                arkade.CreatePackage(testSession, options.OutputDirectory);
                
                arkade.SaveReport(testSession, PrepareTestReportFile(options, testSession));
            }
            finally
            {
                ArkadeProcessingArea.CleanUp();
            }
        }

        private static TestSession CreateTestSession(CommandLineOptions options, Core.Arkade arkade, ArchiveType archiveType)
        {
            TestSession testSession;
            if (File.Exists(options.Archive))
            {
                Log.Debug("File exists");
                testSession = arkade.RunTests(ArchiveFile.Read(options.Archive, archiveType));
            }
            else if (Directory.Exists(options.Archive))
            {
                Log.Debug("Directory exists");
                testSession = arkade.RunTests(ArchiveDirectory.Read(options.Archive, archiveType));
            }
            else
            {
                throw new ArgumentException("Invalid archive path: " + options.Archive);
            }
            return testSession;
        }

        private static FileInfo PrepareTestReportFile(CommandLineOptions options, TestSession testSession)
        {
            string testReportFileName = string.Format(OutputStrings.TestReportFileName, testSession.Archive.Uuid);
            string testReportFullPath = Path.Combine(options.OutputDirectory, testReportFileName);

            return new FileInfo(testReportFullPath);
        }
    }
}