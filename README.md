# ➡️ Jellyfin Skippers (beta)

<div align="center">
  <img alt="Plugin Banner" src="https://raw.githubusercontent.com/RichardDorian/JellyfinSkippers/main/images/banner.png" />
</div>

> [!WARNING]
> This plugin is in beta and might not work as expected. Please report any issues you encounter [here](https://github.com/RichardDorian/JellyfinSkippers/issues).

**Skippers** is a plugin that exposes an API route that let third party clients show a skip button based on the user configuration. For example this lets you skip intros or credits (post credit scenes) to get to actual content faster.

> [!IMPORTANT]
> Unlike other plugins, this one doesn't automatically detect intros or credits. I created this plugin for personal use. If you take the time to "map" intros and credits for your library you'll find this plugin useful. If you don't, other plugins might be a better fit for you.

There are currently three types of skips:

- `Intro`: Skip the intro of a movie or episode.
- `PostCreditScene`: When the movie has a post credit scene. At the end of the movie, this skip will let you skip the credits and go directly to the post credit scene.
- `Other`: Feel free to use this type if none of the above fit your needs.

## 🏗️ Installation

1. Go the the plugin repositories section in your Jellyfin server settings (Home > Dashboard > Plugins > Repositories).
2. Set a name for the repository (e.g. `Skippers`) and set the url to

```
https://raw.githubusercontent.com/RichardDorian/JellyfinSkippers/main/manifest.json
```

3. Click save, go to the catalog section and find the plugin.

4. Select the latest version from the dropdown menu and click install.

5. See [Edit Skipper](#edit-skippers) to edit the config file.

## 📝 Edit skippers

A skipper is an entry in the configuration file. Every skipper has an identifier (media associated to the skipper) and a list of skips.

You can edit the configuration file located here `plugins/configurations/RichardDorian.Plugin.Skippers.xml`.

Here's an example (`Start` and `End` are in seconds):

```xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfSkipper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Skipper>
    <Id>60eed19d-d6be-43fe-b80a-c6d3dc08b317</Id>
    <Skips>
      <Skip>
        <Type>Intro</Type>
        <Start>60</Start>
        <End>74</End>
      </Skip>
      <Skip>
        <Type>PostCreditScene</Type>
        <Start>1556</Start>
        <End>1597</End>
      </Skip>
    </Skips>
  </Skipper>

  <Skipper>
    <Id>5c1a5315-9e15-40da-8d34-9efd3b5cd1fa</Id>
    <Skips>
      <Skip>
        <Type>Other</Type>
        <Start>12</Start>
        <End>19</End>
      </Skip>
    </Skips>
  </Skipper>
</ArrayOfSkipper>
```

When you're finished editing the config file, run the scheduled task called `Update Skippers`. This will read the config file and updater the skippers.

## 🌐 API

The plugin exposes the following API route (which requires authentication):

```http
GET /Item/{Id}/Skips
Accept: application/json
```

If the requested item doesn't have skips, a `404` will be returned. Otherwise an array of skips are returned in a JSON format. Here's an example

```http
GET /Item/60eed19d-d6be-43fe-b80a-c6d3dc08b317/Skips
Accept: application/json
```

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8

[
  {
    "Type": "Intro",
    "Start": 60,
    "End": 74,
    "ShowPromptAt": 60.5,
    "HidePromptAt": 64.7
  },
  {
    "Type": "PostCreditScene",
    "Start": 1556,
    "End": 1597,
    "ShowPromptAt": 1556.5,
    "HidePromptAt": 1569.7
  },
]
```

<!-- prettier-ignore -->
> [!NOTE]
> `ShowPromptAt` and `HidePromptAt` are computed values based on `Start` and `End`. This is supposed to tell the client when to show the prompt button and when to hide it. This is useful if you want to show the prompt button only when the skip is about to happen. Though, this is not required and you can ignore these values or use your own based on your own computations.

<!-- prettier-ignore -->
### ✨ A special thank to [@ConfusedPolarBear](https://github.com/ConfusedPolarBear) for writing [IntroSkipper](https://github.com/ConfusedPolarBear/intro-skipper). As I never wrote a Jellyfin plugin and a single line of C# in my life before, I used their plugin as I base.
