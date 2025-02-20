![Microsoft Defending Democracy Program: ElectionGuard Core2][banner image]

# 🗳 ElectionGuard Core2

[![ElectionGuard Specification 1.1.0](https://img.shields.io/badge/🗳%20ElectionGuard%20Specification-1.1.0-green)](https://www.electionguard.vote) ![Github Package Action](https://github.com/microsoft/electionguard-core2/workflows/Release/badge.svg) [![license](https://img.shields.io/github/license/microsoft/electionguard-core2)](https://github.com/microsoft/electionguard-core2/blob/main/LICENSE) [![license](https://img.shields.io/nuget/v/Electionguard.Encryption)](https://www.nuget.org/packages/ElectionGuard.Encryption/)

This monorepo contains the ElectionGuard 2.0+ code. The core functionality is implemented in C++ for performance and interoperability. It provides functionality for all ElectionGuard workflows including key ceremony, ballot encryption, tally, ballot decryption, and verification. It is designed to be integrated into existing (or new) voting system software. It includes a variety of interop layers to provide functionality to languages including C, .NET, and Java.

This repository is `pre-release` software to showcase the ElectionGuard API implemented in a native language. It is not feature complete and should not be used for production applications.

## 📁 In This Repository

| File/folder                          | Description                                |
| ------------------------------------ | ------------------------------------------ |
| [`.github`](.github)                 | Github workflows and issue templates       |
| [`.vscode`](.vscode)                 | VS Code configurations                     |
| [`/bindings`](/bindings)             | Binding interfaces for different languages |
| [`/cmake`](/cmake)                   | `CMake` dependencies`                      |
| [`/include`](/include)               | Public include headers                     |
| [`/src`](/src)                       | ElectionGuard source code                  |
| [`/test`](/test)                     | Unit tests                                 |
| [`.clang-format`](.clang-format)     | Style guidelines                           |
| [`.gitignore`](.gitignore)           | Define what to ignore at commit time.      |
| [`CMakeLists.txt`](CMakeLists.txt)   | Root CMake file                            |
| [`CONTRIBUTING.md`](CONTRIBUTING.md) | Guidelines for contributing to the sample. |
| [`README.md`](README.md)             | This README file.                          |
| [`LICENSE`](LICENSE)                 | The license for the sample.                |

## ❓ What Is ElectionGuard?

ElectionGuard is an open source software development kit (SDK) that makes voting more secure, transparent and accessible. The ElectionGuard SDK leverages homomorphic encryption to ensure that votes recorded by electronic systems of any type remain encrypted, secure, and secret. Meanwhile, ElectionGuard also allows verifiable and accurate tallying of ballots by any 3rd party organization without compromising secrecy or security.

Learn More in the [ElectionGuard Repository](https://github.com/microsoft/electionguard)

## 🦸 How Can I use ElectionGuard?

ElectionGuard supports a variety of use cases. The Primary use case is to generate verifiable end-to-end (E2E) encrypted elections. The ElectionGuard process can also be used for other use cases such as privacy enhanced risk-limiting audits (RLAs). This implementation only includes encryption functions and cannot be used to generate election keys and it cannot decrypt tally results.

This C++ implementation also includes a C API that can be consumed from anywhere that can call C code directly. A .Net Standard package is also provided.

## 💻 Requirements

### All Platforms

- A [C++17](https://isocpp.org/get-started) standard compliant compiler is required to build the core library. While any modern compiler should work, the library is tested on a subset. Check out the [GitHub actions](#) to see what is officially supported.
- [GNU Make](https://www.gnu.org/software/make/manual/make.html) is used to simplify the commands and GitHub Actions. This approach is recommended to simplify the command line experience. This is built in for MacOS and Linux. For Windows, setup is simpler with [Chocolatey](https://chocolatey.org/install) and installing the provided [make package](https://chocolatey.org/packages/make). The other Windows option is [manually installing make](http://gnuwin32.sourceforge.net/packages/make.htm).
- [CMake](https://cmake.org/) is used to simplify the build experience.

### 🤖 Android

To build for android, you need the Android SDK and platforms 21 and 26. The easiest way is to download android studio. Alternatively, you can use the SDK installation that ships with the Xamarin Tooling in Visual Studio. WE also require the use of the Android NDK. Android builds can be compiled on Linux, Mac, or Windows

- [Android SDK](https://developer.android.com/studio/#downloads)
- [SDK 28](https://developer.android.com/studio/releases/platforms#9.0)
- [NDK 25](https://developer.android.com/ndk/downloads/)

### 🍏 iOS

To build for iOS you need XCode installed

- [XCode](https://developer.apple.com/xcode/resources/) and the [Command Line Tools for XCode](#)
- [CMake 3.19](https://cmake.org/) may be necessary, along with changes to the Makefile. [See ISSUE #138](https://github.com/microsoft/electionguard-cpp/issues/138)

### Linux

The automated install of dependencies is currently only supported on debian-based systems. See the makefile for more information.

### Web Assembly

Building for WebAssembly (wasm) is supported with the `emscripten` toolchain. This currently is setup for compiling on Linux and Mac only.

- Install [emscripten](https://emscripten.org/docs/getting_started/downloads.html)
- Install [Node Version Manager](https://github.com/nvm-sh/nvm)
- Ensure the latest versions of both emscripten and node are activated
- Ensure Emscripten and node are both in your path
- run `make test-wasm` to build for wasm and validate the library.

### 🖥️ Windows (using MSVC)

Building on windows is supported using the `MSVC` toolchain. MSVC is the default toolchain on Windows. All of these tools should be installed as admin or in a command prompt as admin to make sure that all of the security settings are appropriate. As for the Visual Studio 2022 install, VS 2022 Community edition will work for developing ElectionGuard. You also may use Professional or Enterprise versions.

- Install [Chocolatey](https://chocolatey.org/install)
- Install [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio Code](https://community.chocolatey.org/packages/vscode)
- Install [Github CommandLine](https://community.chocolatey.org/packages/git) or [Github CLI](https://community.chocolatey.org/packages/gh) or [Github Desktop](https://community.chocolatey.org/packages/github-desktop)
- Install [Powershell Core](https://github.com/powershell/powershell)
- Install [VS 2022](https://visualstudio.microsoft.com/vs/) or [VS 2022](https://community.chocolatey.org/packages/visualstudio2022community)
- Open the Visual Studio Installer and install
  - MSVC v142 - VS 2022 C++ x64/x86 build tools
  - Windows 10 SDK (latest)
  - C++ CMake tools for Windows
  - C++/CLI support for v142 build tools
  - ![Visual Studio Options][vs options]

#### 🚧 The Procedure Entry Point Could not be Located

When compiling with shared libraries, you may encounter an error running the unit tests project. This is likely due to windows resolving the incorrect implementation of `libstdc++-6.dll`. Solving this depends on your use case, but you can either ensure that the path modifications made above appear before any other paths which include this library (e.g. c\Windows\System32\), or you can include a copy of the correct DLL in the output folder. [See this StackOverflow post for more information](https://stackoverflow.com/questions/18668003/the-procedure-entry-point-gxx-personality-v0-could-not-be-located)

### 🌐 .NET Standard

A .NET Standard binding library is provided so that this package can be consumed from C# applications. At this time, MacOS, Linux and Windows are supported.

- [Latest DotNet SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com)
- [NuGet Command Line (CLI)](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference#macoslinux)
- On Linux, you need [Mono](https://www.mono-project.com/download/stable/)

## Build C++

Using **make**,

### Download Dependencies

```sh
make environment
```

### Build the Library for the current host (Release, default toolchain)

```sh
make build
```

### Build the Library for the current host (Debug, default toolchain)

```sh
export TARGET=Debug && make build
```

### Android

The Android Build currently Targets API Level 26 but can be configured by modifying the Makefile

Set the path to the NDK, replacing the version with your own

```sh
export NDK_PATH=/Users/$USER/Library/Android/sdk/ndk/21.3.6528147 && make build-android
```

### iOS

The iOS build currently targets iPhone OS 12 but can be configured by modifying the Makefile

Creates a fat binary for the simulator and targets a recent version of iOS

```sh
make build-ios
```

### Windows

Using the default MSVC 2022 toolchain (with optional platform builds):

```sh
make build
make build-arm64
make build-x86
make build-x64
```

## Build Wrappers

### .Net Standard 2.0

Wraps the build artifacts in a C# wrapper conforming to .Net Standard 2.0.

```sh
make build-netstandard
```

## Test

### Running the C++ and C tests on Windows using the MSVC toolchain

```sh
make test
```

### Running the netstandard tests

To run the tests when building for the current host (Linux, Mac, windows:)

```sh
make build-netstandard
make test-netstandard
```

To run the tests when building for a mobile device, you can run the .Net Standard tests using the Xamarin Test runner on the Android Emulator or the iOS simulator:

**NOTE: Xamarin build support is temporarily disabled while the project migrates to the new SDK style project format.** Please refer to ISSUE #195 for more information.

```sh
make build-netstandard
```

Then, open Visual studio for Mac and run the `ElectionGuard.Tests.Android` or `ElectionGuard.Tests.iOS` project.

## Command Line Interface

There is a command line interface in the `./apps/electionguard-cli` folder. This is a tool useful for generating test data and interacting with ElectionGuard.

```sh
make build-cli
make test-cli
make verify
```

## 📄 Documentation

## Contributing

This project encourages community contributions for development, testing, documentation, code review, and performance analysis, etc. For more information on how to contribute, see [the contribution guidelines][contributing]

### Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

### Reporting Issues

Please report any bugs, feature requests, or enhancements using the [GitHub Issue Tracker](https://github.com/microsoft/electionguard-python/issues). Please do not report any security vulnerabilities using the Issue Tracker. Instead, please report them to the Microsoft Security Response Center (MSRC) at [https://msrc.microsoft.com/create-report](https://msrc.microsoft.com/create-report). See the [Security Documentation][security] for more information.

### Have Questions?

ElectionGuard would love for you to ask questions out in the open using GitHub Discussions.

## License

This repository is licensed under the [MIT License]

## Thanks! 🎉

A huge thank you to those who helped to contribute to this project so far, including:

**[Josh Benaloh _(Microsoft)_](https://www.microsoft.com/en-us/research/people/benaloh/)**

<a href="https://www.microsoft.com/en-us/research/people/benaloh/"><img src="https://www.microsoft.com/en-us/research/wp-content/uploads/2016/09/avatar_user__1473484671-180x180.jpg" title="Josh Benaloh" width="80" height="80"></a>

**[Michael Naehrig _(Microsoft)_](https://www.microsoft.com/en-us/research/people/mnaehrig/)**

<a href="https://www.microsoft.com/en-us/research/people/mnaehrig/"><img src="https://www.microsoft.com/en-us/research/uploads/prod/2023/09/square_small.jpg" title="Michael Naehrig" width="80" height="80"></a>

**Olivier Pereira _(Microsoft)_**

**[RC Carter _(Election Tech)_](https://www.election.tech/)**

<a href="https://github.com/rc-eti"><img src="https://avatars.githubusercontent.com/u/139162996?v=4" title="rc-eti" width="80" height="80"></a>

**[Steve Maier](https://github.com/SteveMaier-IRT) [_(InfernoRed Technology)_](https://infernored.com/)**

<a href="https://github.com/SteveMaier-IRT"><img src="https://avatars.githubusercontent.com/u/82616727?v=4" title="SteveMaier-IRT" width="80" height="80"></a>

**[Keith Fung](https://github.com/keithrfung) [_(InfernoRed Technology)_](https://infernored.com/)**

<a href="https://github.com/keithrfung"><img src="https://avatars2.githubusercontent.com/u/10125297?v=4" title="keithrfung" width="80" height="80"></a>

**[Matt Wilhelm](https://github.com/AddressXception) [_(InfernoRed Technology)_](https://infernored.com/)**

<a href="https://github.com/AddressXception"><img src="https://avatars0.githubusercontent.com/u/6232853?s=460&u=8fec95386acad6109ad71a2aad2d097b607ebd6a&v=4" title="AddressXception" width="80" height="80"></a>

**[Dan S. Wallach](https://www.cs.rice.edu/~dwallach/) [_(Rice University)_](https://www.rice.edu/)**

<a href="https://www.cs.rice.edu/~dwallach/"><img src="https://avatars2.githubusercontent.com/u/743029?v=4" title="danwallach" width="80" height="80"></a>

**[Marina Polubelova](https://polubelova.github.io/) [_(INRIA Paris)_](https://prosecco.gforge.inria.fr/)**

<a href="https://polubelova.github.io/"><img src="https://polubelova.github.io/authors/admin/avatar_hu562f921c0165de84dfdc53044b574fa1_846381_270x270_fill_q90_lanczos_center.jpg" title="polubelova" width="80" height="80"></a>

**[Santiago Zanella-Béguelin](https://www.microsoft.com/en-us/research/people/santiago/) [_(Microsoft Research)_](https://www.microsoft.com/en-us/research/)**

<a href="https://www.microsoft.com/en-us/research/people/santiago/"><img src="https://www.microsoft.com/en-us/research/uploads/prod/2020/08/profile_cropped-5f44d9b09ecd7.jpg" title="santiago" width="80" height="80"></a>

**[Jonathan Protzenko](https://jonathan.protzenko.fr/) [_(Microsoft Research)_](https://www.microsoft.com/en-us/research/group/research-software-engineering-rise/)**

<a href="https://jonathan.protzenko.fr/"><img src="https://jonathan.protzenko.fr/assets/protzenko.jpg" title="protzenko" width="80" height="80"></a>

<!-- Links -->

[banner image]: https://raw.githubusercontent.com/microsoft/electionguard-python/main/images/electionguard-banner.svg
[pull request workflow]: https://github.com/microsoft/electionguard-ccpp/blob/main/.github/workflows/pull_request.yml
[contributing]: https://github.com/microsoft/electionguard-core2/blob/main/CONTRIBUTING.md
[security]: https://github.com/microsoft/electionguard-core2/blob/main/SECURITY.md
[mit license]: https://github.com/microsoft/electionguard-core2/blob/main/LICENSE
[vs options]: https://github.com/microsoft/electionguard-core2/blob/main/images/vs_options.png
