using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using DskIntroPlayer.Configuration;

namespace DskIntroPlayer
{
    /// <summary>
    /// DSK Intro Player - spielt ein eigenes Intro-Video vor Filmen
    /// (und optional Serienepisoden) ausgewählter Bibliotheken ab.
    /// </summary>
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public static Plugin Instance { get; private set; }

        // Fest vergebene, eindeutige Plugin-Id - nicht mehr ändern.
        public override Guid Id => new Guid("7d3f6a1b-9c2e-4d5a-8b7f-1e2c3d4a5b6c");

        public override string Name => "DSK Intro Player";

        public override string Description =>
            "Spielt ein eigenes Intro-Video vor Filmen (und optional Serienepisoden) " +
            "in ausgewählten Bibliotheken ab.";

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "DskIntroPlayer",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.configPage.html",
                    EnableInMainMenu = true
                }
            };
        }
    }
}
