// ReSharper disable CheckNamespace

namespace RustCSharpDemo;

/*
 * Your interface must match the expectations set by the Rust library.
 * Things like structures, types, and references must match, or you’ll likely get an error code of 139,
 * and the executing application will halt.
 */

public partial class CalculatorWrapper
{
    private const string LibraryName = "libcalculator";

    private static readonly string[] WindowsDllNames = ["libcalculator", "calculator"];
    private static readonly string[] OsxDllNames = ["libcalculator"];
    private static readonly string[] LinuxDllNames = ["libcalculator"];

    static CalculatorWrapper()
    {
        NativeLibrary.SetDllImportResolver(typeof(CalculatorWrapper).Assembly, (name, assembly, path) =>
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
                Console.WriteLine($"Loading using {dllName}");

                if (NativeLibrary.TryLoad(dllName, assembly, path, out var handle))
                    return handle;
            }

            return IntPtr.Zero;
        });
    }

    [LibraryImport(LibraryName, EntryPoint = "add")]
    public static partial int Add(int left, int right);

    [LibraryImport(LibraryName, EntryPoint = "say_hello", StringMarshalling = StringMarshalling.Utf8)]
    public static partial void SayHello(string name);

    [LibraryImport(LibraryName, EntryPoint = "random_point")]
    public static partial Point GetRandomPoint();

    [LibraryImport(LibraryName, EntryPoint = "distance")]
    public static partial double Distance(ref Point one, ref Point two);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
[DebuggerDisplay("({X}, {Y})")]
public struct Point
{
    public UInt32 X;
    public UInt32 Y;

    public override readonly string ToString()
        => $"(x: {X}, y: {Y})";
}