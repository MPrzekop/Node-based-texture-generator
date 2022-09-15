# Node based texture generator

Node based texture generation/editing system. Inspired by ASE and Substance Designer. Designed for fast in-editor adjustments.
<p align="center">
  <img src="https://github.com/MPrzekop/Node-based-texture-generator/blob/Images/Images/ChannelMixing.gif" width="70%" title="Graph Demo">
</p>
 
## Features
* real-time per node preview
* Exporting to jpg, png, EXR and TGA
* dynamic node ports typing (eg. multiply can handle texture*value, value*value, vector*vector etc)
* generalized maths operations interface
* generalized value abstract node
* abstract node for shader pass node
* abstract node for compute shaders
* WIP keyboard shortcuts
* WIP test coverage

 
## Instalation
### alpha
Package is currently in alpha, meaning it can export texture to file and perform implemented operations, but there might be bugs, interfaces can change and documentation is incomplete. 
### Package Manager
Go to `Window -> Package Manager` and add from git [URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) using this URL:
`https://github.com/MPrzekop/Node-based-texture-generator.git`
Package is created using Unity 2021.3 and depends on [XNode](https://github.com/Siccity/xNode) 

## Screenshots
<p align="center">
  <img src="https://github.com/MPrzekop/Node-based-texture-generator/blob/Images/Images/Dilate.gif" width="20%" title="basic sphere with snow material">
  <img src="https://github.com/MPrzekop/Node-based-texture-generator/blob/Images/Images/PreviewColor.gif" width="20%" title="sphere without textures with glitter and SSS">
  <img src="https://github.com/MPrzekop/Node-based-texture-generator/blob/Images/Images/Resample.gif" width="20%" title="sphere with glitter and no SSS"> 
</p>
