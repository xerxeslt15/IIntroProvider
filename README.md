# DSK Intro Player

Emby-Server-Plugin, das ein eigenes Intro-Video vor Filmen (und optional
Serienepisoden) in ausgewählten Bibliotheken abspielt. Nutzt Embys native
`IIntroProvider`-Schnittstelle, die genau für Vorfilme/Pre-Rolls gedacht ist.

**Wichtig:** Dieses Plugin hat bewusst KEINE eigene Einstellungsseite im
Emby-Dashboard, weil die aktuelle (Beta-)Version des Emby-Webclients beim
Öffnen klassischer Plugin-Konfigurationsseiten abstürzt. Stattdessen wird
die Konfiguration in einer einfachen Textdatei direkt auf dem Server
bearbeitet.

## Build (über GitHub Actions)

1. Repo auf GitHub anlegen und diesen Ordner-Inhalt hochladen (inkl. `.github/workflows/build.yml`).
2. Der Workflow "Build DSK Intro Player" läuft automatisch bei jedem Push auf `main`/`master`
   (oder manuell über "Run workflow").
3. Nach erfolgreichem Lauf unter **Actions → letzter Lauf → Artifacts** die Datei
   `DskIntroPlayer` herunterladen. Darin liegt `DskIntroPlayer.dll`.

## Installation auf dem Asustor-NAS

1. `DskIntroPlayer.dll` in den Emby-Plugin-Ordner kopieren (z. B. den Pfad, den du
   schon beim ersten Versuch verwendet hast).
2. Deine Intro-Videodatei (z. B. `intro.mp4`) an einen Ort legen, den der Emby-Server-Prozess
   lesen kann.
3. Emby Server einmal neu starten. Dadurch legt Emby automatisch eine leere
   Konfigurationsdatei für das Plugin an.

## Konfiguration (per Datei, nicht per Weboberfläche)

1. Im Emby-Dashboard unter **Dashboard → Erweitert → Konfigurationsdatei-Pfad**
   nachsehen, wo dein Emby-Server-Datenordner liegt (oder im Dashboard unter
   "Erweitert" den Punkt, der den Programmdaten-Pfad zeigt).
2. Darin den Unterordner `plugins/configurations/` öffnen.
3. Dort liegt jetzt eine Datei namens `DskIntroPlayer.xml`. Mit einem einfachen
   Texteditor öffnen (z. B. Notepad, oder direkt über die Asustor-Weboberfläche).
4. Der Inhalt sieht ungefähr so aus:

   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <PluginConfiguration>
     <IntroFilePaths></IntroFilePaths>
     <EnabledLibraryNames></EnabledLibraryNames>
     <IncludeEpisodes>false</IncludeEpisodes>
   </PluginConfiguration>
   ```

5. Die Werte eintragen, zum Beispiel:

   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <PluginConfiguration>
     <IntroFilePaths>/volume1/Media/Intro/intro1.mp4;/volume1/Media/Intro/intro2.mp4</IntroFilePaths>
     <EnabledLibraryNames>Filme, Serien</EnabledLibraryNames>
     <IncludeEpisodes>false</IncludeEpisodes>
   </PluginConfiguration>
   ```

   - `IntroFilePaths`: ein oder mehrere vollständige Server-Pfade zu deinen Intro-Dateien, getrennt durch Semikolon (;). Bei mehreren wird bei jeder Wiedergabe zufällig eine ausgewählt.
   - `EnabledLibraryNames`: genaue Namen deiner Bibliotheken, durch Komma getrennt.
     Leer lassen = Intro komplett deaktiviert.
   - `IncludeEpisodes`: `true` oder `false` - ob das Intro auch vor Serienepisoden
     laufen soll.

6. Datei speichern, Emby Server neu starten, damit die Änderungen geladen werden.

Danach spielt Emby das Intro automatisch vor jedem Start eines Films (bzw. einer
Episode, falls aktiviert) in den ausgewählten Bibliotheken ab.

## Hinweise

- Nach JEDER Änderung an der XML-Datei muss Emby Server neu gestartet werden -
  die Datei wird nur beim Start eingelesen.
- Ohne eingetragene Bibliothek bleibt das Intro komplett deaktiviert.
- Existiert die eingetragene Datei nicht (mehr), wird einfach kein Intro eingespielt
  statt eines Fehlers.
