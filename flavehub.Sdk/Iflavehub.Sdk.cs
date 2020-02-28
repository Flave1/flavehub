using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using flavehub.Contracts.RequestObjs;
using Refit; 

namespace flavehub.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IflavehubApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<PostResponse>> GetAllAsync();

        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> GetAsync(int postId);

        [Post("/api/v1/posts")]
        Task<ApiResponse<PostResponse>> CreateAsync([Body] AddPostReqObj createPostRequest);

        [Put("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> UpdateAsync(int postId, [Body] EditPostReqObj updatePostRequest);

        [Delete("/api/v1/posts/{postId}")]
        Task<ApiResponse<string>> DeleteAsync(int postId);
    }
}