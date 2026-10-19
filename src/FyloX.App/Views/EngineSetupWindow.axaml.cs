using Avalonia.Controls;
using FyloX.App.Services;
using FyloX.App.ViewModels;

namespace FyloX.App.Views;

public partial class EngineSetupWindow : Window
{
    public EngineSetupWindow()
    {
        InitializeComponent();
        Motion.Track(this);
        DataContextChanged += (_, _) =>
        {
            if (DataContext is EngineSetupViewModel vm)
                vm.CloseRequested += (_, _) => Close();
        };
    }
}
