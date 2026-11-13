# NuGet Package Upgrade

## Strategy
Avaliar todos os PackageReference diretos dos quatro projetos e atualizar somente pacotes vulneráveis para versões estáveis compatíveis com .NET 10.

## Preferences
- **Flow Mode**: Automatic
- **Commit Strategy**: Manual
- **Target Framework**: net10.0
- **Package Scope**: Somente pacotes vulneráveis
- **Source Control**: Permanecer na branch `develop` e preservar alterações locais existentes

## Decisions
- Não atualizar pacotes não vulneráveis nem fazer upgrades indiscriminados.
- Não criar branch nem fazer stash ou commit das alterações locais existentes.
- Vulnerabilidade identificada: `Microsoft.AspNetCore.WebUtilities` 2.3.11, depois 2.3.12 após a atualização de ViewFeatures, GHSA-6527-9fp8-r4x6, severidade Moderate, transitiva em Core, Model e DAL.
- Correção planejada: atualizar `Microsoft.AspNetCore.Mvc.ViewFeatures` de 2.3.13 para 2.3.14 e adicionar `Microsoft.AspNetCore.WebUtilities` 2.3.14 diretamente em `FS.Store.Model`, permitindo a resolução segura da cadeia ASP.NET Core 2.3.x.
- Não selecionar recomendações 11.x do assessment, pois o objetivo é a menor alteração compatível com .NET 10.

## Custom Instructions
