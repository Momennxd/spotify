using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Spotify_BusinessLayer
{
    /// <summary>
    /// this class represents the main played queue
    /// </summary>
    public class clsSongsQueue
    {


        //private members
        /// <summary>
        /// this queue is used to restore the main queue after shuffling
        /// </summary>
        private Queue<clsSong> _OriginSongsQueue;



        private Queue<clsSong> _SongsQueue;


        private int _CurrentPlayedSongIndex;

        public enum enMode { eShuffle = 1, eNormal = 2 }

        private enMode _Mode;

        /// <summary>
        /// 1 - Shuffle
        /// 2 - Normal
        /// </summary>
        public enMode Mode { get { return _Mode; } }

        /// <summary>
        /// if the index is -1 that means there is no played song in this queue.
        /// </summary>
        public int CurrentPlayedSongIndex { get { return _CurrentPlayedSongIndex; } }

        public int PlaylistID { get; set; }


        /// <summary>
        /// this property shows if the user pressed shuffle when there is nothing played,
        /// if true that means the queue needs to be shuffled ASA a song gets played
        /// </summary>
        public bool FirstTimeShuffle { get; set; }  

        public void AddSong(ref clsSong song)
        {
            if (song != null)
                _SongsQueue.Enqueue(song);
        }

        public void AddSongs(ref List<clsSong> songs, int CurrentPlayedSongIndex, enMode mode)
        {
            if (songs != null)
            {
                _SongsQueue.Clear();
                songs.ForEach(song => _SongsQueue.Enqueue(song));

                //setting the played song
                this._CurrentPlayedSongIndex = CurrentPlayedSongIndex;

                if (mode == enMode.eShuffle || this.FirstTimeShuffle)
                    this.Shuffle();
            }
        }


        public void AddSongs(ref DataTable songs, int CurrentPlayedSongIndex, enMode mode)
        {
            if (songs != null)
            {
                _SongsQueue.Clear();

                foreach (DataRow row in songs.Rows)
                {
                    clsSong song = clsSong.FindBySongID(Convert.ToInt16(row["SongID"]));
                                      
                    _SongsQueue.Enqueue(song);
                }

                //setting the played song
                this._CurrentPlayedSongIndex = CurrentPlayedSongIndex;

                if (mode == enMode.eShuffle || this.FirstTimeShuffle)
                    this.Shuffle();

            }

           


        }



        /// <summary>
        /// this function sets the _CurrentPlayedSongIndex private member to the parameter Index.
        /// </summary>
        /// <param name="SongIndexInPlaylist">
        /// this index should be the song index in the playlist starting from 0.
        /// </param>
        public void SetCurrentPlayedSong(int SongIndexInPlaylist)
        {
            if (SongIndexInPlaylist > 0 && SongIndexInPlaylist < _SongsQueue.Count)
                _CurrentPlayedSongIndex = SongIndexInPlaylist;
        }

        public int GetNextSongIndex()
        {
            //getting the first song index of the playlist if the current song is the last one
            if (_CurrentPlayedSongIndex == (_SongsQueue.Count - 1))
            {
                _CurrentPlayedSongIndex = 0;
                return _CurrentPlayedSongIndex;
            }
            else
                return ++_CurrentPlayedSongIndex;

        }

        public int GetPrevSongIndex()
        {
            //getting the last song index of the playlist if the current song is the first one
            if (_CurrentPlayedSongIndex == 0)
            {
                _CurrentPlayedSongIndex = _SongsQueue.Count - 1;
                return _CurrentPlayedSongIndex;
            }
            else
                return --_CurrentPlayedSongIndex;
        }

        public clsSong SetNextSongAsPlayedInQueue()
        {
            if (_SongsQueue.Count == 0)
                return null;

            return _SongsQueue.ElementAt(GetNextSongIndex());
        }

        public clsSong SetPervSongAsPlayedInQueue()
        {
            if (_SongsQueue.Count == 0)
                return null;

            return _SongsQueue.ElementAt(GetPrevSongIndex());
        }

        public clsSong GetCurrentPlayedSong()
        {
            return _SongsQueue.ElementAt(this.CurrentPlayedSongIndex);
        }


        /// <summary>
        /// this function shuffles the queue 
        /// </summary>
        public void Shuffle()
        {
            //if (this.FirstTimeShuffle)
            //    _Mode = enMode.eShuffle;

            if (_Mode == enMode.eShuffle || this._SongsQueue.Count == 0)
                return;

            //setting the origin queue
            _OriginSongsQueue = new Queue<clsSong>(this._SongsQueue);

            if (_SongsQueue.Count > 1)
            {
                List<clsSong> list = _SongsQueue.ToList();
                Random random = new Random();

                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    clsSong value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }

                _SongsQueue.Clear();
                foreach (clsSong item in list)
                {
                    _SongsQueue.Enqueue(item);
                }
            }

            _Mode = enMode.eShuffle;
        }


        /// <summary>
        /// this function unshuffles the queue and resort it by index
        /// </summary>
        public void Sort()
        {

            if (_Mode == enMode.eNormal)
                return;


            //_SongsQueue.Clear();
            _SongsQueue = _OriginSongsQueue;
            //_OriginSongsQueue.Clear();

            _Mode = enMode.eNormal;
        }

        public void Clear()
        {
            this._SongsQueue.Clear();
        }

        public int Size()
        {
            return _SongsQueue.Count;
        }


        public bool IsEmpty()
        {
            return _CurrentPlayedSongIndex == -1;
        }


        public clsSongsQueue()
        {
            _SongsQueue = new Queue<clsSong>();
            _CurrentPlayedSongIndex = -1;
            PlaylistID = -1;
            FirstTimeShuffle = false;
            _Mode = enMode.eNormal;
        }


    }
}