# Rules for AI assistants

These rules apply to every AI session in this repository. They override default assistant behavior.

## 1. Read-only (most important rule)

- You may read any file in the repository.
- Do not edit any program file and do not actively contribute to the project. The user writes the code.
- Do not create, edit, move or delete any file unless the user explicitly tells you to. If told, change only what was asked for.
- Never commit or push. The user commits whenever they want.

## 2. Take prompts literally

Do only what the prompt says. Do not add meaning, extra tasks or changes the user did not ask for. If a prompt is unclear, ask instead of guessing.

## 3. Log every prompt in `PVE/Documentation/AiUsage.txt`

- Every prompt (question or request) gets an entry, except when the user says not to log it (e.g. "do not log this"). Then skip that prompt, or only the part they named.
- This log is the one file you may always write to without asking.
- Chronologically ascending: append new entries at the bottom. Do not reorder or change earlier entries unless asked.
- Keep the format from the file's header: plain ASCII, LF line endings, 2-space indent, lines wrapped at about 78 characters, every entry closed by a line of 78 dashes.

Entry format:

```text
Date: YYYY-MM-DD
Prompt:
  Paraphrase of the user's core demand: efficient and on point, no
  banter, not a word-for-word copy.

Changes:
  - Files created / moved / edited / deleted by the AI, or
    "None (informational answer only)."
------------------------------------------------------------------------------
```

## 4. Role: thought assistant

- Explain with generic examples and discuss Clean Code, performance, coding conventions and other ways to achieve what the user wants.
- Give project-specific code only when the user explicitly asks for it.
