using System;
using System.Collections.Generic; 

namespace flavehub.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version; 

        public static class Posts
        {
            public const string GetAll = Base + "/post/getAllposts";
            public const string Get = Base + "/post/getPost/{postId}";
            public const string Update = Base + "/post/updatePost/{postId}";
            public const string Delete = Base + "/post/deletePost/{postId}";
            public const string Create = Base + "/post/createPost";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string RefreshToken = Base + "/identity/refresh";
        }


        public static class Comment
        {
            public const string GetAll = Base + "/comment/getAllComments";
            public const string Get = Base + "/comment/getComment/{comentId}";
            public const string Update = Base + "/comment/updateComment/{commentId}";
            public const string Delete = Base + "/comment/deleteComment/{commentId}";
            public const string Create = Base + "/comment/createComment";
        }

        public static class Category
        {
            public const string GetAll = Base + "/category/getAllCategories";
            public const string Get = Base + "/category/getcategory/{categoryId}";
            public const string Update = Base + "/category/updateCategory/{categoryId}";
            public const string Delete = Base + "/category/deleteCategory/{categoryId}";
            public const string Create = Base + "/category/createCategory";
        }

        public static class Social
        {
            public const string Github = Base + "/github/api/details"; 
        }
    }
}
