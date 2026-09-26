# Partas.Skills

Claude Code skills for Partas F# projects, published as the `partas` plugin marketplace.

## Install

```shell
/plugin marketplace add <path-or-git-url-of-this-repo>
/plugin install fsharp-xml-docs@partas
```

## Skills

| Plugin | What it does |
| --- | --- |
| `fsharp-xml-docs` | Writing, auditing and fixing F# `///` XML documentation: tag set, density budget, all-or-none params, `<code lang>`, `<include>`, and an FCS-based audit (`scripts/audit.fsx`, or a SageFs session for millisecond re-runs). |

`fsharp-xml-docs` also ships `assets/xml-docs-rules.md`, a short rules file to copy into a
project's `.claude/rules/` so the core conventions are loaded whenever code is written.

## Third-party material

`plugins/fsharp-xml-docs/skills/fsharp-xml-docs/references/comment-hygiene.md` is copied from
roboz0r's [comment-hygiene](https://github.com/roboz0r/comment-hygiene) (MIT; notice in
`LICENSE-comment-hygiene`). The source commit is recorded at the top of that file. To re-sync,
diff it against upstream `skills/comment-hygiene/writing.md` and update the recorded commit.
Reviews and sweeps use roboz0r's plugin directly (`comment-hygiene@roboz0r`).

## Evals

Test prompts and assertions for each skill live in `evals/` beside it. Run results go to
`<skill>-workspace/`, which is not committed.
