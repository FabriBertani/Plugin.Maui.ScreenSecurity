# Changelog

## 1.2.3 (2025/10/09)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.2.2...v1.2.3)

**Implemented enhancements:**
- Added the `ThrowErrors` property to allow users to catch exceptions with the new error handling implementation.
- Added better hooks, error handling and logging.
- Added support for Windows 10 Version 2004 and above.
- Applied code improvements to the library.
- Updated sample project.
- Changed CHANGELOG date format to **ISO 8601** `(yyyy-MM-dd)`.

**Fixed bugs:**
- Fixed [#57 (Crash release - Main Thread issue)](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues/57)

## 1.2.2 (2025/06/02)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.2.1...v1.2.2)

**Fixed bugs:**
- Fixed a bug that prevented samples projects from compiling.
- Fixed package build to properly include Windows platform.

## 1.2.1 (2025/05/25)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.2.0...v1.2.1)

**Implemented enhancements:**
- Removed .Net7 and added .Net9 support to all platforms.
- Applied code improvements to the library and the sample projects.
- Updated sample projects to .Net9.

**Fixed bugs:**
- Fixed issue where blur protection was not being disabled.

## 1.2.0 (2024/10/14)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.6...v1.2.0)

**Implemented enhancements:**
- Added `Blazor` sample to showcase the implementation of this plugin.
- Added `IsProtectionEnabled` property to check if screen protection is already enabled or disabled.
- Added `ScreenCaptured` event handler, which triggers notifications when a screenshot is taken or the screen is recorded.
- Added plugin initialization.
- Updated sample projects with new implementations.

**Fixed bugs:**
- Implemented a new screenshot prevention method for iOS 17+.
- Fixed `GetWindow` method on iOS.
- Merged [#39: Bug iOS on Blur](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/pull/39) PR by [fabien367](https://github.com/fabien367).

## 1.1.8-beta (2024/05/25)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.7-beta...v1.1.8-beta)

**Implemented enhancements:**
- Added `Blazor` sample to showcase the implementation of this plugin.
- Added `IsProtectionEnabled` property to check if screen protection is already enabled or disabled.

## 1.1.7-beta (2024/05/18)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.6...v1.1.7-beta)

**Fixed bugs:**
- Implemented a new screenshot prevention method for iOS 17.
- Fixed `GetWindow` method on iOS.

## 1.1.6 (2024/03/18)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.5...v1.1.6)

**Implemented enhancements:**
- Removed .Net6 and added .Net8 support to all platforms.
- Added general code improvements.

**Fixed bugs:**
- Merged [#22: iOS 17 fix](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/pull/22) PR by [Gogzs](https://github.com/Gogzs).- Fixed screenshot not working on iOS 17+ issue, by changing the screenshot protection implementation, now a blank white or black (depending on the current OS theme) is added before taking the screenshot to cover the screen content.

## 1.1.5 (2023/10/24)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.1.0...v1.1.5)

**Breaking changes:**
- All methods **marked as obsolete** were removed.

**Implemented enhancements:**
- Added .net6 and .net7 targets.

## 1.1.0 (2023/07/21)
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

## 1.0.7-beta (2023/07/18)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.6-beta...v1.0.7-beta)

**Implemented enhancements:**
- Added .Net6 support to all platforms.

## 1.0.6-beta (2023/07/18)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.5-beta...v1.0.6-beta)

**Fixed bugs:**
- Blur screen protection not working properly due to interference with screenshot protection.
- Failed to disable and re-enable the protections due to an iOS layer issue in the screenshot protection.

## 1.0.5-beta (2023/07/17)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.4-beta...v1.0.5-beta)

**Implemented enhancements:**
- Added unified endpoints to be used on all platforms without preprocessing directives.
- Renamed `IOSWindowsHelper` to `IOSHelper` and added the `GetCurrentTheme` method.
- Created `StringsExtensions` helper on iOS to validate strings using regular expressions.
- Created new class name `ScreenProtectionOptions` that will be used as parameter on the new unified api.

## 1.0.4-beta (2023/07/12)
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


## 1.0.3-beta (2023/07/09)
[Full Changelog](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/compare/v1.0.0...v1.0.3-beta)

**Implemented enhancements:**
- Added screenshot prevention for iOS

## 1.0.2-beta (2023/06/03)

**Fixed bugs:**
- Fixed Android thread exception: [#6 Exception: Only the original thread that created a view hierarchy can touch its views](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues/6)

## 1.0.1-beta (2023/05/29)

**Implemented enhancements:**
- Added Windows support