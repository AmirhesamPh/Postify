namespace Postify.Abstractions;

public static class Apis
{
    public static class Accounts
    {
        public const string Route = "/accounts";

        public static class Endpoints
        {
            public const string SignUp = "SignUp";
            public const string SignIn = "SignIn";
        }
    }

    public static class Posts
    {
        public const string Route = "/posts";

        public static class Endpoints
        {
            public const string Create = "CreatePost";
            public const string GetAll = "GetAll";
            public const string Update = "UpdatePost";
            public const string Delete = "DeletePost";
            public const string SetPostStatus = "SetPostStatus";
        }
    }
}
