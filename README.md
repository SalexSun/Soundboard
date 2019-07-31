Soundboard
=================

Provides a way to use a secondary keyboard on windows PC as soundboard device

Forked from this project
https://github.com/aelij/RawInputProcessor

Known issues
- project is still work in progress 
- key input from secondary keyboard is not ignored by windows
- when playing a sound first time after startup the sound may not be clear because to the audio file is not yet loaded

Config keyboard
- configuration by editing SoundboardApp.exe.config
- one time config to select which keyboard to be used for trigger of sounds
    1) run the app
	2) press a key on the desired keyboard
	3) copy the id in the 'Device Details' section
	4) open the file SoundboardApp.exe.config
	5) open the link to xml escape tool
	6) format the Device id to escape xml special characters
	7) past this newly formated string in the value field of the "mykeyboardid" key

config audio
- all audio files must have a .wav format
- default location for audio source files is C:\Soundboard\Audio\, this is configurable in SoundboardApp.exe.config
- the filename specifies to which the keyboard key the audio file is mapped. 
    (example: the file C:\Soundboard\Audio\Q.wav links to the Q keyboard key. For full list of all keyboard keys including special keys such as arrows etc. visit https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netframework-4.8 