namespace Postify.Abstractions;

public static partial class Apis
{
    public static partial class Accounts
    {
        public const string Route = "/accounts";

        public static partial class Endpoints
        {
            public const string SignUp = "SignUp";
            public const string SignIn = "SignIn";
        }
    }

    public static partial class Posts
    {
        public const string Route = "/posts";

        public static partial class Endpoints
        {
            public const string Create = "CreatePost";
            public const string GetAll = "GetAll";
            public const string Update = "UpdatePost";
            public const string Delete = "DeletePost";
            public const string SetPostStatus = "SetPostStatus";
        }
    }
}
