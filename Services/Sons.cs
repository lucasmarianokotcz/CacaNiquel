namespace Services
{
    public static class Sons
    {
        private static readonly string pastaDefaultSounds = "../../../Sounds/";
        private static readonly string extensaoDefaultSounds = ".wav";

        public static string Win { get; } = "win";
        public static string Lost { get; } = "lost";
        public static string Error { get; } = "error";

        public static string GetSom(string som)
        {
            return pastaDefaultSounds + som + extensaoDefaultSounds;
        }
    }
}
