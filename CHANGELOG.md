# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.2.2] - 2020-03-14

### Changed
- Updated `com.unity.platforms` package version to `0.2.2-preview.3`.
- Updated `com.unity.platforms.desktop` package version to `0.2.2-preview.3`.

## [0.2.1] - 2020-02-25

### Changed
- Updated `com.unity.platforms` package version to `0.2.1-preview.7`.
- Updated `com.unity.platforms.desktop` package version to `0.2.1-preview.7`.
- Removed stdout redirect to Unity Console
- Add a UsesIL2CPP property to BuildTarget

## [0.2.1-preview] - 2020-01-24

### Changed
- Updated `com.unity.platforms` package version to `0.2.1-preview`.
- Updated `com.unity.platforms.desktop` package version to `0.2.1-preview`.

## [0.2.0-preview.1] - 2020-01-17

### Changed
- Updated `com.unity.platforms` package version to `0.2.0-preview.2`.
- Updated `com.unity.platforms.desktop` package version to `0.2.0-preview.1`.

## [0.2.0-preview] - 2020-01-13

### Changed
- Updated `com.unity.platforms` package version to `0.2.0-preview`.
- Updated `com.unity.platforms.desktop` package version to `0.2.0-preview`.

## [0.1.8-preview] - 2019-12-11

### Changed
- Bump com.unity.platforms version to 0.1.8-preview

## [0.1.7-preview.4] - 2019-12-09

### Changed
- Disabled burst for windows/dotnet/collections checks, because it was broken.

## [0.1.7-preview.3] - 2019-12-05

### Changed
- WindowsBuildTargets will no longer throw trying to print null once a process has terminated.

## [0.1.7-preview.2] - 2019-11-12

### Changed
- Made fields public on `DotsWindowsTarget` and friends

## [0.1.7-preview] - 2019-10-25

### Changed
- Updated `com.unity.platforms` to `0.1.7-preview`.

## [0.1.6-preview] - 2019-10-23

### Added
- Added `CanBuild` property to all build targets.

### Changed
- Updated `com.unity.platforms` to `0.1.6-preview`.

## [0.1.5-preview] - 2019-10-22

### Changed
- Updated `com.unity.platforms` to `0.1.5-preview`.
- Windows build targets `HideInBuildTargetPopup` is now `false`.

## [0.1.4-preview] - 2019-09-26
- Bug fixes  
- Add iOS platform support
- Add desktop platforms package

## [0.1.3-preview] - 2019-09-03

- Bug fixes  

## [0.1.2-preview] - 2019-08-13

### Added

- The static property `BuildTarget.DefaultBuildTarget` is set to `DotNetWindowsBuildTarget` when running Unity editor on Windows platform.

### Changed

- Support for Unity 2019.1.

## [0.1.1-preview] - 2019-06-10

- Initial release of *Unity.Platforms.Windows*.
