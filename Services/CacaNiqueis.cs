using System;
using System.Collections.Generic;
using System.Drawing;

namespace Services
{
    public class CacaNiqueis
    {
        #region Properties

        public int MoedasIniciais { get; } = 100;
        public int MoedasUser { get; set; } = 100;
        public int Aposta { get; set; } = 0;
        public int FatorRendimento { get; } = 10;
        public Dictionary<string, Bitmap> ImagensLoad { get; set; }
        public Dictionary<string, string> SonsLoad { get; set; }
        public Dictionary<int, Bitmap> ImagensArray { get; set; }

        #endregion

        #region Public Methods

        public Dictionary<string, Bitmap> LoadImagens()
        {
            ImagensLoad = new Dictionary<string, Bitmap>();
            ImagensArray = new Dictionary<int, Bitmap>();
            int i = 0;

            try
            {
                ImagensLoad[Imagens.Img7] = new Bitmap(Imagens.GetImagem(Imagens.Img7));
                ImagensLoad[Imagens.Banana] = new Bitmap(Imagens.GetImagem(Imagens.Banana));
                ImagensLoad[Imagens.BigWin] = new Bitmap(Imagens.GetImagem(Imagens.BigWin));

                foreach (Bitmap imagem in ImagensLoad.Values)
                {
                    ImagensArray[i] = imagem;
                    i++;
                }
            }
            catch (Exception)
            {
                ImagensLoad = null;
            }

            return ImagensLoad;
        }

        public Dictionary<string, string> LoadSounds()
        {
            SonsLoad = new Dictionary<string, string>();

            try
            {
                SonsLoad[Sons.Win] = Sons.GetSom(Sons.Win);
                SonsLoad[Sons.Lost] = Sons.GetSom(Sons.Lost);
                SonsLoad[Sons.Error] = Sons.GetSom(Sons.Error);
            }
            catch (Exception)
            {
                SonsLoad = null;
            }

            return SonsLoad;
        }

        public bool Apostar(int quantidade)
        {
            if (IsValidAposta(quantidade))
            {
                Aposta = quantidade;
                MoedasUser -= quantidade;
                return true;
            }

            return false;
        }

        public int Ganhou()
        {
            return Aposta * FatorRendimento;
        }

        public int MoedasAtualizadas()
        {
            MoedasUser += Aposta * FatorRendimento;
            return MoedasUser;
        }

        #endregion

        #region Private Methods

        private bool IsValidAposta(int quantidade)
        {
            if (MoedasUser - quantidade >= 0)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
