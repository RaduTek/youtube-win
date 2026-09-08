# YouTube for Windows

YouTube desktop client for recreation backends (like [BackTube](https://github.com/RaduTek/BackTube) or [yt2009](https://github.com/ftde0/yt2009))

![Screenshot of application](docs/overview.png)

[Install with ClickOnce](https://pub.radutek.uk/ClickOnce/YouTube.application) (also updates automatically)

[More screenshots](docs/screenshots.md)

## Why?

On some 2000s low end devices, like UMPCs, the Flash Player is barely able to play videos in yt2009 at normal frame rate. But those same videos play just fine in a desktop video player application, like Windows Media Player with the right codecs installed.

So this .NET Framework 2.0 application offers a native frontend for the server, using the YouTube Data API v2.0, that's also used by mobile applications.

New in version 2.0: Browse and watch videos directly in the application, without opening a separate player. Hardware acceleration is supported too.

<!-- ## Features

- Search for videos
- Download videos
- Queue videos for playback into a temporary playlist
- Prioritizes downloaded videos over streaming -->

## How to use

- Install with ClickOnce, download a pre-built version or build the application yourself.
- Launch it, go to Settings and type in the address of your instance
- Enjoy: search for and watch videos

## Requirements

- .NET Framework 2.0 or higher (3.5 required for ClickOnce installation)
- Internet Explorer 7 or later (>= 8 is recommended)

(Internet Explorer is used to render the video lists and the video description)

### Compatibility

**Tested on Windows XP with Windows Media Player 10, Windows Vista, Windows 7 and Windows 10.**

- **Windows XP and Vista**: A codec pack capable of demuxing MP4 and decoding H.264 and AAC is necessary. I've used K-Lite Codec Pack.

- **Windows 7 and later**: All necessary codecs are already included with WMP.

### Tests

- Samsung Q1 Ultra (Intel A100 and A110): Play videos smoothly in 360p only
- Eee PC 901 (Intel Atom N270): Play videos smoothly in 480p, sometimes 720p (especially with Super Hybrid Engine / overclock)
- Sony VAIO P (Intel Atom Z540 - GMA 500 / Poulsbo US15W): Play videos smoothly in 1080p with hardware acceleration (working in Windows 7 out-of-box) - CPU usage at 20-30%.