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
        /// Kommagetrennte Liste der Bibliotheks-IDs (Virtual Folder Ids),
        /// für die das Intro abgespielt werden soll.
        /// Leer = für keine Bibliothek aktiv (Intro ist deaktiviert),
        /// bis mindestens eine Bibliothek in den Plugin-Einstellungen ausgewählt wurde.
        /// </summary>
        public string EnabledLibraryIds { get; set; } = string.Empty;

        /// <summary>
        /// Wenn true, wird das Intro auch vor Serienepisoden abgespielt.
        /// Wenn false, nur vor Filmen.
        /// </summary>
        public bool IncludeEpisodes { get; set; } = false;
    }
}
