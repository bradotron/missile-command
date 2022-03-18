# missile-command

# Project Setup
1. New Project - 2D Core
2. Add Packages (Window > Package Manager)
    * Universal Render Pipeline (Universal RP or URP)
    * Cinemachine (if your camera will be static for the whole game, this is probably unnecessary)
    * *Note - if you have csharp project open in VSCode or Visual Studio, installing a package may error out.*

## URP Setup
After the package is installed, you need to add an URP asset.
* In the Project view...
    * Right Click to open the menu
    * Rendering > Universal Render Pipeline > Pipeline Asset
    * For 2D projects: delete the `UniversalRenderPipelineAsset_Renderer`
    * Right Click > Rendering > Universal Render Pipeline > 2D Renderer
    * Rename the renderer if desired
    * Select the `UniversalRenderPipelineAsset` and drag the 2D Renderer to the `Renderer List`, expand the `General` tab if you do not see it.
* Open the Edit Menu, select `Project Settings`
    * Select `Graphics`
    * Click the target symbol in the `Scriptable Render Pipeline Settings`
    * Select the `UniversalRenderPipelineAsset` that was created'
    * close the settings

## Basic Scene Setup
* Rename your scene to `GameScene` or whatever you'd like
* Update the camera 
    * Select the `Main Camera`
    * Check that the `Transform` is on `(0, 0, -10)`
    * Under `Projection` check that `Projection` is set to `Orthographic`
    * Set the `Size` to `10` - *the size represents the number of units up/down to the camera edges on the `Y` axis. i.e. the camera is on `(0,0)`, the top edge of the camera will be at `(x, 10)` and the bottom will be `(x, -10)`.*
    * Under `Environment` change the `Background Type` and `Background` to your desired settings

## Cinemachine
* If you do not have the `Cinemachine` menu, install the `Cinemachine` package if you haven't yet
* Select Cinemachine > Create 2D Camera
* Select your new Camera, probably named like `CM vcam1`
* The only thing you really need to update is under `Lens` set the `Orthographic Size` to match the `Othographic Size` you want for an initial zoom setting, something like `10`.
* Until you need the camera to move/zoom, this is about all you need to do for now.