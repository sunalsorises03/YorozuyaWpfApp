﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yorozuya.WpfApp.Models;

namespace Yorozuya.WpfApp.Servcies.Contracts;

public interface IPostService
{
    public Task<IEnumerable<Post>?> GetPostsByFieldAsync(string field);

    public Task<IEnumerable<Reply>?> GetPostRepliesAsync(Post post);

    public Task<IEnumerable<Post>?> GetUserPostsAsync(string token);

    public Task<IEnumerable<Reply>?> GetUserRepliesAsync(string token);

    public Task<bool> GetIsLikedAsync(Reply reply);

    //public bool GetIsUserPost(Post post);

    //public bool GetIsUserReply(Reply reply);

    public Task AcceptReplyAsync(Post post, Reply reply);

    public Task<Reply> ReplyPostAsync(Post post, string replyContent);

    public Task LikeAsync(Reply reply);

    public Task CancelLikeAsync(Reply reply);

    public Task DeletePostAsync(Post post);

    public Task DeleteReplyAsync(Reply reply);

}
