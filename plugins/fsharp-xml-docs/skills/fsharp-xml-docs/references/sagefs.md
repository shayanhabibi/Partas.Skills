# Audit loop in SageFs

The SageFs host already has FSharp.Compiler.Service loaded; reference that same file rather than
a NuGet copy, then load the audit once and re-run it after each edit:

```fsharp
let fcs =
    System.AppDomain.CurrentDomain.GetAssemblies()
    |> Array.find (fun a -> a.GetName().Name = "FSharp.Compiler.Service")
fcs.Location;;
// #r the printed path, then:
#load "<skill dir>/scripts/XmlDocAudit.fsx";;
XmlDocAudit.audit true [ @"<path>" ] |> XmlDocAudit.report;;
XmlDocAudit.docListing @"<file>";;                  // what --docs prints
XmlDocAudit.codeChanges @"<original>" @"<edited>";; // what --against adds
```

A whole repository audits in well under a second on a warm session.
`XmlDocAudit.auditWith compiler true paths` includes generated files. If the host's FCS version
differs from 43.13 and the load fails with syntax-tree pattern errors, fall back to the CLI.
