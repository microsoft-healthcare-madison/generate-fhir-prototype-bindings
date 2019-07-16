##### Goals:

- Create a program to generate languange bindings to facilitate rapid prototyping and testing of changes to FHIR resources.


##### Usage:

```
  -i, --fhir-directory    Required. FHIR Base Directory

  -o, --output-path       Required. Path or filename for output

  --output-single         (Default: true) Set to output a single file

  --ts                    (Default: false) Generate TypeScript bindings

  --namespace             (Default: fhir) The Exported namespace or module name

  --help                  Display this help screen.

  --version               Display version information.
```

Example:
```
dotnet generate-fhir-prototype-bindings.dll -i /path/to/fhir -o /path/to/output --ts
```


##### To Do:

- Allow filtering on kinds (e.g., logical).
- Parse FHIR structure definitions.
- Ability to specify specific XML's to parse (replacing from structure definitions).
- Output multiple files (currently limited to single).
- Add additional language support (C#, Java).