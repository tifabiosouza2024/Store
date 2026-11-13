# NuGet Package Upgrade Plan

## Overview

**Target**: Corrigir vulnerabilidades conhecidas de pacotes NuGet sem upgrades indiscriminados, mantendo compatibilidade com .NET 10.
**Scope**: Quatro projetos .NET 10, com uma dependência transitiva vulnerável compartilhada.

## Tasks

### 01-fix-webutilities-vulnerability: Corrigir Microsoft.AspNetCore.WebUtilities vulnerável

Atualizar a referência direta `Microsoft.AspNetCore.Mvc.ViewFeatures` em `FS.Store.Model` de `2.3.13` para `2.3.14` e adicionar nesse mesmo projeto uma referência direta a `Microsoft.AspNetCore.WebUtilities` `2.3.14`. A primeira alteração mantém a cadeia ASP.NET Core 2.3.x atualizada; a segunda é necessária porque o restore confirmou que `ViewFeatures` 2.3.14 ainda resolve `WebUtilities` 2.3.12, que permanece vulnerável (GHSA-6527-9fp8-r4x6). Não alterar pacotes não vulneráveis nem adotar versões 11.x. A referência direta deve propagar a versão corrigida aos projetos consumidores Model, DAL e Core.

**Done when**: O pacote vulnerável não aparece em `dotnet list package --vulnerable --include-transitive` nos quatro projetos; a restauração e o build da solução concluem sem erros nem avisos; os testes relevantes disponíveis passam; nenhuma referência não relacionada é alterada; a vulnerabilidade GHSA-6527-9fp8-r4x6 fica corrigida ou explicitamente documentada se não puder ser eliminada.
