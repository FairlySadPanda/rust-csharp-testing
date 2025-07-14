using RustCSharpDemo;
using Spectre.Console;

AnsiConsole.WriteLine("Demonstrating interactions between Rust and C#");

var one = new Point { X = 1, Y = 1 };
var two = new Point { X = 2, Y = 2 };

var distance = CalculatorWrapper.Distance(ref one, ref two);
AnsiConsole.MarkupLine($"Distance between [yellow]{one}[/] and [red]{two}[/] is [green]{distance}[/]");

var (left, right) = (2, 2);
var result = CalculatorWrapper.Add(left, right);

AnsiConsole.MarkupLine($"[yellow]{left} + {right}[/] = [green]{result}![/]");
CalculatorWrapper.SayHello("There");
AnsiConsole.MarkupInterpolated($"[red]{CalculatorWrapper.GetRandomPoint()}[/]");