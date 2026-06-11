# Unity Audio Manager

A simple and lightweight audio management system for Unity projects written in C#. It provides a centralized way to handle sound effects, background music, and audio settings.

## 🛠 Project Structure

* **AudioManager.cs** — The core singleton manager responsible for playing and controlling audio.
* **AudioObject.cs / AudioObject.prefab** — A wrapper object containing a Unity `AudioSource` to handle individual playback.
* **AudioButton.cs** — A helper script to quickly attach sound effects to UI Buttons.
* **AudioDebugger.cs** — A utility script for real-time audio debugging in the editor.
* **Data / Default Sounds.asset** — A ScriptableObject configuration file holding default audio clips and settings.

## 🚀 Features
* Centralized management for Sound Effects (SFX) and Music.
* Configurable audio data asset utilizing `ScriptableObject`.
* Drop-in component for UI button click sounds.
* Modular structure easy to integrate into any project.

## 📦 Installation

1. Clone this repository or copy the project files into your Unity project's `Assets/` directory.
2. Ensure the `AudioObject` prefab is properly set up with its corresponding script attached.

## 💻 Usage

### Setting Up Audio
1. Locate or create an audio settings file in the `Data` folder (`Default Sounds.asset`).
2. Add your audio clips and assign them unique keys/identifiers.

### Playing Audio via Code
```csharp
// Play a sound effect by its identifier
AudioManager.Instance.PlaySound("ClickSound");

// Play background music
AudioManager.Instance.PlayMusic("MainMenuTheme");
```

### UI Button Integration
1. Attach the `AudioButton` script to any GameObject containing a Unity `Button` component.
2. Specify the sound key in the Inspector to play on click.

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.
