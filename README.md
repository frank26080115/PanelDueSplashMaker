# PanelDueSplashMaker

This is used to create splash screens for PanelDue https://github.com/dc42/PanelDueFirmware

It automatically

 * stretches images to fit
 * converts the colour depth down to 8 bits
 * performs the conversion usig bmp2c-escher3d.exe
 * finds the appropriate COM port to open (searches by VID and PID, also searches for description matching BOSSA)
 * automatically retrieves a list of available PanelDue firmware AND downloads it for you
 * runs BOSSA from the command line with correct arguments

You can't screw up using this!

Supports BMP, JPG, PNG, without needing you to convert beforehand.

It'll even warn you if your image won't fit the available flash memory.

There's also some dithering capability, but it usually makes the image huge.

WARNING: the screen size is determined by how big of an image you try to load. If you load a big image, it will assume 800x480. If you load a small image, it assumes 480x272.

Thanks to everybody who worked on the Duet family of 3D control boards and the PanelDue. Thanks to Smark K8 for https://www.codeproject.com/Articles/66341/A-Simple-Yet-Quite-Powerful-Palette-Quantizer-in-C . Thanks to Shumatech for https://github.com/shumatech/BOSSA

=============

Written in C# and using WinForm

Pre-compiled EXE is in the release_binary directory

Otherwise, use Visual Studio to open the csproj
