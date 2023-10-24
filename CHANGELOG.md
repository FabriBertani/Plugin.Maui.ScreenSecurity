# Changelog

## 1.1.5 (10/24/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.0...v1.1.5)

**Breaking changes:**
- All methods **marked as obsolete** were removed.

**Implemented enhancements:**
- Added .net6 and .net7 targets.

## 1.1.0 (07/21/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.0...v1.1.0)

**Breaking changes:**
- All methods from the previous version **were marked obsolete** and will be removed in the next stable release.

**Implemented enhancements:**
- Added Windows support.
- Added screenshot prevention for iOS.
- Added unified endpoints to be used on all platforms without preprocessing directives.
- Added .Net6 support to all platforms.

**Fixed bugs:**
- Fixed Android thread exception: [#6 Exception: Only the original thread that created a view hierarchy can touch its views](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues/6)
- Blur screen protection not working properly due to interference with screenshot protection.
- Failed to disable and re-enable the protections due to an iOS layer issue in the screenshot protection.

## 1.0.7-beta (07/18/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.6-beta...v1.0.7-beta)

**Implemented enhancements:**
- Added .Net6 support to all platforms.

## 1.0.6-beta (07/18/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.5-beta...v1.0.6-beta)

**Fixed bugs:**
- Blur screen protection not working properly due to interference with screenshot protection.
- Failed to disable and re-enable the protections due to an iOS layer issue in the screenshot protection.

## 1.0.5-beta (07/17/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.4-beta...v1.0.5-beta)

**Implemented enhancements:**
- Added unified endpoints to be used on all platforms without preprocessing directives.
- Renamed `IOSWindowsHelper` to `IOSHelper` and added the `GetCurrentTheme` method.
- Created `StringsExtensions` helper on iOS to validate strings using regular expressions.
- Created new class name `ScreenProtectionOptions` that will be used as parameter on the new unified api.

## 1.0.4-beta (07/12/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.3-beta...v1.0.4-beta)

**Implemented enhancements:**
- Added ErrorsHandler class to avoid repeated code.
-  Improved **Android** code by:
    - Unifying the methods.
    - Implementing SetRecentsScreenshotEnabled method for Android 13 and above.
    - Implementing new ErrorsHandler.
- Improved **Windows** code by:
    - Unifying the methods.
    - Implementing new ErrorsHandler.
-  Improved **iOS** code by:
    - Splitting each protection.
    - Added improvements to overall code and performance.
    - Implementing new ErrorsHandler.


## 1.0.3-beta (07/09/2023)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.0...v1.0.3-beta)

**Implemented enhancements:**
- Added screenshot prevention for iOS

## 1.0.2-beta (06/03/2023)

**Fixed bugs:**
- Fixed Android thread exception: [#6 Exception: Only the original thread that created a view hierarchy can touch its views](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues/6)

## 1.0.1-beta (05/29/2023)

**Implemented enhancements:**
- Added Windows support