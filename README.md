# generate-fhir-prototype-bindings

A .Net Core utility to generate language bindings from FHIR sources for use in prototyping changes to FHIR resources.


# Usage


```
  -i, --fhir-directory    Required. FHIR Base Directory

  -o, --output-path       Required. Path or filename for output (will append appropriate extension if not provided)

  --output-single         (Default: true) Set to output a single file

  --ts                    (Default: false) Generate TypeScript bindings

  --cs                    (Default: false) Generate C# bindings

  --types-to-output       (Default: ) '|' Separated list of resources/profiles/types to export (will include all necessary types below). E.g., Topic|Subscription

  --namespace             (Default: fhir) The Exported namespace or module name

  --help                  Display this help screen.

  --version               Display version information.
```

Example:
```
dotnet generate-fhir-prototype-bindings.dll -i /path/to/fhir -o /path/to/output.ts --ts
```


## To Do:

- Allow filtering on structure types (e.g., logical).
- Parse FHIR structure definitions.
- Specify specific XMLs to parse (to replace content from structure definitions).
- Output multiple files.
- Add additional language support (Java).


## Contributing
This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

There are many other ways to contribute:
* [Submit bugs](https://github.com/microsoft-healthcare-madison/generate-fhir-prototype-bindings/issues) and help us verify fixes as they are checked in.
* Review the [source code changes](https://github.com/microsoft-healthcare-madison/generate-fhir-prototype-bindings/pulls).
* Engage with users and developers on [Official FHIR Zulip](https://chat.fhir.org/)
* [Contribute bug fixes](CONTRIBUTING.md).

See [Contributing](CONTRIBUTING.md) for more information.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

FHIR&reg; is the registered trademark of HL7 and is used with the permission of HL7. 