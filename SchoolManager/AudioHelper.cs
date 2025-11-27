using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public static class AudioHelper
    {
        // Phát âm thanh đúng
        public static void PlayCorrect()
        {
            try
            {
                // Properties.Resources.correct trả về Stream của file wav
                using (SoundPlayer player = new SoundPlayer(Properties.Resources.correct))
                {
                    player.Play();
                }
            }
            catch { /* Bỏ qua lỗi nếu không tìm thấy file để game không bị crash */ }
        }

        // Phát âm thanh sai
        public static void PlayWrong()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(Properties.Resources.wrong))
                {
                    player.Play();
                }
            }
            catch { }
        }
    }
}
