#!/bin/sh
set -eu

install_dir="$HOME/Applications/FyloX.app"
if [ -L "$install_dir" ] || [ ! -d "$install_dir" ]; then
    printf '%s\n' "No ordinary FyloX app bundle was found at $install_dir."
    exit 1
fi
resolved_parent=$(CDPATH= cd -- "$HOME/Applications" && pwd -P)
resolved_app=$(CDPATH= cd -- "$install_dir" && pwd -P)
if [ "$resolved_app" != "$resolved_parent/FyloX.app" ]; then
    printf '%s\n' 'Refusing to remove an app outside the expected Applications directory.'
    exit 1
fi
bundle_id=$(/usr/libexec/PlistBuddy -c 'Print :CFBundleIdentifier' "$install_dir/Contents/Info.plist")
if [ "$bundle_id" != 'com.fylox.app' ]; then
    printf '%s\n' 'The bundle is not FyloX.'
    exit 1
fi
"$install_dir/Contents/MacOS/FyloX" --uninstall-cleanup
if [ -L "$HOME/.local/bin/fylox" ] && [ "$(readlink "$HOME/.local/bin/fylox")" = "$install_dir/Contents/MacOS/fylox-cli" ]; then
    rm -- "$HOME/.local/bin/fylox"
fi
rm -rf -- "$install_dir"
printf '%s\n' 'FyloX was removed. Your settings and conversion results were kept.'
