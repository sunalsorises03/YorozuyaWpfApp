﻿using System.Threading.Tasks;
using System.Windows;
using Yorozuya.WpfApp.ViewModels.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Yorozuya.WpfApp.Models;
using System.ComponentModel;

namespace Yorozuya.WpfApp.Views.Windows;

/// <summary>
/// QuestionWindow.xaml 的交互逻辑
/// </summary>
public partial class PostWindow : UiWindow
{
    public PostWindow(PostWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = this;
        ViewModel = viewModel;
        ViewModel.GetCancelConfirmDialogService().SetDialogControl(Dialog);
        ViewModel.GetSnackbarService().SetSnackbarControl(Snackbar);
        ViewModel.WindowOpened += (sender, e) =>
        {
            Show();
            Focus();
        };
    }

    public PostWindowViewModel ViewModel { get; }

    void OnMainWindowButtonClicked(object sender, RoutedEventArgs e)
    {
        var mainWindow = App.Current.ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        if (mainWindow.WindowState == WindowState.Minimized)
            mainWindow.WindowState = WindowState.Normal;
        mainWindow.Focus();
    }

    async void OnCopyButtonClickedAsync(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(ViewModel.CurrentReply!.Content))
            return;
        Wpf.Ui.Common.Clipboard.SetText(ViewModel.CurrentReply.Content);
        CopyButton.Icon = SymbolRegular.Checkmark24;
        CopyButton.Appearance = ControlAppearance.Success;
        await Task.Delay(3000);
        CopyButton.Icon = SymbolRegular.Copy24;
        CopyButton.Appearance = ControlAppearance.Transparent;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        Hide();
        e.Cancel = true;
    }
}
