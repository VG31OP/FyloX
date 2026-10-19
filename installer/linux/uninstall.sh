#!/bin/sh
set -eu

data_dir=${XDG_DATA_HOME:-"$HOME/.local/share"}
install_dir="$data_dir/fylox/application"
desktop_file="$data_dir/applications/com.fylox.app.desktop"
python3 - "$data_dir" "$install_dir" <<'PY'
import pathlib
import sys

root, target = map(pathlib.Path, sys.argv[1:])
expected = root.resolve() / 'fylox' / 'application'
if target.is_symlink() or target.resolve() != expected:
    raise SystemExit('Refusing to remove an installation outside its expected directory.')
if (target / '.fylox-package').read_text(encoding='utf-8').strip() != 'com.fylox.app':
    raise SystemExit('The directory is not an installation created by FyloX.')
PY
"$install_dir/FyloX" --uninstall-cleanup
if [ -L "$HOME/.local/bin/fylox" ] && [ "$(readlink "$HOME/.local/bin/fylox")" = "$install_dir/fylox-cli" ]; then
    rm -- "$HOME/.local/bin/fylox"
fi
if [ -f "$desktop_file" ] && grep -q '^X-FyloX-Managed=true$' "$desktop_file"; then
    rm -- "$desktop_file"
fi
rm -rf -- "$install_dir"
printf '%s\n' 'FyloX was removed. Your settings and conversion results were kept.'
