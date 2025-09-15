# Contributing

Thanks for wanting to make a contribution and wanting to improve this library for everyone! You can always reach out in the repo or the [matrix](https://matrix.to/#/!OcgAznOYIIpPAkwOhi:matrix.org?via=matrix.org). This is a guideline, use your initiative.

## How to Contribute

1.  Fork and clone the repo
2.  Run `Visual Studio Solution` to install dependencies
3.  Create a branch for your PR with `git checkout -b pr-type/your-branch-name`

## Commit Guidelines

Be sure your commit messages follow this specification: https://www.conventionalcommits.org/en/v1.0.0-beta.4/

## Branch Names (pr-type)

- `feat/` - feature
- `fix/` - bug fix
- `wip/` - testing

## Help patches

If you're adding a brand new feature, you need to make sure you add a help patch entry, here's a few tips:

- Keep the help patch simple & show the essence of the feature, remember some people may be looking at using this for the first time & it's important the help patches are clear and concise.
- Keep assets minimal (3D Models, textures) to avoid bloating the repository
- If you think a more involved example is necessary, you can always add a `Example` to the while keeping `Reference` minimalistic [providing-help](https://thegraybook.vvvv.org/reference/extending/providing-help.html)

## Relesing

Releases currently triggerd manualy with following convention:

- `fix:` will create a `0.0.x` version
- `feat:` will create a `0.x.0` version
- `BREAKING CHANGE:` will create a `x.0.0` version

## Deployment workflows

Workflows triggered with `draft new release` action, create tag:

- `vX.X.X`: stable release
- `vX.X.X-(pre|rc|etc.)`: prerelease

Appearence of tag `v*` should trigger action.

## FAQ

If you start from solution, make sure you have installed avalonia nugets for your vvvv-gamma:
```sh
nuget install Avalonia -version 11.2.1
nuget install Avalonia.Desktop -version 11.2.1
nuget install Avalonia.Themes.Fluent -version 11.2.1
nuget install Avalonia.Fonts.Inter -version 11.2.1
```
