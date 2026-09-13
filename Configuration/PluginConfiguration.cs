using MediaBrowser.Model.Plugins;

namespace DskIntroPlayer.Configuration
{
    /// <summary>
    /// Konfiguration des DSK Intro Player Plugins.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// Vollständiger Pfad zur Intro-Videodatei auf dem Emby-Server
        /// (z. B. /volume1/Media/Intro/intro.mp4 oder C:\Intro\intro.mp4).
        /// </summary>
        public string IntroFilePath { get; set; } = string.Empty;

        /// <summary>
        /// Kommagetrennte Liste der Bibliotheksnamen (genau wie im Emby-Dashboard
        /// unter "Bibliotheken" angezeigt), für die das Intro abgespielt werden soll.
        /// Leer = Intro komplett deaktiviert, bis mindestens ein Name eingetragen wurde.
        /// </summary>
        public string EnabledLibraryNames { get; set; } = string.Empty;

        /// <summary>
        /// Wenn true, wird das Intro auch vor Serienepisoden abgespielt.
        /// Wenn false, nur vor Filmen.
        /// </summary>
        public bool IncludeEpisodes { get; set; } = false;
    }
}
