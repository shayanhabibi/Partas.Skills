// dotnet fsi audit.fsx [--compiler] [--check] <file-or-dir>...
//   --compiler  also report FS3390 (malformed XML, unknown or undocumented <param> names)
//   --check     exit 1 when anything is found, for a pre-commit gate
//   --include-generated  also audit generated files found under a directory
//   --docs      print every doc block with its lines and declaration instead of auditing
//   --against <original>  with one edited file: also report code lines that differ from <original>
// The FCS version must match the syntax tree XmlDocAudit.fsx was written against.

#r "nuget: FSharp.Compiler.Service, 43.13.101-rc1.26425.128"
#load "XmlDocAudit.fsx"

let args = fsi.CommandLineArgs |> Array.skip 1 |> List.ofArray
let rec split flags against paths = function
    | "--against" :: original :: rest -> split flags (Some original) paths rest
    | a :: rest when a.StartsWith "--" -> split (a :: flags) against paths rest
    | p :: rest -> split flags against (p :: paths) rest
    | [] -> flags, against, List.rev paths
let flags, against, paths = split [] None [] args
let has flag = List.contains flag flags

if has "--docs" then
    XmlDocAudit.sourcesUnder (has "--include-generated") paths
    |> List.iter (XmlDocAudit.docListing >> printfn "%s")
else
    let changes =
        match against, paths with
        | Some original, [ edited ] -> XmlDocAudit.codeChanges original edited
        | Some _, _ -> failwith "--against takes exactly one edited file"
        | None, _ -> []
    let findings = changes @ XmlDocAudit.auditWith (has "--compiler") (has "--include-generated") paths
    printfn "%s" (XmlDocAudit.report findings)
    if against.IsSome && changes.IsEmpty then printfn "comment-only: no code lines differ from %s" against.Value
    if has "--check" && not findings.IsEmpty then exit 1
