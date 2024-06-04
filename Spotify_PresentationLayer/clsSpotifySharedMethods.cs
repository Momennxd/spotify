using Spotify_PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Spotify_BusinessLayer;
using System.Data;
using System.Threading;
using Bunifu;
using Bunifu.Framework.UI;
using Spotify_PresentationLayer.Controls;

namespace Spotify_PresentationLayer
{
    public static class clsSpotifySharedMethods
    {
        public const int ChildrenListCapacity = 3;

        public static void GetUserInfoByFile(ref string UserName, ref string Password, string FilePath)
        {
            string FileContent = GetFileTextContent(FilePath);

            UserName = FileContent.Split(new string[] { "#//#" }, StringSplitOptions.None)[0];
            Password = FileContent.Split(new string[] { "#//#" }, StringSplitOptions.None)[1];
        }

        public static void ClearFileContent(string FilePath)
        {
            string text = "";

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                // Write the text to the file
                writer.Write(text);
            }
        }

        public static string GetFileTextContent(string FilePath)
        {
            // Create a new instance of StreamReader and specify the file path
            using (StreamReader reader = new StreamReader(FilePath))
            {
                // Read the entire content of the file
                string text = reader.ReadToEnd();

                // Display the text
                return text;
            }
        }

        public static void SaveUserInfoToLogin(string UserName, string Password, string FilePath, char Sep)
        {
            string text = UserName + Sep + Password;

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                // Write the text to the file
                writer.Write(text);
            }
        }

        public static void ShowFormInPanel(Panel pnl, Form frm)
        {
            pnl.Controls.Clear();
            frm.Dock = DockStyle.Fill; frm.TopLevel = false; frm.TopMost = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            pnl.Controls.Add(frm);
            frm.Show();
        }

        public static void RoundControlsByCustomElips(Bunifu.Framework.UI.BunifuElipse bElips, Control[] C)
        {
            if (bElips is null)
            {
                throw new ArgumentNullException(nameof(bElips));
            }

            for (int i = 0; i < C.Length; i++)
            {
                bElips.ApplyElipse(C[i]);
            }
        }

        public static void PerfomTwoStatesButtonStyle(BunifuImageButton Button, Image OffStateImage, Image OnStateImage)
        {
            if (Button.Tag.ToString() == "0")
            {
                Button.Image = OnStateImage;
                Button.Tag = "1";

            }
            else
            {
                Button.Image = OffStateImage;
                Button.Tag = "0";

            }
        }

        public static void ShowDataOnContolText(Control LBL, dynamic Data)
        {
            LBL.Text = Data.ToString();
        }

        public static void ShowDateOnCustomDatePicker(BunifuDatepicker DT, DateTime Date)
        {
            DT.Value = Date;
        }

        public static void ShowItemAsSelectedOnComboBoxByName(ref ComboBox CB, string ItemName)
        {
            for (int i = 0; i < CB.Items.Count; i++)
            {
                if (CB.GetItemText(CB.Items[i]) == ItemName)
                {
                    CB.SelectedIndex = i;
                    return;
                }
            }
        }


        public static void ImportNationalitiesToComboBox(ComboBox cbNationalities, DataTable dtCountries)
        {
            foreach (DataRow Row in dtCountries.Rows)
            {
                cbNationalities.Items.Add(Row["CountryName"].ToString());
            }
        }

        public static void ImportArtistsNickNamesToComboBox(ComboBox cbArtistsNickNames, DataTable dtArtists)
        {
            foreach (DataRow Row in dtArtists.Rows)
            {
                cbArtistsNickNames.Items.Add(Row["NickName"].ToString());
            }
        }


        public static void DispalyImageForUser(PictureBox pbPersonPic, clsPerson Person)
        {
            if (string.IsNullOrEmpty(Person.ProfilePicPath))
            {
                if (Person.Gender == true)
                    pbPersonPic.Image = Resources.MaleUser;

                else if (Person.Gender == false)

                    pbPersonPic.Image = Resources.FemaleUser;
                else
                    pbPersonPic.Image = null;
            }
            else
            {
                pbPersonPic.ImageLocation = Person.ProfilePicPath;

            }
        }

        public static bool IsNumber(string UserInput)
        {
            double number;
            if (double.TryParse(UserInput, out number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateIfTxtBoxIsNotNumber(Control txtBox, string ErrorText, ErrorProvider errorProvider)
        {
            if (!IsNumber(txtBox.Text))
            {
                errorProvider.SetError(txtBox, ErrorText);
                return false;
            }

            return true;
        }

        public static bool ValidateTxtBox(System.Windows.Forms.TextBox txtBox, string ErrorCase, string ErrorText, ErrorProvider errorProvider)
        {
            if (txtBox.Text == ErrorCase)
            {
                errorProvider.SetError(txtBox, ErrorText);
                return false;
            }

            return true;
        }

        public static bool ValidateTxtBox(BunifuTextbox txtBox, string ErrorCase, string ErrorText, ErrorProvider errorProvider)
        {
            if (txtBox.Text == ErrorCase)
            {
                errorProvider.SetError(txtBox, ErrorText);
                return false;
            }

            return true;
        }

        public static bool ValidateTxtBox(System.Windows.Forms.TextBox txtBox, bool ErrorCase, string ErrorText, ErrorProvider errorProvider)
        {
            if (ErrorCase)
            {
                errorProvider.SetError(txtBox, ErrorText);
                return false;
            }

            return true;
        }

        public static bool ValidateComboBoxIfEmpty(ComboBox CB, string ErrorCase, string ErrorText, ErrorProvider errorProvider)
        {
            if (CB.SelectedItem == null || CB.SelectedItem.ToString() == ErrorCase)
            {
                errorProvider.SetError(CB, ErrorText);
                return false;
            }

            return true;
        }


        public static bool IsEmailValid(string Email)
        {
            if (Email == "")
                return true;

            Email = Email.ToLower();
            return (Email.Contains("@") && Email.EndsWith(".com"));
        }

        public static bool IsDate1GreaterThanDate2(DateTime date1, DateTime date2)
        {
            return (date1 > date2);
        }

        public static void ImportDataToPersonObj(dynamic Val, ref dynamic Varible)
        {
            Varible = Val;
        }

        public static byte GetGenderIndexByName(string GenderName)
        {
            if (GenderName == "Male")
                return 0;
            else if (GenderName == "Female")
                return 1;
            else
                return 2;
        }

        public static bool ValidateCustomDatePicker(BunifuDatepicker bDatePicker, DateTime minDate, DateTime maxDate, string ErrorText, ErrorProvider errorProvider)
        {
            if (bDatePicker.Value > maxDate || bDatePicker.Value < minDate)
            {
                errorProvider.SetError(bDatePicker, ErrorText);
                return false;
            }

            return true;
        }

        public static bool ValidateTxtBox(Control txtBox, bool ErrorCase, string ErrorText, ErrorProvider errorProvider)
        {
            if (ErrorCase)
            {
                errorProvider.SetError(txtBox, ErrorText);
                return false;
            }

            return true;
        }


        public static DateTime GetBirthDateFor18YearsAgo()
        {
            DateTime today = DateTime.Today;
            DateTime birthDate = today.AddYears(-18);
            return birthDate;
        }

        public static DateTime GetBirthDateFor110YearsAgo()
        {
            DateTime today = DateTime.Today;
            DateTime maxAgeDate = today.AddYears(-110);
            return maxAgeDate;
        }

        public static void DeleteOldPersonImageFromSource(clsPerson Person)
        {
            if (!string.IsNullOrEmpty(Person.ProfilePicPath))
            {
                System.IO.File.Delete(Person.ProfilePicPath);
            }
        }

        public static void DeleteOldSourceFile(string FilePath)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
        }


        public static void DispalyDefaultImageForUser(PictureBox pbPersonPic, clsPerson Person)
        {

            if (Person.Gender == true)
                pbPersonPic.Image = Resources.MaleUser;

            else if (Person.Gender == false)

                pbPersonPic.Image = Resources.FemaleUser;
            else
                pbPersonPic.Image = null;

        }

        public static void DispalyDefaultImageForUser(PictureBox pbPersonPic, int Gender)
        {

            if (Gender == 0)
                pbPersonPic.Image = Resources.MaleUser;

            else if (Gender == 1)

                pbPersonPic.Image = Resources.FemaleUser;
            else
                pbPersonPic.Image = null;

        }

        public static void ResetCustomSearchBar(BunifuTextbox bTextBox, Label lblSearchBar)
        {
            bTextBox.Text = "";
            lblSearchBar.Text = "Search";
        }

        public static void ResetCustomSearchBar(BunifuTextbox bTextBox, Label lblSearchBar, string SearchBarTxt)
        {
            bTextBox.Text = "";
            lblSearchBar.Text = SearchBarTxt;
        }



        public static string GetSongDurationStringFormat(int Duration)
        {
            int nMinutes = 0, nSeconds = 0;

            if (Duration > 0)
            {
                nMinutes = Duration / 60;
                nSeconds = Duration % 60;
            }

            return (nMinutes < 10 ? ("0" + nMinutes) : nMinutes.ToString()) + ":" +
                (nSeconds < 10 ? ("0" + nSeconds) : nSeconds.ToString());

        }

        public static double GetMp3Duration(string filePath)
        {
            try
            {
                var file = TagLib.File.Create(filePath);
                return file.Properties.Duration.TotalSeconds;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void SetButtonMouseEnterLeave(Button button, bool IsEnter, Color MouseEnterClr, Color MouseLeaveClr)
        {
            if (IsEnter)
            {
                button.ForeColor = MouseEnterClr;
            }
            else
            {
                button.ForeColor = MouseLeaveClr;

            }
        }


        public static List<object> ConvertToList(ref DataTable dataTable)
        {
            List<object> result = new List<object>();

            foreach (DataRow row in dataTable.Rows)
            {
                List<object> rowData = new List<object>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    rowData.Add(row[column]);
                }

                result.Add(rowData);
            }

            return result;
        }

        public static List<object> ConvertToList(DataTable dataTable)
        {
            List<object> result = new List<object>();

            foreach (DataRow row in dataTable.Rows)
            {
                List<object> rowData = new List<object>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    rowData.Add(row[column]);
                }

                result.Add(rowData);
            }

            return result;
        }


        /// <summary>
        /// this functions takes the datatabel that has the aritsts of a song and concatenates
        /// them to an array (use it with the string.join)
        /// </summary>
        /// <param name="Artists">
        /// arists data table with the column "NickName"
        /// </param>      
        /// <returns>
        /// array of song artists
        /// </returns>
        public static string[] ConcatenateSongArtists(DataTable Artists)
        {
            List<string> result = new List<string>();

            foreach (DataRow row in Artists.Rows)
            {
                result.Add(row["NickName"].ToString());
            }

            return result.ToArray();
        }

        public static void SetHeartButton(BunifuImageButton button, bool On)
        {
            if (On)
            {
                button.Image = Resources.icons8_heart_1001;
                button.Tag = "1";

            }
            else
            {
                button.Image = Resources.icons8_heart_48;
                button.Tag = "0";

            }
        }

        public static void SetPlayPauseButton(BunifuImageButton button, bool On)
        {
            if (On)
            {
                button.Image = Resources.icons8_pause_1001;
                button.Tag = "1";

            }
            else
            {
                button.Image = Resources.icons8_play_100;
                button.Tag = "0";

            }
        }

        public static void SetShuffleButton(BunifuImageButton button, bool On)
        {
            if (On)
            {
                button.Image = Resources.icons8_shuffle_60__1_1;
                button.Tag = "1";

            }
            else
            {
                button.Image = Resources.icons8_shuffle_60;
                button.Tag = "0";

            }
        }

        /// <summary>
        /// This functions show if a song in a playlist is played or not,
        /// It updates the UI
        /// </summary>
        public static void ShowSongState(FlowLayoutPanel Pnl, int PlaylistID, int SongIndex, bool Play)
        {
            if (Pnl == null || Pnl.Controls.Count == 0)
                return;

            foreach (ctrlSong song in Pnl.Controls)
            {
                //making sure that we are altering the right playlist :)
                if (PlaylistID != song.PlaylistID)
                    return;

                if (song.SongIndexInPlaylist == SongIndex)
                {
                    if (Play)
                    {
                        //dislplay as played
                        song.DisplayAsPlayed(true);

                    }
                    else
                    {
                        //display as stopped or paused
                        song.DisplayAsPlayed(false);

                    }
                }
            }
        }


        /// <summary>
        /// this function updates all the songs UI, It displays if the songs are played or paused
        /// </summary>
        public static void UpdatePlaylistUI(FlowLayoutPanel Pnl)
        {
            foreach (ctrlSong song in Pnl.Controls)
            {
                if (song.Song.SongID == clsPlayedSong.PlayedSong.SongID && clsPlayedSong.IsPlayed)
                {
                    //dislplay as played
                    song.DisplayAsPlayed(true);
                }
                else
                {
                    //display as stopped or paused
                    song.DisplayAsPlayed(false);
                }
            }
        }

        
        /// <summary>
        /// this functions loads the songs user controls to a list using Multithreading
        /// (it adds no events)
        /// 
        /// </summary>
        /// <param name="dtSongs"></param>
        /// <param name="PlaylistID"></param>
        /// <returns>
        /// songs user controls list
        /// </returns>
        
        //public static List<ctrlSong> GetSongsControlsList(DataTable dtSongs, int PlaylistID)
        //{
        //    List<ctrlSong> lstsongsControls = new List<ctrlSong>();
        //    ctrlSong SongControl;
        //    clsSong song;

        //    Parallel.For(0, dtSongs.Rows.Count, i =>
        //    {
        //        song = new clsSong()
        //        {
        //            SongID = Convert.ToInt16(dtSongs.Rows[i]["SongID"]),
        //            SongName = dtSongs.Rows[i]["SongName"].ToString(),
        //            ImagePath = dtSongs.Rows[i]["ImagePath"].ToString(),
        //            SongPath = dtSongs.Rows[i]["SongPath"].ToString(),
        //            Duration = Convert.ToInt16(dtSongs.Rows[i]["Duration"]),
        //            ReleaseDate = (DateTime)dtSongs.Rows[i]["ReleaseDate"],
        //            PlayCount = Convert.ToInt16(dtSongs.Rows[i]["PlayCount"]),
        //            GenreID = Convert.ToInt16(dtSongs.Rows[i]["GenreID"]),
        //        };

        //        SongControl = new ctrlSong(song, i, PlaylistID);
        //        SongControl.DisplaySongData(song);

        //        lstsongsControls.Add(SongControl);
        //    }
        //    );


        //    return lstsongsControls;
        //}
        


        private static List<List<clsSong>> _GetSongsList(ref DataTable dtSongs, int EachListCapacity) 
        {
            List<List<clsSong>> songsListFather = new List<List<clsSong>>();

            List<clsSong> songsListChildren = new List<clsSong>();

            clsSong song;


            for (int i = 0; i < dtSongs.Rows.Count; i++)
            {                
                song = new clsSong()
                {

                    SongID = Convert.ToInt16(dtSongs.Rows[i]["SongID"]),
                    SongName = dtSongs.Rows[i]["SongName"].ToString(),
                    ImagePath = dtSongs.Rows[i]["ImagePath"].ToString(),
                    SongPath = dtSongs.Rows[i]["SongPath"].ToString(),
                    Duration = Convert.ToInt16(dtSongs.Rows[i]["Duration"]),
                    ReleaseDate = (DateTime)dtSongs.Rows[i]["ReleaseDate"],
                    PlayCount = Convert.ToInt16(dtSongs.Rows[i]["PlayCount"]),
                    GenreID = Convert.ToInt16(dtSongs.Rows[i]["GenreID"]),

                };

                songsListChildren.Add(song);
                
                if (i == (EachListCapacity - 1) || i == dtSongs.Rows.Count - 1)
                {
                    songsListFather.Add(songsListChildren);
                    songsListChildren = new List<clsSong>();

                }
            }

            return songsListFather;

        }

        private static void _AddSongsToMainControl(ref List<ctrlSong> MainControlsList,
            int SongIndexInPlaylistConst, List<clsSong> SongsList, int PlaylistID)
        {

            ctrlSong SongControls;

            for (byte i = 0; i < SongsList.Count; i++)
            {
                SongControls = new ctrlSong(SongsList[i], i + SongIndexInPlaylistConst, PlaylistID);

                SongControls.DisplaySongData(SongsList[i]);
                MainControlsList.Add(SongControls);
            }

           
        }


        private static List<ctrlSong> LoadSongsControlsList(List<ctrlSong> MainControlsList,
            List<List<clsSong>> SongsList, int PlaylistID)
        {

            if (SongsList.Count == 0)
                return null;

            Thread[] threads = new Thread[SongsList.Count];

            for (int i = 0; i < SongsList.Count; i++)
            {
                threads[i] = new Thread(() => _AddSongsToMainControl(
                    ref MainControlsList, i, SongsList[i], PlaylistID));
                threads[i].Start();
               // threads[i].Join();
            }

            return MainControlsList;

        }

        public static List<ctrlSong> GetSongsControlsList(ref DataTable dtSongs, int PlaylistID)
        {
           
            List<ctrlSong> MainControlsList = new List<ctrlSong>();

            //getting the father songs list

            //int EachListCapacity = dtSongs.Rows.Count / ChildrenListCapacity +
            //    dtSongs.Rows.Count % ChildrenListCapacity;
    
            List<List<clsSong>> SongsList =
                _GetSongsList(ref dtSongs, ChildrenListCapacity);


            MainControlsList = LoadSongsControlsList(MainControlsList, SongsList, PlaylistID);





            return MainControlsList;
        }






        /// <summary>
        /// this functions convrets a songs data table to a list
        /// </summary>
        /// <param name="dtSongs"></param>
        /// <returns>
        /// a songs Datatable 
        /// </returns>
        public static List<clsSong> GetSongsList(ref DataTable dtSongs)
        {
            List<clsSong> listSongs = new List<clsSong> ();
            foreach (DataRow song in dtSongs.Rows)
            {
                listSongs.Add(new clsSong()
                {

                    SongID = Convert.ToInt16(song["SongID"]),
                    SongName = song["SongName"].ToString(),
                    ImagePath = song["ImagePath"].ToString(),
                    SongPath = song["SongPath"].ToString(),
                    Duration = Convert.ToInt16(song["Duration"]),
                    ReleaseDate = (DateTime)song["ReleaseDate"],
                    PlayCount = Convert.ToInt16(song["PlayCount"]),
                    GenreID = Convert.ToInt16(song["GenreID"]),

                });
            }

            return listSongs;
        }


        public static List<ctrlSong> GetSongsControlsList(List<clsSong> SongsList, int PlaylistID)
        {
            List<ctrlSong> MainControlsList = new List<ctrlSong>();
            ctrlSong songcontrol = null;
          
            int Iteration = 0;
            SongsList.ForEach(song =>
            {
                songcontrol = new ctrlSong(SongsList.ElementAt(Iteration), Iteration, PlaylistID);
                songcontrol.DisplaySongData();
                MainControlsList.Add(songcontrol);
                Iteration++;
            });

            return MainControlsList;    
        }



   
        public static void PerformShuffleSort(BunifuImageButton button)
        {
            if (button == null)
                return;

            if (button.Tag.ToString() == "0")
            {
                clsScene.SongsQueue.Sort();
            }
            else
            {
                clsScene.SongsQueue.FirstTimeShuffle = true;
                clsScene.SongsQueue.Shuffle();
            }
        }



        public static int GetRandomNum(int From, int To)
        {
            Random r = new Random();
            return r.Next(From, To);
        }





    }
}