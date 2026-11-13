# NuGet package upgrade assessment

_Mode: **quick assessment** — package API diffs only; no per-project source scan was run._

## Recommended versions

- **Microsoft.AspNetCore.Authentication.JwtBearer**: **10.0.15** (unified across 1 project(s)).
- **Microsoft.AspNetCore.Http.Abstractions**: **2.3.14** (unified across 1 project(s)).
- **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation**: **10.0.15** (unified across 1 project(s)).
- **Microsoft.AspNetCore.Mvc.ViewFeatures**: **2.3.14** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore**: **10.0.15** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.Abstractions**: **11.0.0** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.Design**: **10.0.15** (unified across 2 project(s)).
- **Microsoft.EntityFrameworkCore.Proxies**: **10.0.15** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.Relational**: **10.0.15** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.Tools**: **11.0.0** (unified across 1 project(s)).
- **Microsoft.Extensions.Configuration**: **11.0.0** (unified across 2 project(s)).
- **Microsoft.Extensions.Configuration.Json**: **11.0.0** (unified across 1 project(s)).
- **Microsoft.Extensions.Identity.Core**: **11.0.0** (unified across 2 project(s)).
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets**: ⚠ **divergent** — projects need or support different versions:
    - FS.Store.WebAPI.csproj: 1.24.2-preview.1 → ⚠ no supported version — this project cannot be upgraded
- **MySql.EntityFrameworkCore**: **10.0.12** (unified across 1 project(s)).
- **System.Linq.Dynamic.Core**: **1.7.4** (unified across 1 project(s)).
- **System.Security.Cryptography.Pkcs**: **11.0.0** (unified across 3 project(s)).
- **System.Security.Cryptography.Xml**: **11.0.0** (unified across 3 project(s)).

## Public API changes

> **Types moved (namespace changed) are not removals.** A moved type keeps its name and members;
> the fix is a `using`-directive change, not a rewrite. Do not treat a moved type as deleted.

- **System.Security.Cryptography.Pkcs**: no source-breaking public API changes detected — see [`apidiff/System.Security.Cryptography.Pkcs.apidiff.md`](apidiff/System.Security.Cryptography.Pkcs.apidiff.md).
- **System.Security.Cryptography.Xml**: no source-breaking public API changes detected — see [`apidiff/System.Security.Cryptography.Xml.apidiff.md`](apidiff/System.Security.Cryptography.Xml.apidiff.md).
- **Microsoft.AspNetCore.Mvc.ViewFeatures**: no source-breaking public API changes detected — see [`apidiff/Microsoft.AspNetCore.Mvc.ViewFeatures.apidiff.md`](apidiff/Microsoft.AspNetCore.Mvc.ViewFeatures.apidiff.md).
- **Microsoft.EntityFrameworkCore.Abstractions**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.Abstractions.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.Abstractions.apidiff.md).
- **Microsoft.Extensions.Configuration**: no source-breaking public API changes detected — see [`apidiff/Microsoft.Extensions.Configuration.apidiff.md`](apidiff/Microsoft.Extensions.Configuration.apidiff.md).
- **Microsoft.Extensions.Configuration.Json**: no source-breaking public API changes detected — see [`apidiff/Microsoft.Extensions.Configuration.Json.apidiff.md`](apidiff/Microsoft.Extensions.Configuration.Json.apidiff.md).
- **Microsoft.Extensions.Identity.Core**: no source-breaking public API changes detected — see [`apidiff/Microsoft.Extensions.Identity.Core.apidiff.md`](apidiff/Microsoft.Extensions.Identity.Core.apidiff.md).
- **Microsoft.EntityFrameworkCore**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.apidiff.md).
- **Microsoft.EntityFrameworkCore.Design**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.Design.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.Design.apidiff.md).
- **Microsoft.EntityFrameworkCore.Proxies**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.Proxies.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.Proxies.apidiff.md).
- **Microsoft.EntityFrameworkCore.Relational**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.Relational.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.Relational.apidiff.md).
- **Microsoft.EntityFrameworkCore.Tools**: no source-breaking public API changes detected — see [`apidiff/Microsoft.EntityFrameworkCore.Tools.apidiff.md`](apidiff/Microsoft.EntityFrameworkCore.Tools.apidiff.md).
- **Microsoft.AspNetCore.Http.Abstractions**: no source-breaking public API changes detected — see [`apidiff/Microsoft.AspNetCore.Http.Abstractions.apidiff.md`](apidiff/Microsoft.AspNetCore.Http.Abstractions.apidiff.md).
- **Microsoft.AspNetCore.Authentication.JwtBearer**: no source-breaking public API changes detected — see [`apidiff/Microsoft.AspNetCore.Authentication.JwtBearer.apidiff.md`](apidiff/Microsoft.AspNetCore.Authentication.JwtBearer.apidiff.md).
- **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation**: no source-breaking public API changes detected — see [`apidiff/Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.apidiff.md`](apidiff/Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.apidiff.md).

## Breaking-change findings

- Version divergence findings (Pkg.0003): 1
- Requested-version-unsupported findings (Pkg.0002): 0

- Quick mode does not scan source, so there are no per-line `PkgApi` usage findings. Review the
  per-package API diffs above and rely on build errors during execution to pinpoint affected code.
- A full code scan can locate the exact source location of every breaking-change usage across the repo.
  It is opt-in and slower — re-run the assessment with `fullScan=true` only if the user requests it.

## Next steps

1. **Resolve version divergence** — the scoped projects cannot all move to one version. Where a project shows `no supported version`, nothing published supports its target framework(s), so raising that project's framework or dropping it from the scope is the reconciliation choice; otherwise pick a common version in planning.
2. Proceed to planning to triage the API changes above and plan the code fixes.

