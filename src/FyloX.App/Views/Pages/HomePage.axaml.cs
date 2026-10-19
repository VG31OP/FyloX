using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using FyloX.App.Services;
using Microsoft.Extensions.Logging;

namespace FyloX.App.Views.Pages;

public partial class HomePage : UserControl
{
    /// <summary>
    /// The first drag over the drop zone of this run is logged: a log without it after "drops don't work" means the
    /// drag never reached FyloX (Windows refuses drops from Explorer into a FyloX running as administrator).
    /// </summary>
    private static int _dragEnterLogged;

    public HomePage()
    {
        InitializeComponent();
        DragDrop.AddDragEnterHandler(DropZone, (_, e) =>
        {
            DropZone.Classes.Add("over");
            var files = DraggedFiles.Offered(e.DataTransfer);
            e.DragEffects = files ? DragDropEffects.Copy : DragDropEffects.None;
            if (Interlocked.Exchange(ref _dragEnterLogged, 1) == 0)
                AppHost.Get<ILogger<HomePage>>().LogInformation("First drag over the drop zone (files: {Files})", files);
        });
        DragDrop.AddDragOverHandler(DropZone, (_, e) =>
            e.DragEffects = DraggedFiles.Offered(e.DataTransfer) ? DragDropEffects.Copy : DragDropEffects.None);
        DragDrop.AddDragLeaveHandler(DropZone, (_, _) => DropZone.Classes.Remove("over"));
        DragDrop.AddDropHandler(DropZone, OnDrop);
    }

    private void OnDrop(object? sender, DragEventArgs e)
    {
        DropZone.Classes.Remove("over");
        var files = DraggedFiles.Local(e.DataTransfer);
        if (files.Count == 0)
            return;

        e.DragEffects = DragDropEffects.Copy;
        var screenPoint = this.PointToScreen(e.GetPosition(this));
        AppHost.Get<RadialController>().ShowForFiles(screenPoint.X, screenPoint.Y, files);
    }

    private async void OnBrowseFiles(object? sender, RoutedEventArgs e) => await BrowseFilesAsync();

    private async void OnDropZonePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && e.Source is not Button)
            await BrowseFilesAsync();
    }

    private async Task BrowseFilesAsync()
    {
        if (TopLevel.GetTopLevel(this) is not { } top)
            return;

        var storageFiles = await top.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Files to Convert with FyloX",
            AllowMultiple = true
        });

        if (storageFiles is not { Count: > 0 })
            return;

        var paths = storageFiles
            .Select(f => f.TryGetLocalPath() ?? f.Path.LocalPath)
            .Where(p => !string.IsNullOrEmpty(p))
            .ToList();

        if (paths.Count == 0)
            return;

        var center = new Point(DropZone.Bounds.Width / 2, DropZone.Bounds.Height / 2);
        var screenPoint = DropZone.PointToScreen(center);
        AppHost.Get<RadialController>().ShowForFiles(screenPoint.X, screenPoint.Y, paths);
    }
}
