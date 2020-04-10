namespace Services
{
    public static class Imagens
    {
        private static readonly string pastaDefaultBitmaps = "../../../Bitmaps/";
        private static readonly string extensaoDefaultBitmaps = ".jpg";

        public static string Img7 { get; } = "7";
        public static string Banana { get; } = "banana";
        public static string BigWin { get; } = "big_win";

        public static string GetImagem(string imagem)
        {
            return pastaDefaultBitmaps + imagem + extensaoDefaultBitmaps;
        }
    }
}
