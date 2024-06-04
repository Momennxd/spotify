using Bunifu.Framework.UI;
using Spotify_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Spotify_PresentationLayer.Controls.ctrlSong;

namespace Spotify_PresentationLayer.Controls
{
    public partial class ctrlLikedSongsPlaylist : UserControl
    {
        public ctrlLikedSongsPlaylist()
        {
            InitializeComponent();

            //Checking if this playlist is played and updating the UI of the play-pause button
            if (clsScene.SongsQueue.PlaylistID == this.PlaylistID)
            {
                clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause,
                   clsPlayedSong.IsPlayed);             
            }
        }


        /// <summary>
        /// zero is the const liked songs playlist ID
        /// </summary>
        
        private const int _PlaylistID = 0;
        public int PlaylistID { get { return _PlaylistID; } }

        public static ctrlSong CurrentPlayedSongControl { get; set; }


        private void PlayPause_Click(object sender, PlayPauseEventArgs eventArgs)
        {
            //setting the UI of the current PlayPause button 
            clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause,
              eventArgs.IsPlay);

            CurrentPlayedSongControl = (ctrlSong)sender;

            clsSpotifySharedMethods.UpdatePlaylistUI(fpnlSongs);

            //checking if the played song is a part of the current played playlist in the queue
            //if it is not then the queue gets filled with the new playlist
            if (PlaylistID != clsScene.SongsQueue.PlaylistID)
                _AddLikedSongsToQueue((ctrlSong)sender);
        }


        private void _AddLikedSongsToQueue(ctrlSong PlayedSongControl)
        {
            if (clsScene.LoggedUser == null)
                return;

            //setting the playlist ID to to LikedSongsPlaylistID which is zero as const.
            clsScene.SongsQueue.PlaylistID = _PlaylistID;

            DataTable dtLikedSongs = clsLikedSong.GetAllRowsByPersonID(clsScene.LoggedUser.PersonID);

            clsScene.SongsQueue.AddSongs(ref dtLikedSongs, PlayedSongControl.SongIndexInPlaylist, clsScene.frmMainScreen.Mode);


            //setting the current played song control based on the songs queue played song index
            CurrentPlayedSongControl =
                    (ctrlSong)fpnlSongs.Controls[clsScene.SongsQueue.CurrentPlayedSongIndex];


        }

        /*
        void DisplaySong(ref clsSong song, int SongIndexInPlaylist, int PlaylistID)
        {
            ctrlSong SongControls = new ctrlSong(song, SongIndexInPlaylist, PlaylistID);
            SongControls.DisplaySongData(song);

            //##TODO adding the events
            SongControls.PlayPauseClick += PlayPause_Click;

            //adding to the current played playlist
            //clsScene.CurrentPlaylist.Add(ctrlSong);
            fpnlSongs.Controls.Add(SongControls);
        }
        */

        void DisplaySongsOnPnl(int UserID)
        {
            if (UserID > 0)
            {
                clsUser user = clsUser.FindByUserID(UserID);
                DataTable dtLikedSongs = clsSong.GetLikedSongsByPerson(user.PersonID);

                //testing speed
                //for (int i = 0; i < 8; i++)
                {

                    List<ctrlSong> lstSongs =
                        clsSpotifySharedMethods.GetSongsControlsList(
                            clsSpotifySharedMethods.GetSongsList(ref dtLikedSongs), _PlaylistID);




                    lstSongs.ForEach(songControl =>
                    {
                        songControl.PlayPauseClick += PlayPause_Click;
                        fpnlSongs.Controls.Add(songControl);
                    });
                }

            }
        }

     
        public FlowLayoutPanel GetSongsFLowPanel()
        {
            return fpnlSongs;
        }

        public void DisplayLikedSongs(int UserID)
        {
            DisplaySongsOnPnl(UserID);
        }

        /// <summary>
        /// This function starts the playlist if the playlist is not played
        /// </summary>
        void StartPlaylist()
        {
            //checking if the song played for the first time 
            if (clsScene.SongsQueue.PlaylistID != this.PlaylistID)
            {
                //here we play the song and the event PlayPause invokes the function PlayPause_Click
                if (clsScene.SongsQueue.Mode == clsSongsQueue.enMode.eShuffle
                    || clsScene.SongsQueue.FirstTimeShuffle)
                {
                    //playing a random song in the playlist because of the shuffle mode
                    ((ctrlSong)fpnlSongs.Controls[
                        clsSpotifySharedMethods.GetRandomNum(0,
                        fpnlSongs.Controls.Count - 1)]).PerformPlayPause();
                }
                else if (clsScene.SongsQueue.Mode == clsSongsQueue.enMode.eNormal)
                {
                    ((ctrlSong)fpnlSongs.Controls[0]).PerformPlayPause();
                }
                 
            }
            else
            {
                ((ctrlSong)fpnlSongs.Controls[clsScene.SongsQueue.CurrentPlayedSongIndex]).PerformPlayPause();
            }



        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            clsSpotifySharedMethods.SetPlayPauseButton((BunifuImageButton)sender,
               ((BunifuImageButton)sender).Tag.ToString() == "0");


            StartPlaylist();

        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            //updating UI
            clsSpotifySharedMethods.SetShuffleButton((BunifuImageButton)sender,
              ((BunifuImageButton)sender).Tag.ToString() == "0");

            clsScene.frmMainScreen.UpdateShuffleButtonUI(
                ((BunifuImageButton)sender).Tag.ToString() == "0");

            //


            clsSpotifySharedMethods.PerformShuffleSort((BunifuImageButton)sender);

        }
    }
}
