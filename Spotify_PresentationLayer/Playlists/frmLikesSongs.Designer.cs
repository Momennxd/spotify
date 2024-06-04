namespace Spotify_PresentationLayer.Playlists
{
    partial class frmLikesSongs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlLikedSongsPlaylist1 = new Spotify_PresentationLayer.Controls.ctrlLikedSongsPlaylist();
            this.SuspendLayout();
            // 
            // ctrlLikedSongsPlaylist1
            // 
            this.ctrlLikedSongsPlaylist1.BackColor = System.Drawing.Color.Black;
            this.ctrlLikedSongsPlaylist1.Location = new System.Drawing.Point(-6, -7);
            this.ctrlLikedSongsPlaylist1.Name = "ctrlLikedSongsPlaylist1";
            this.ctrlLikedSongsPlaylist1.Size = new System.Drawing.Size(1068, 731);
            this.ctrlLikedSongsPlaylist1.TabIndex = 0;
            // 
            // frmLikesSongs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1068, 731);
            this.Controls.Add(this.ctrlLikedSongsPlaylist1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLikesSongs";
            this.Text = "frmLikesSongs";
            this.Load += new System.EventHandler(this.frmLikesSongs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Controls.ctrlLikedSongsPlaylist ctrlLikedSongsPlaylist1;
    }
}