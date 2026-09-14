using System;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using DskIntroPlayer.Configuration;

namespace DskIntroPlayer
{
    /// <summary>
    /// DSK Intro Player - spielt ein eigenes Intro-Video vor Filmen
    /// (und optional Serienepisoden) ausgewählter Bibliotheken ab.
    ///
    /// Bewusst OHNE eigene Web-Konfigurationsseite: Die Einstellungen werden
    /// direkt in der von Emby automatisch angelegten Konfigurationsdatei
    /// bearbeitet (siehe README.md). Das umgeht Kompatibilitätsprobleme des
    /// (Beta-)Webclients mit klassischen Plugin-Konfigurationsseiten.
    /// </summary>
    public class Plugin : BasePlugin<PluginConfiguration>
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
            "in ausgewählten Bibliotheken ab. Konfiguration erfolgt über eine Datei " +
            "(siehe README.md), nicht über die Weboberfläche.";
    }
}
