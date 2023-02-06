﻿using CommunityToolkit.Mvvm.Input;
using ElectionGuard.ElectionSetup;

namespace ElectionGuard.UI.ViewModels;

[QueryProperty(CurrentKeyCeremonyParam, "KeyCeremonyId")]
public partial class ViewKeyCeremonyViewModel : BaseViewModel
{
    public const string CurrentKeyCeremonyParam = "KeyCeremonyId";

    public ViewKeyCeremonyViewModel(IServiceProvider serviceProvider, KeyCeremonyService keyCeremonyService) : 
        base("ViewKeyCeremony", serviceProvider)
    {
        _keyCeremonyService = keyCeremonyService;
        IsJoinVisible = !AuthenticationService.IsAdmin;
    }

    [ObservableProperty]
    private KeyCeremony? _keyCeremony;

    [ObservableProperty]
    private bool _isJoinVisible;

    [ObservableProperty]
    private string _keyCeremonyId = string.Empty;

    partial void OnKeyCeremonyIdChanged(string value)
    {
        Task.Run(async() => KeyCeremony = await _keyCeremonyService.GetByIdAsync(value)).Wait();
    }

    [RelayCommand(CanExecute = nameof(CanJoin))]
    public void Join()
    {
        var currentGuardianUserName = AuthenticationService.UserName;


    }

    private bool CanJoin()
    {
        return KeyCeremony is not null;
    }

    private readonly KeyCeremonyService _keyCeremonyService;
}