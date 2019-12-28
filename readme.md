projlint
========

A tool for maintaining .NET project conventions and boilerplate



Description
===========

Adhering to conventions and maintaining repetitive configuration boilerplate
takes time and effort away from development of the software itself.  The
overhead only gets worse as the size and number of projects increases.

_projlint_ checks for adherence to conventions and the presence of necessary
boilerplate.  It also (optionally) applies those conventions and maintains that
boilerplate.  It attempts to do so as automatically, intelligently, and
non-destructively as possible.

_projlint_ operates on the Git working directory it is run from.



Synopsis
========

    projlint [command] [args]



Commands
========

    analyse
    analyze
        Check for convention violations (default)

    apply
        Apply conventions

    help
        Display usage information



Conventions
===========

Text files use Unix-style line endings.

Text files use UTF-8 encoding with no byte-order marks.

Repository root directories contains single `.sln` files.

`.sln` filenames match Git repository names.

`.sln` files use Windows-style line endings because Visual Studio (re)writes
them that way regardless of their existing format or `.editorconfig` settings.

Subdirectories that contain `.cs` files also contain a single `.csproj` file.

Names of `.csproj` files match names of containing subdirectories.

Root directories contain `.gitignore` files.

`.gitignore` files contain appropriate entries for .NET projects.

Root directories contain `.gitattributes` files.

`.gitattributes` files contain directive(s) to disable Git line-ending
conversion.

Root directories contain `.editorconfig` files.

`.editorconfig` files contain directives matching as many of the above
conventions as possible.



License
=======

[MIT License](https://github.com/macro187/projlint/blob/master/license.txt)



Copyright
=========

Copyright (c) 2019  
Ron MacNeil \<<https://github.com/macro187>\>  

