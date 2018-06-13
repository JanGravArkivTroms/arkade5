namespace Arkivverket.Arkade.Util
{
    public class ArkadeConstants
    {
        public const string SystemLogFileNameFormat = "arkade-{Date}.log";

        public const string NoarkihXmlFileName = "NOARKIH.XML";
        public const string AddmlXmlFileName = "addml.xml";
        public const string AddmlXsdFileName = "addml.xsd";
        public const string ArkivstrukturXmlFileName = "arkivstruktur.xml";
        public const string ArkivstrukturXsdFileName = "arkivstruktur.xsd";
        public const string MetadatakatalogXsdFileName = "metadatakatalog.xsd";
        public const string InfoXmlFileName = "info.xml";
        public const string ArkadeXmlLogFileName = "arkade-log.xml";
        public const string EadXmlFileName = "ead.xml";
        public const string EacCpfXmlFileName = "eac-cpf.xml";
        public const string DiasPremisXmlFileName = "dias-premis.xml";
        public const string DiasPremisXsdFileName = "dias-premis.xsd";
        public const string DiasMetsXmlFileName = "dias-mets.xml";
        public const string DiasMetsXsdFileName = "dias-mets.xsd";
        public const string LogXmlFileName = "log.xml";
        public const string ArkivuttrekkXmlFileName = "arkivuttrekk.xml";
        public const string PublicJournalXmlFileName = "offentligJournal.xml";
        public const string PublicJournalXsdFileName = "offentligJournal.xsd";
        public const string RunningJournalXmlFileName = "loependeJournal.xml";
        public const string RunningJournalXsdFileName = "loependeJournal.xsd";
        public const string ChangeLogXmlFileName = "endringslogg.xml";
        public const string ChangeLogXsdFileName = "endringslogg.xsd";
        public const string MetadataPredefinedFieldValuesFileName = "metadata-feltverdier.xml";

        public const string AddmlXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.addml.xsd";
        public const string ArkivstrukturXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.arkivstruktur.xsd";
        public const string MetadatakatalogXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.metadatakatalog.xsd";
        public const string DiasPremisXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.DIAS_PREMIS.xsd";
        public const string DiasMetsXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.mets.xsd";
        public const string ChangeLogXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.endringslogg.xsd";
        public const string PublicJournalXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.offentligJournal.xsd";
        public const string RunningJournalXsdResource = "Arkivverket.Arkade.ExternalModels.xsd.loependeJournal.xsd";

        public const string DirectoryNameArkadeProcessingAreaRoot = "arkade-tmp";
        public const string DirectoryNameArkadeProcessingAreaWork = "work";
        public const string DirectoryNameArkadeProcessingAreaLogs = "logs";
        public const string DirectoryNameTemporaryLogsLocation = ".arkade-tmplogs";
        public const string DirectoryNameRepositoryOperations = "repository_operations";
        public const string DirectoryNameContent = "content";
        public const string DirectoryNamePackageOutputContainer = "Arkadepakke";
        public const string DirectoryNameAppDataArkadeSubFolder = "Arkivverket";
        
        public static readonly string[] DocumentDirectoryNames =
            { "dokumenter", "DOKUMENTER", "dokument", "DOKUMENT" };
    }
}