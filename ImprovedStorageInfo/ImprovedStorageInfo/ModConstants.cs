namespace Koi.Subnautica.ImprovedStorageInfo;

/// <summary>
/// Contains all mode constants.
/// </summary>
public static class ModConstants
{
    public static class Meta
    {
        public const string Guid = "Koi.ImprovedStorageInfo";
        public const string Version = "2.2.0";
        public const string Name = "Improved Storage Info";
    }

    public static class Config
    {
        public static class Sections
        {
            public const string General = "General";
        }


        public static class Fields
        {
            public static class Enabled
            {
                public const string Label = "Enabled";
                public const bool DefaultValue = true;
                public const string Description = "Check to enable mod functionnalities, uncheck to disable.";
            }
        }
    }

    public static class Translations
    {
        public const string RootFolder = "assets/languages";

        public static class Keys
        {
            public static class ContainerEmpty
            {
                public const string Key = "ContainerEmpty";
                public const string DefaultValue = "Empty (0 / {0})";
            }

            public static class ContainerFull
            {
                public const string Key = "ContainerFull";
                public const string DefaultValue = "Full ({0} / {1})";
            }

            public static class ContainerNotEmpty
            {
                public const string Key = "ContainerNotEmpty";
                public const string DefaultValue = "({0} / {1})";
            }
        }
    }
}