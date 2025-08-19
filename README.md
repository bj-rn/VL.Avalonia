# VL.Avalonia

[Avalonia](https://avaloniaui.net/) UI framework for [vvvv](https://vvvv.org/) live programming environment.

`VL.Avalonia` bridges the gap between the visual programming environment VL and the Avalonia UI framework, enabling developers to create rich, interactive desktop applications using visual programming capabilities.

## Development notice

The Avalonia is big, this project aims to align Avalonia to vvvv/vl workflows. That may lead to breaking changes, deprecations etc. The project is in development stage, so be advised.

Getting in touch: [VL Avalonia Room on Matrix.org](https://matrix.to/#/!OcgAznOYIIpPAkwOhi:matrix.org?via=matrix.org)

## Installation

In order to use this library with VL you have to install the nuget that is available via nuget.org. For information on how to use nugets with VL, see [Managing Nugets](https://thegraybook.vvvv.org/reference/libraries/dependencies.html#manage-nugets) in the VL documentation. As described there you go to the commandline and then type:

For now you have to install `VL.Avalonia` packages separately:

```sh
# VL.Avalonia core packages with controls, styles, help etc.
nuget install VL.Avalonia

# VL.Avalonia.Skia Avalonia application hosting and rendering in vvvv/vl (AvaloniaLayer)
nuget install VL.Avalonia.Skia

# VL.Avalonia.Stride Experimental Avalonia application renderer for VL.Stride (AvaloniaRenderer)
nuget install VL.Avalonia.Stride

# VL.Avalonia.Custom example project on bringing your own XAML Controls in vvvv/vl
# includes additional controls
nuget install VL.Avalonia.Custom
```

Tested with `vvvv_gamma_7.0`

## How-to

Demos are available via the Help Browser!

## Contributing

[CONTRIBUTING](CONTRIBUTING.md)

## Credits

- [antokhio](https://github.com/antokhio)
- [woeishi](https://github.com/woeishi)
- [azneo](https://github.com/azeno)
- [bj-rn](https://github.com/bj-rn)
- [cloneproduction](https://github.com/cloneproduction)

Try it with vvvv, the visual live-programming environment for .NET  
Download: http://visualprogramming.net
