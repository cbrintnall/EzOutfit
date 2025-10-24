![EzOutfit](./Assets/EzOutfit_READMEPreview.png)

---

A Rimworld mod dedicated to making it faster and easier to create outfits.

## Basic Usage

1. Open up the assign menu
2. Open up "Manage Outfits"
3. Click the new "Create From" button
4. Select a pawn who's current outfit you'd like to use as a template

Once selected, a new outfit will be created using the selected pawn's apparel as a template. This can be accessed under the outfit label "[pawn's name]'s outfit."

### Mod Compatibility

This mod is proudly compatible with Assignment Copy which features similar, but well paired functionality. This has only been tested on V1.4 of Rimworld.

### Rimworld Version Map

* `V1.4 -> 0.4`
* `V1.5 -> > 0.4`
* `V1.6 -> 0.5`

### Extras

- [Steam workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2885961570)

### Contributing

## Building

1. Download and setup the `dotnet cli`
2. `cd` into `Source/EzOutfit`
3. Set the following environment variables
    * `RimworldDir` should be set to the root of your Rimworld installation (same level as the .exe)
    * `RimworldModDir` should be set to the `Mods` directory under `RimworldDir`
3. Run `dotnet build -c <V1.4 | V1.5 | V1.6>`
4. Building will output the specific version you specified. A post-build command will copy the necessary mod data into `RimworldModDir` for easy use. You can then boot up Rimworld and test.

## Build help

- Make sure you set the expected environment variables in your terminal, then if you use vscode (how I work here) make sure to open it from that terminal with `code .`. It'll inherit the environment variables and locate all the references properly.
- Using this build approach does have that drawback that you must change your Rimworld version to whichever you want to build.