﻿using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Yorozuya.WpfApp.Models;
using Yorozuya.WpfApp.Servcies.Contracts;

namespace Yorozuya.WpfApp.ViewModels.Pages;

public partial class PersonPageViewModel : BaseViewModel
{
    private readonly IUserService _userService;
    private readonly IPostService _postService;
    private readonly IMessenger _messenger;

    [ObservableProperty] private UserInfo? _nowUserInfo;

    [ObservableProperty] private List<Post> _postSource = new();

    [ObservableProperty] private List<Reply> _replySource = new();

    [RelayCommand]
    private void OpenPost(Post post)
    {
        _messenger.Send(post);
    }

    [RelayCommand]
    private void OpenReply(Reply reply)
    {
        //_messenger.Send(reply);
    }

    private async void SetActionCard()
    {
        var posts = await _postService.GetUserPostsAsync(_userService.Token);
        var replies = await _postService.GetUserRepliesAsync(_userService.Token);
        PostSource = posts?.ToList() ?? new();
        ReplySource = replies?.ToList() ?? new();
    }

    public PersonPageViewModel(IUserService userService, IPostService postService, IMessenger messenger)
    {
        _userService = userService;
        _postService = postService;
        _messenger = messenger;
        NowUserInfo = userService.UserInfo;
        SetActionCard();
    }
}
