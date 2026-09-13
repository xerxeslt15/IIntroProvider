# DSK Intro Player

Emby-Server-Plugin, das ein eigenes Intro-Video vor Filmen (und optional
Serienepisoden) in ausgewählten Bibliotheken abspielt. Nutzt Embys native
`IIntroProvider`-Schnittstelle, die genau für Vorfilme/Pre-Rolls gedacht ist –
kein Client-Trick, sondern serverseitig eingebaute Emby-Funktion.

## Build (über GitHub Actions)

1. Repo auf GitHub anlegen und diesen Ordner-Inhalt hochladen (inkl. `.github/workflows/build.yml`).
2. Der Workflow "Build DSK Intro Player" läuft automatisch bei jedem Push auf `main`/`master`
   (oder manuell über "Run workflow").
3. Nach erfolgreichem Lauf unter **Actions → letzter Lauf → Artifacts** die Datei
   `DskIntroPlayer` herunterladen. Darin liegt `DskIntroPlayer.dll`.

## Installation auf dem Asustor-NAS

1. `DskIntroPlayer.dll` in den Emby-Plugin-Ordner kopieren, z. B.:
   `/usr/local/AppCentral/EmbyServer/var/plugins/`
   (Pfad kann je nach Asustor-Setup leicht abweichen – im Zweifel im Emby-Dashboard
   unter **Erweitert → Plugins** den tatsächlichen Plugin-Pfad prüfen.)
2. Deine Intro-Videodatei (z. B. `intro.mp4`) an einen Ort legen, den der Emby-Server-Prozess
   lesen kann (z. B. neben deine Medienordner, oder ein eigener `Intro`-Ordner).
3. Emby Server neu starten.
4. Im Dashboard unter **Plugins → DSK Intro Player**:
   - Pfad zur Intro-Datei eintragen (Server-Pfad, nicht der Windows/Client-Pfad).
   - Bibliotheken auswählen, in denen das Intro laufen soll.
   - Optional "auch vor Serienepisoden" aktivieren.
   - Speichern.

Danach spielt Emby das Intro automatisch vor jedem Start eines Films (bzw. einer
Episode, falls aktiviert) in den ausgewählten Bibliotheken ab – bei jedem Client,
der Embys Intro-Mechanismus unterstützt (Emby-Apps, Emby Theater, die meisten
offiziellen Clients).

## Hinweise

- Ohne ausgewählte Bibliothek bleibt das Intro komplett deaktiviert (Sicherheitsnetz
  gegen versehentliches globales Abspielen).
- Existiert die eingetragene Datei nicht (mehr), wird einfach kein Intro eingespielt
  statt eines Fehlers.
- Manche Drittclients (z. B. manche Kodi-Addons) ignorieren Embys Intro-Mechanismus –
  das ist eine Client-Einschränkung, kein Plugin-Fehler.
