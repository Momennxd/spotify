using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spotify_BusinessLayer;
using WMPLib;


namespace Spotify_PresentationLayer
{
    public static class clsPlayedSong
    {

        private static WMPLib.WindowsMediaPlayer _Player = new WMPLib.WindowsMediaPlayer();

        private static clsSong _PlayedSong = new clsSong();
        public static clsSong PlayedSong { get { return _PlayedSong; } }

        private static bool _IsPlayedFirstTime = true;

        public static bool IsPlayedFirstTime { get { return _IsPlayedFirstTime; } }

        private static bool _IsPlayed = false;
        public static bool IsPlayed { get { return _IsPlayed; } }

        public static int GetVolume()
        {
           return _Player.settings.volume;
        }

        public static void SetVolume(int Volume)
        {
            if (Volume >= 0)
                _Player.settings.volume = Volume;
        }

        public static double GetCurrentPosition()
        {
            return _Player.controls.currentPosition;
        }

        public static void SetCurrentPosition(double NewPosition)
        {
            if (NewPosition >= 0)
                _Player.controls.currentPosition = NewPosition;
        }

        public static void SetSong(clsSong song)
        {
            _PlayedSong = song;

            _IsPlayedFirstTime = true;

        }

        public static void Play()
        {
            if (_PlayedSong == null)
                return;

           

            if (_IsPlayedFirstTime)
            {
                //pausing the played song to play the new one
                if (!string.IsNullOrEmpty(_Player.URL))
                    _Player.controls.stop();

                _Player.URL = _PlayedSong.SongPath;
                _IsPlayedFirstTime = false;
                _Player.controls.play();           
            }
            else
            {
                _Player.controls.play();
            }

            _IsPlayed = true;
        }

        public static void Pause()
        {
            if (_PlayedSong == null)
                return;

            _Player.controls.pause();
            _IsPlayed = false;

        }

        public static void Stop()
        {
            _Player.controls.stop();
            _IsPlayed = false;

        }

        public static void Repeat()
        {
            _Player.controls.currentPosition = 0;
        }

        public static double GetDuration()
        {
            return _Player.controls.currentItem.duration;
        }
    }
}
