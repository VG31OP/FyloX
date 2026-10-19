#!/bin/sh
set -eu

source_dir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd -P)
install_dir="$HOME/Applications/FyloX.app"
bin_dir="$HOME/.local/bin"
if [ -e "$install_dir" ] || [ -L "$install_dir" ]; then
    printf '%s\n' "An installation already exists at $install_dir. Run uninstall.sh first."
    exit 1
fi
if [ -e "$bin_dir/fylox" ] || [ -L "$bin_dir/fylox" ]; then
    printf '%s\n' "$bin_dir/fylox already exists. Remove or rename it before installing FyloX."
    exit 1
fi
test -x "$source_dir/FyloX.app/Contents/MacOS/FyloX"
mkdir -p "$HOME/Applications" "$bin_dir"
/usr/bin/ditto "$source_dir/FyloX.app" "$install_dir"
ln -s "$install_dir/Contents/MacOS/fylox-cli" "$bin_dir/fylox"
printf '%s\n' "Installed $install_dir" "CLI: $bin_dir/fylox (add $bin_dir to PATH if needed)."
printf '%s\n' 'Open FyloX from Applications. Grant Accessibility and Finder Automation access when requested.'
