#!/usr/bin/env bash
set -euo pipefail
# ────────────────────────────────────────────────
# rebase-and-deploy.sh
# Keeps <feature_branch> in sync with origin/master,
# then triggers Terraform apply (or any deploy step).
# ────────────────────────────────────────────────

FEATURE_BRANCH=${1:-infra-update}     # fallback branch name
REMOTE=${2:-origin}

main() {
  echo "🔄  Fetching latest refs from $REMOTE …"
  git fetch "$REMOTE"

  echo "⏩  Updating local master with $REMOTE/master"
  git checkout master
  git reset --hard "$REMOTE/master"

  echo "🔀  Rebasing $FEATURE_BRANCH onto master …"
  git checkout "$FEATURE_BRANCH"
  git rebase master          # or: git pull --rebase $REMOTE master

  echo "📤  Pushing $FEATURE_BRANCH (force-with-lease) …"
  git push --force-with-lease demo "$FEATURE_BRANCH"

  echo "✅  Push done – GitHub Actions will now run .github/workflows/main.yml"
}

main "$@"