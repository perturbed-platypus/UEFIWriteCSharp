# UEFIWriteCShasp

UEFIWriteCShasp is a C# application that will write a buffer into a UEFI Variable that is intended to be executed by [UEFIReadCSharp](http://github.com/perturbed-platypus/UEFIReadCSharp).

The UEFI Variable it will write is "CSHARP-UEFI" and will use the GUID "{E660597E-B94D-4209-9C80-1805B5D19B69}" which is the VirtualBox Test GUID.

## How to Play

1. Put your payload in `buf.cs` as a byte array in `public static byte[] buffer`
1. Build the project using msbuild / visual studio
1. Run the binary to Hide yo Sh!t!
