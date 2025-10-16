namespace Koi.Subnautica.ImprovedScanInfo_BZ
{
    /// <summary>
    /// Contains all mode constants.
    /// </summary>
    public static class ModConstants
    {
        public static class Meta
        {
            public const string Guid = "Koi.ImprovedScanInfo";
            public const string Version = "1.2.0";
            public const string Name = "Improved Scan Info";
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
                public static class BlueprintAlreadySynthetized
                {
                    public const string Key = "BlueprintAlreadySynthetized";
                    public const string DefaultValue = "Blueprint already synthethized";
                }
            }
        }
    }
}