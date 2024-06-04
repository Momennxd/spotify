using Bunifu.Framework.UI;
using Spotify_BusinessLayer;
using Spotify_PresentationLayer.Playlists;
using Spotify_PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_PresentationLayer
{
    public partial class frmMainScreen : Form
    {
        public frmMainScreen()
        {
            InitializeComponent();
            clsScene.frmMainScreen = this;
        }

        public clsSongsQueue.enMode Mode
        {
            get { if (btnShuffle.Tag.ToString() == "0")
                    return clsSongsQueue.enMode.eNormal;
                else
                    return clsSongsQueue.enMode.eShuffle;
                }
        }

        struct stCurrentFlowPanel
        {
            public static FlowLayoutPanel _OpenedFlowPanel;

            public static int PlaylistID;
        }

        void Mute(bool Mute)
        {
            if (Mute)
            {
                //mutting the volume
                clsPlayedSong.SetVolume(0);
            }
            else
            {
                clsPlayedSong.SetVolume(sliderVolumne.Value);
            }
        }

        void FocusOn(Label label)
        {
            MainLables_Click(label, EventArgs.Empty);
        }

        void ShowInnerForm(Label btn)
        {
            pnlFormsLoader.Controls.Clear();

            switch (btn.Name)
            {
                case "lblLikedSongs":
                    frmLikesSongs _frmLikesSongs = new frmLikesSongs();

                    //getting the flow panel
                    stCurrentFlowPanel._OpenedFlowPanel =
                        _frmLikesSongs.ctrlLikedSongsPlaylist1.GetSongsFLowPanel();

                    //getting the playlist ID
                    stCurrentFlowPanel.PlaylistID =
                        _frmLikesSongs.ctrlLikedSongsPlaylist1.PlaylistID;

                    clsSpotifySharedMethods.ShowFormInPanel(pnlFormsLoader, _frmLikesSongs);
                    break;




            }

        }

        void ColorLabel(Label btn, Color Clr)
        {
            btn.ForeColor = Clr;
        }

        void ColorAllMainScreenLabelsExcept(Label btn)
        {

            if (lblExplore.Name != btn.Name)
            {
                ColorLabel(lblExplore, Color.FromArgb(178, 178, 178));
                lblExplore.Tag = "0";
            }


            if (lblMadeForYou.Name != btn.Name)
            {
                ColorLabel(lblMadeForYou, Color.FromArgb(178, 178, 178));
                lblMadeForYou.Tag = "0";
            }

            if (lblLikedSongs.Name != btn.Name)
            {
                ColorLabel(lblLikedSongs, Color.FromArgb(178, 178, 178));
                lblLikedSongs.Tag = "0";
            }


            if (lblAlbums.Name != btn.Name)
            {
                ColorLabel(lblAlbums, Color.FromArgb(178, 178, 178));
                lblAlbums.Tag = "0";
            }

            if (lblArtists.Name != btn.Name)
            {
                ColorLabel(lblArtists, Color.FromArgb(178, 178, 178));
                lblArtists.Tag = "0";
            }

            if (lblPlayLists.Name != btn.Name)
            {
                ColorLabel(lblPlayLists, Color.FromArgb(178, 178, 178));
                lblPlayLists.Tag = "0";
            }
          

            if (lblArtistName.Name != btn.Name)
            {
                ColorLabel(lblArtistName, Color.FromArgb(178, 178, 178));
                lblArtistName.Tag = "0";
            }

        }

        void SytelLabels(object sender)
        {
            ((Label)sender).ForeColor = Color.White;
            ((Label)sender).Tag = "1";
            ColorAllMainScreenLabelsExcept((Label)sender);
        }

        void LabelMouseEnter(Label label)
        {
            label.ForeColor = Color.White;
        }

        void LabelMouseLeave(Label label)
        {
            label.ForeColor = Color.FromArgb(178, 178, 178);
        }

        /// <summary>
        /// this function plays or pauses the chosen song
        /// </summary>
        /// <param name="Play"></param>
        void PerformPlayPause(bool Play)
        {
            if (Play)
                clsPlayedSong.Play();
            else
                clsPlayedSong.Pause();

            
        }


        void LoadNextPrevSong(bool Next)
        {
            if (clsScene.SongsQueue.IsEmpty())
                return;

            clsSong NewSong;

            if (Next)
            {
                NewSong = clsScene.SongsQueue.SetNextSongAsPlayedInQueue();
                clsPlayedSong.SetSong(NewSong);          
            }
            else
            {
                if (clsPlayedSong.GetCurrentPosition() <= 3)
                {
                    NewSong = clsScene.SongsQueue.SetPervSongAsPlayedInQueue();
                    clsPlayedSong.SetSong(NewSong);
                }
                else
                {
                    NewSong = clsScene.SongsQueue.GetCurrentPlayedSong();
                    clsPlayedSong.Repeat();
                }
            }

            PerformPlayPause(true);
            clsSpotifySharedMethods.UpdatePlaylistUI(stCurrentFlowPanel._OpenedFlowPanel);
            UpdateSongBarUI(NewSong);


        }


        //public methods

        /// <summary>
        /// this function updates the UI of the song bar
        /// it updates the song name and the artists names and the slider..etc
        /// </summary>
        /// <param name="song"></param>
        public void UpdateSongBarUI(clsSong song)
        {
            pbSongPic.ImageLocation = song.ImagePath;
            lblSongName.Text = song.SongName;

            lblArtistName.Text = string.Join(",",
                clsSpotifySharedMethods.ConcatenateSongArtists(
                    clsStoredProsedures.GetSongArtists(song.SongID)));

            lblSongDuration.Text =
                clsSpotifySharedMethods.GetSongDurationStringFormat(song.Duration);

            clsSpotifySharedMethods.SetHeartButton(btnLikeSong,
                clsStoredProsedures.sp_DoesUserLikeSong(song.SongID, clsScene.LoggedUser.UserID));


            clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause, clsPlayedSong.IsPlayed);


        }

        public void EnableSongTimer(bool Enable)
        {
            SongTimer.Enabled = Enable;
        }

        public void ShowPlayedSongStateInPlaylist(bool Play)
        {
            clsSpotifySharedMethods.ShowSongState(stCurrentFlowPanel._OpenedFlowPanel,
                    stCurrentFlowPanel.PlaylistID, clsScene.SongsQueue.CurrentPlayedSongIndex, Play);
        }

        public void UpdateShuffleButtonUI(bool Enable)
        {
            _ = Enable == true ? btnShuffle.Tag = "1" : btnShuffle.Tag = "0";

            clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(
                   btnShuffle, Resources.icons8_shuffle_60, Resources.icons8_shuffle_60__1_1);
        }

        //events methods
        private void btnTwoStates_Click(object sender, EventArgs e)
        {
            if (((BunifuImageButton)sender).Name == "btnShuffle")
            {
                clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(
                    btnShuffle, Resources.icons8_shuffle_60, Resources.icons8_shuffle_60__1_1);
                
                clsSpotifySharedMethods.PerformShuffleSort((BunifuImageButton)sender);


            }
            else if (((BunifuImageButton)sender).Name == "btnRepeat")
                clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(
                    btnRepeat, Resources.icons8_loop_100__1_1, Resources.icons8_loop_100);

            else if (((BunifuImageButton)sender).Name == "btnPlayPause")
            {
               
                clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(btnPlayPause, 
                    Resources.icons8_play_100, Resources.icons8_pause_1001);

                //play or pause the song
                PerformPlayPause(btnPlayPause.Tag.ToString() == "1");
                //Updating the current played song UI (Play or pauses)
                ShowPlayedSongStateInPlaylist(btnPlayPause.Tag.ToString() == "1");

            }

            else if (((BunifuImageButton)sender).Name == "btnLikeSong")
                clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(btnLikeSong,
                    Resources.icons8_heart_48, Resources.icons8_heart_1001);

            else if (((BunifuImageButton)sender).Name == "btnMute")
            {
                clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(btnMute,
                    Resources.icons8_sound_48__1_1, Resources.icons8_no_audio_50);

                Mute(btnMute.Tag.ToString() == "1");
            }

        }

        private void MainLables_Click(object sender, EventArgs e)
        {
            SytelLabels((Label)sender);
            ShowInnerForm((Label)sender);
        }

        private void frmMainScreen_Load(object sender, EventArgs e)
        {
            FocusOn(lblMadeForYou);
        }

        private void MainLabels_MouseEnter(object sender, EventArgs e)
        {
            LabelMouseEnter((Label)sender);
        }

        private void MainLabels_MouseLeave(object sender, EventArgs e)
        {
            if (((Label)sender).Tag.ToString() == "1")
                return;
            LabelMouseLeave((Label)sender);
        }

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            sliderSongProgress.RangeMin = 0;
            sliderSongProgress.RangeMax = clsPlayedSong.PlayedSong.Duration;
            sliderSongProgress.Value =
                (int)clsPlayedSong.GetCurrentPosition();
     
            lblCurrentSongTime.Text =
                clsSpotifySharedMethods.GetSongDurationStringFormat(
                    (int)clsPlayedSong.GetCurrentPosition());


            //checking if the song is finished "-1" to prevent the error because the current position gets set to 
            //zero automaticly when the song is finished 
            
            if ((int)clsPlayedSong.GetCurrentPosition() >= 
                clsScene.SongsQueue.GetCurrentPlayedSong().Duration - 1)
            {
                if (btnRepeat.Tag.ToString() == "1")
                {
                    clsPlayedSong.Repeat();
                }
                else
                {
                    LoadNextPrevSong(true);
                }
            }
        }

        private void btnNextSong_Click(object sender, EventArgs e)
        {
            LoadNextPrevSong(true);
        }

        private void btnPrevSong_Click(object sender, EventArgs e)
        {
            LoadNextPrevSong(false);
        }

        private void sliderSongProgress_onValueChanged(object sender, int newValue)
        {
            clsPlayedSong.SetCurrentPosition(newValue);
        }

        private void materialSlider1_onValueChanged(object sender, int newValue)
        {
            clsPlayedSong.SetVolume(sliderVolumne.Value);
        }
    }
}
