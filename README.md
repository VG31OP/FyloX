# FyloX ⚡

<p align="center">
  <b>Ultra-Fast, Offline Universal File Converter &amp; Transformer for Windows, macOS &amp; Linux.</b><br>
  Hold a hotkey (<kbd>Ctrl</kbd> on Windows/Linux, <kbd>Option</kbd> on macOS), drag any files or folders, and drop them on the intuitive donut wheel right at your cursor.
</p>

<p align="center">
  <a href="https://shiroya.in"><b>Website</b></a> ·
  <a href="https://github.com/VG31OP/FyloX/releases/latest"><b>Download</b></a> ·
  <a href="#install">Install</a> ·
  <a href="#features">Features</a> ·
  <a href="#development">Development</a> ·
  <a href="#developer">Developer</a>
</p>

<p align="center">
  <a href="https://github.com/VG31OP/FyloX/releases/latest"><img src="https://img.shields.io/github/v/release/VG31OP/FyloX?color=6366F1&label=release" alt="Latest release"></a>
  <img src="https://img.shields.io/badge/Windows-10%20%7C%2011-6366F1" alt="Windows 10 | 11">
  <img src="https://img.shields.io/badge/macOS%20%7C%20Linux-Supported-06B6D4" alt="macOS | Linux">
  <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-10B981" alt="MIT license"></a>
</p>

---

## Highlights

<table>
  <tr>
    <td width="33%" valign="top">
      <b>🎯 Instant Cursor Donut</b><br>
      No hunting for app windows. A fluid radial donut appears instantly under your mouse cursor, showing only the actions and conversions compatible with your selection.
    </td>
    <td width="33%" valign="top">
      <b>📦 246 Formats Supported</b><br>
      Convert images, documents, audio, video, 3D models, archives, and fonts seamlessly. Full support for PDF, DOCX, XLSX, PPTX, HWP/HWPX, WebP, AVIF, HEIC, MP4, MP3, and more without requiring Microsoft Office or Adobe Suite.
    </td>
    <td width="33%" valign="top">
      <b>🔒 100% Offline &amp; Private</b><br>
      Your data never leaves your workstation. Every operation runs locally with zero telemetry, zero uploads, and peak privacy.
    </td>
  </tr>
</table>

---

## Features

- **Interactive Radial Donut Menu**: Triggered with configurable modifier keys (<kbd>Ctrl</kbd>, <kbd>Alt</kbd>, or mouse gestures).
- **Modern Deep Slate Design**: Beautiful dark and light themes with electric indigo accents, refined typography, and fluid micro-animations.
- **Conversion Pipelines & Presets**: Chain multiple actions (e.g., resize image &rarr; convert to WebP &rarr; compress) into a single reusable preset.
- **Batch Processing**: Convert hundreds of files in parallel utilizing all CPU cores with negligible memory overhead.
- **Shell & Context Menu Integration**: Seamless Windows Explorer right-click integration.
- **Extensible Engine Architecture**: Modular converters powered by native assemblies, Magick.NET, FFmpeg, and high-performance rust/native tools.

---

## Install

Download the latest release from [Releases](https://github.com/VG31OP/FyloX/releases/latest).

- **Windows 10/11**:
  - Run `FyloX.exe` directly (portable) or use the installer.
  - To install the Windows context menu, run `Install-FyloX-ContextMenu.bat`.
- **macOS & Linux**:
  - Native builds available for Apple Silicon (`arm64`), Intel (`x64`), and Linux (`x64`/`arm64`).

---

## Development

Build FyloX from source using the [.NET 10 SDK](https://dotnet.microsoft.com/download):

```bash
git clone https://github.com/VG31OP/FyloX.git
cd FyloX
dotnet run --project src/FyloX.App
dotnet test
```

To create a release build:

```bash
dotnet publish src/FyloX.App/FyloX.App.csproj -c Release -r win-x64 --self-contained -p:Version=2.0.0 -p:FyloXRelease=true -o release/FyloX
```

---

## Developer & Maintainer

**Vraj Shiroya** ([@VG31OP](https://github.com/VG31OP))  
Developer • AI Enthusiast • Open-Source Contributor  
- 🌐 Website: [shiroya.in](https://shiroya.in)  
- 🐙 GitHub: [github.com/VG31OP](https://github.com/VG31OP)

---

## Support & Sponsor

FyloX is free and open-source under the MIT license. If you find FyloX valuable, starring the repository ⭐ and sharing it helps tremendously!

<p align="left">
  <a href="https://github.com/sponsors/VG31OP"><img src="https://img.shields.io/badge/GitHub%20Sponsors-EA4AAA?style=for-the-badge&logo=githubsponsors&logoColor=white" alt="GitHub Sponsors"></a>
</p>

---

## License

MIT License © 2026 Vraj Shiroya ([VG31OP](https://github.com/VG31OP)) and FyloX contributors.  
Bundled third-party components keep their respective licenses, detailed in [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
