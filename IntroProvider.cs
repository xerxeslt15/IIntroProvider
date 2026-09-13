using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;

namespace DskIntroPlayer
{
    /// <summary>
    /// Stellt Emby das konfigurierte Intro-Video als Vorfilm ("Pre-Roll") zur Verfügung.
    /// Emby ruft GetIntros(...) automatisch vor jeder Wiedergabe eines passenden
    /// Items auf und spielt die zurückgegebenen Pfade davor ab.
    /// </summary>
    public class IntroProvider : IIntroProvider
    {
        public string Name => "DSK Intro Player";

        /// <summary>
        /// Alle Dateien, die als Intro infrage kommen (für Emby's interne Verwaltung/Debug).
        /// </summary>
        public IEnumerable<string> GetAllIntroFiles()
        {
            var path = Plugin.Instance?.Configuration.IntroFilePath;

            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                yield return path;
            }
        }

        public Task<IEnumerable<IntroInfo>> GetIntros(BaseItem item, User user)
        {
            var config = Plugin.Instance?.Configuration;

            if (config == null || string.IsNullOrWhiteSpace(config.IntroFilePath))
            {
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            if (!File.Exists(config.IntroFilePath))
            {
                // Datei nicht (mehr) vorhanden -> kein Intro einspielen, statt einen Fehler zu werfen.
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            // Nur vor Filmen, optional zusätzlich vor Serienepisoden.
            var isMovie = item is Movie;
            var isEpisode = item is Episode;

            if (!isMovie && !(isEpisode && config.IncludeEpisodes))
            {
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            var enabledNames = ParseEnabledLibraryNames(config.EnabledLibraryNames);

            // Solange keine Bibliothek eingetragen wurde, bleibt das Intro komplett deaktiviert.
            if (enabledNames.Count == 0)
            {
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            var topParent = item.GetTopParent();
            var libraryName = topParent?.Name;

            if (string.IsNullOrEmpty(libraryName) || !enabledNames.Contains(libraryName))
            {
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            var intro = new IntroInfo
            {
                ItemId = 0,
                Path = config.IntroFilePath
            };

            return Task.FromResult<IEnumerable<IntroInfo>>(new[] { intro });
        }

        private static HashSet<string> ParseEnabledLibraryNames(string raw)
        {
            var parts = (raw ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0);

            return new HashSet<string>(parts, StringComparer.OrdinalIgnoreCase);
        }
    }
}
