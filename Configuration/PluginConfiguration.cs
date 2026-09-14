using MediaBrowser.Model.Plugins;

namespace DskIntroPlayer.Configuration
{
    /// <summary>
    /// Konfiguration des DSK Intro Player Plugins.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// Ein oder mehrere Pfade zu Intro-Videodateien auf dem Emby-Server,
        /// getrennt durch Semikolon (;), z. B.:
        /// /volume1/Media/Intro/intro1.mp4;/volume1/Media/Intro/intro2.mp4
        /// Bei mehreren Einträgen wird bei jeder Wiedergabe zufällig einer ausgewählt.
        /// </summary>
        public string IntroFilePaths { get; set; } = string.Empty;

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
