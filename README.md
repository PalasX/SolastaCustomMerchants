# Solasta Custom Merchants

This Mod allows you to customize any vendor stock in Solasta: Crown of the Magister

# How to Compile

1. Install Visual Studio 2019
2. Edit SolastaCustomMerchants.csproj and fix your game folder on line 5
3. Use "InstallDebug" to have it installed directly to your Mods folder

# How to Install

1. Download and instal [Unity Mod Manager](https://www.nexusmods.com/site/mods/21)
2. Add below *GameInfo* XML tag, under *Config* tag, to $UNITY_MOD_MANAGER_HOME\UnityModManagerConfig.xml:
```
	<GameInfo Name="Solasta">
		<Folder>Solasta</Folder>
		<ModsDirectory>Mods</ModsDirectory>
		<ModInfo>Info.json</ModInfo>
		<GameExe>Solasta.exe</GameExe>
		<EntryPoint>[UnityEngine.UIModule.dll]UnityEngine.Canvas.cctor:Before</EntryPoint>
		<StartingPoint>[Assembly-CSharp.dll]RuntimeManager.LoadRuntime:After</StartingPoint>
		<MinimalManagerVersion>0.22.13</MinimalManagerVersion>
	</GameInfo>
```
3. Execute Unity Mod Manager, Select Solasta, and Install
4. Select Mods tab, drag and drop from releases

# How to Debug

1. Download and install [7zip](https://www.7-zip.org/a/7z1900-x64.exe)
2. Download Unity Editor 2019.4.1 from [Unity Archive](https://unity3d.com/get-unity/download/archive)
3. Open Downloads folder
4. Right-click UnitySetup64-2019.4.1f1.exe, 7Zip -> Extract Here
5. Open Solasta game folder
6. Backup Solasta.exe and UnityPlayer.dll to Solasta.exe.Original and UnityPlayer.dll.original respectively
7. Locate WindowsPlayer.exe and UnityPlayer.dll under YOUR_DOWNLOADS_FOLDER\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono and copy them over to Solasta game folder
8. Rename WindowsPlayer.exe to Solasta.exe
9. Edit Solasta_Data\boot.config and add:
```
wait-for-managed-debugger=1
player-connection-debug=1
```

You can now attach the Unity Debugger from Visual Studio 2019, Debug -> Attach Unity Debug

# How to Customize

1. Open $SOLASTA_HOME/Mods/SolastaCustomMerchants/Merchants.json on a text editor
2. Add new vendors to the list
3. Add new items under vendors
4. (Optionally), set stock properties on items (i.e: faction, max amount, initial amount)

You can lookup all existing merchants and items names at https://github.com/spacehamster/SolastaDataminer/releases/tag/0.3.3

# Credits

* @Spacehamster for all coding/hacking support (my other car is a cdr. answer is certainly 42!).
* @susphiciousone for original idea.
