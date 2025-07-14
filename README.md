# C# using a Rust Library - Demo

A demonstration of using a Rust library in a C# program. The Rust library is built during the precompile step of the C# program, and the resultant library is translated to where the C# program can use it, with custom DLL resolvers to cope with Windows, OSX and Linux.

# How to run this program

You'll need Rust and Dotnet 9 installed on your machine.

1. Pull and open this repo.
2. Run `dotnet build`
3. Hopefully see console output doing some trivial work.

A makefile is supplied for the truly laziest developers.

# Credits

https://khalidabuhakmeh.com/working-with-rust-libraries-from-csharp-dotnet-applications for the bulk of the code.
