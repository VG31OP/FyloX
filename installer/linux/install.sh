#!/bin/sh
set -eu

source_dir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd -P)
data_dir=${XDG_DATA_HOME:-"$HOME/.local/share"}
install_dir="$data_dir/fylox/application"
bin_dir="$HOME/.local/bin"
desktop_file="$data_dir/applications/com.fylox.app.desktop"
if [ -e "$install_dir" ] || [ -L "$install_dir" ]; then
    printf '%s\n' "An installation already exists at $install_dir. Run its uninstall.sh first."
    exit 1
fi
if [ -e "$bin_dir/fylox" ] || [ -L "$bin_dir/fylox" ]; then
    printf '%s\n' "$bin_dir/fylox already exists. Remove or rename it before installing FyloX."
    exit 1
fi
if [ -e "$desktop_file" ] || [ -L "$desktop_file" ]; then
    printf '%s\n' "$desktop_file already exists. Remove or rename it before installing FyloX."
    exit 1
fi
command -v python3 >/dev/null
test -x "$source_dir/FyloX"
test -x "$source_dir/fylox-cli"
mkdir -p "$data_dir/fylox" "$bin_dir" "$data_dir/applications"
cp -R "$source_dir" "$install_dir"
printf '%s\n' 'com.fylox.app' > "$install_dir/.fylox-package"
ln -s "$install_dir/fylox-cli" "$bin_dir/fylox"
python3 - "$install_dir" "$desktop_file" <<'PY'
import pathlib
import sys

app, desktop = map(pathlib.Path, sys.argv[1:])
def quoted_exec(value):
    value = value.replace('%', '%%')
    for character in ('\\', '"', '`', '$'):
        value = value.replace(character, '\\' + character)
    return '"' + value.replace('\\', '\\\\').replace('\n', '\\n').replace('\r', '\\r') + '"'

def desktop_value(value):
    return str(value).replace('\\', '\\\\').replace('\n', '\\n').replace('\r', '\\r')

desktop.write_text(
    '[Desktop Entry]\nType=Application\nName=FyloX\n'
    'Comment=Convert files with FyloX\n'
    f'Exec={quoted_exec(str(app / "FyloX"))} --convert %F\n'
    f'Icon={desktop_value(app / "fylox.png")}\n'
    'Terminal=false\nCategories=Utility;\nX-FyloX-Managed=true\n', encoding='utf-8')
PY
if command -v update-desktop-database >/dev/null 2>&1; then
    update-desktop-database "$data_dir/applications"
fi
printf '%s\n' "Installed FyloX at $install_dir" "CLI: $bin_dir/fylox (add $bin_dir to PATH if needed)."
