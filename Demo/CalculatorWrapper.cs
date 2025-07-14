// ReSharper disable CheckNamespace

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RustCSharpDemo;

/*
 * Your interface must match the expectations set by the Rust library.
 * Things like structures, types, and references must match, or you’ll likely get an error code of 139,
 * and the executing application will halt.
 */

public partial class CalculatorWrapper
{
    private const string LIBRARY_NAME = "libcalculator";

    private static readonly string[] WindowsDllNames = ["libcalculator", "calculator"];
    private static readonly string[] OsxDllNames = ["libcalculator"];
    private static readonly string[] LinuxDllNames = ["libcalculator"];

    static CalculatorWrapper()
    {
        NativeLibrary.SetDllImportResolver(typeof(CalculatorWrapper).Assembly, (_, assembly, path) =>
        {
            string[] dllNames = [];

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                dllNames = WindowsDllNames;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                dllNames = OsxDllNames;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                dllNames = LinuxDllNames;

            foreach (var dllName in dllNames)
            {
                if (NativeLibrary.TryLoad(dllName, assembly, path, out var handle))
                    return handle;
            }

            return IntPtr.Zero;
        });
    }

    [LibraryImport(LIBRARY_NAME, EntryPoint = "add")]
    public static partial int Add(int left, int right);

    [LibraryImport(LIBRARY_NAME, EntryPoint = "say_hello", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void SayHello(string name);

    [LibraryImport(LIBRARY_NAME, EntryPoint = "random_point")]
    public static partial Point GetRandomPoint();

    [LibraryImport(LIBRARY_NAME, EntryPoint = "distance")]
    public static partial double Distance(ref Point one, ref Point two);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
[DebuggerDisplay("({X}, {Y})")]
public struct Point
{
    public uint X;
    public uint Y;

    public readonly override string ToString()
        => $"(x: {X}, y: {Y})";
}