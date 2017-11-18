namespace Common.Utils
{
    public static class CurrentUserHolder
    {
        public static string CurrentUser { get; set; }

        public static bool IsAuthorized => !string.IsNullOrEmpty(CurrentUser);

        public static string GetCurrentUserName()
        {
            return IsAuthorized ? CurrentUser : "Unauthorized";
        }
    }
}
