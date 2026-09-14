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
    /// Stellt Emby ein konfiguriertes Intro-Video als Vorfilm ("Pre-Roll") zur Verfügung.
    /// Emby ruft GetIntros(...) automatisch vor jeder Wiedergabe eines passenden
    /// Items auf und spielt die zurückgegebenen Pfade davor ab. Sind mehrere
    /// Intro-Dateien konfiguriert, wird bei jeder Wiedergabe zufällig eine davon gewählt.
    /// </summary>
    public class IntroProvider : IIntroProvider
    {
        private static readonly Random Rng = new Random();

        public string Name => "DSK Intro Player";

        /// <summary>
        /// Alle Dateien, die als Intro infrage kommen (für Emby's interne Verwaltung/Debug).
        /// </summary>
        public IEnumerable<string> GetAllIntroFiles()
        {
            var config = Plugin.Instance?.Configuration;

            if (config == null)
            {
                yield break;
            }

            foreach (var path in ParseIntroFilePaths(config.IntroFilePaths))
            {
                if (File.Exists(path))
                {
                    yield return path;
                }
            }
        }

        public Task<IEnumerable<IntroInfo>> GetIntros(BaseItem item, User user)
        {
            var config = Plugin.Instance?.Configuration;

            if (config == null || string.IsNullOrWhiteSpace(config.IntroFilePaths))
            {
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

            // Nur tatsächlich vorhandene Dateien berücksichtigen, statt bei einer
            // fehlenden Datei direkt ganz auf das Intro zu verzichten.
            var availablePaths = ParseIntroFilePaths(config.IntroFilePaths)
                .Where(File.Exists)
                .ToList();

            if (availablePaths.Count == 0)
            {
                return Task.FromResult(Enumerable.Empty<IntroInfo>());
            }

            var chosenPath = availablePaths[Rng.Next(availablePaths.Count)];

            var intro = new IntroInfo
            {
                ItemId = 0,
                Path = chosenPath
            };

            return Task.FromResult<IEnumerable<IntroInfo>>(new[] { intro });
        }

        private static List<string> ParseIntroFilePaths(string raw)
        {
            return (raw ?? string.Empty)
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
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
