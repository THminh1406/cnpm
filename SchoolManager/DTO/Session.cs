namespace SchoolManager
{
    public static class Session
    {
        public static int CurrentTeacherId { get; set; } = 0;
        public static string CurrentUserRole { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
    }
}
