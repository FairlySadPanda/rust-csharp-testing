hello-world:
	echo "Hello, World!"

build-calculator-lib:
	cargo build --manifest-path=.\calculator\Cargo.toml

build-cs:
	dotnet build