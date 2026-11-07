# LAZ to LAS Converter

A Windows desktop app that converts compressed LiDAR files (`.laz`) to
uncompressed LAS (`.las`) files. It can convert many files in one run (batch).

Free tool by AgriMetSoft.

## What it does

- Select one or more `.laz` files.
- Choose an output folder.
- Convert them all to `.las`. The points are read from each `.laz` and written
  out as an uncompressed `.las` file.
- A table shows the result for each file: file name, done or not, any error,
  the number of points, and the X/Y range.
- A progress bar shows the overall progress.

It reads and writes LiDAR files using laszip4j.

## How to use

1. Open the app.
2. Select one or more `.laz` files.
3. Choose an output folder.
4. Click **Convert**. Each file is saved as `<name>.las` in that folder.

## Build from source

- Open `LAZ_to_LAS_File_Converter.csproj` in Visual Studio 2022.
- Restore the NuGet packages (IKVM and SharpZipLib).
- Build. The project targets .NET Framework 4.7.2 (WPF).

Two build notes, to be honest about the setup:

- The project references `laszip4j.dll` from a local folder outside the
  project. To build, put that DLL inside the project (for example a `lib`
  folder) and fix the reference path in the `.csproj`.
- The project is set to sign the ClickOnce manifest with a certificate. If you
  do not have that certificate, turn off "Sign the ClickOnce manifests" in the
  project properties (or remove `SignManifests` from the `.csproj`) to build.

## Notes

- This is a free tool. The source is shared so others can read it.
- laszip4j is a Java library used through IKVM, so the first start may take a
  moment.
