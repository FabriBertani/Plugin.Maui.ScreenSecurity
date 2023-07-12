# Changelog

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