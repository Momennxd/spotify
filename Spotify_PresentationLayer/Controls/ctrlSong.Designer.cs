namespace Spotify_PresentationLayer.Controls
{
    partial class ctrlSong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblArtist = new System.Windows.Forms.Label();
            this.lblSongName = new System.Windows.Forms.Label();
            this.pnlSongName = new System.Windows.Forms.Panel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblListeners = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.btnSongSettings = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnLikeSong = new Bunifu.Framework.UI.BunifuImageButton();
            this.pbPlaySongEffect = new System.Windows.Forms.PictureBox();
            this.pbSongPic = new System.Windows.Forms.PictureBox();
            this.btnPlayPause = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnlSongName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSongSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLikeSong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlaySongEffect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSongPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlayPause)).BeginInit();
            this.SuspendLayout();
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblArtist.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.lblArtist.Location = new System.Drawing.Point(149, 38);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(38, 15);
            this.lblArtist.TabIndex = 39;
            this.lblArtist.Text = "Artist";
            this.lblArtist.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lblArtist.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongName.ForeColor = System.Drawing.Color.White;
            this.lblSongName.Location = new System.Drawing.Point(3, 2);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(90, 20);
            this.lblSongName.TabIndex = 38;
            this.lblSongName.Text = "Song Name";
            // 
            // pnlSongName
            // 
            this.pnlSongName.Controls.Add(this.lblSongName);
            this.pnlSongName.Location = new System.Drawing.Point(145, 12);
            this.pnlSongName.Name = "pnlSongName";
            this.pnlSongName.Size = new System.Drawing.Size(290, 23);
            this.pnlSongName.TabIndex = 44;
            this.pnlSongName.Click += new System.EventHandler(this.Controls_MouseCLick);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.ForeColor = System.Drawing.Color.White;
            this.lblDuration.Location = new System.Drawing.Point(887, 25);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(49, 20);
            this.lblDuration.TabIndex = 42;
            this.lblDuration.Text = "00:00";
            this.lblDuration.Click += new System.EventHandler(this.Controls_MouseCLick);
            // 
            // lblListeners
            // 
            this.lblListeners.AutoSize = true;
            this.lblListeners.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListeners.ForeColor = System.Drawing.Color.White;
            this.lblListeners.Location = new System.Drawing.Point(654, 25);
            this.lblListeners.Name = "lblListeners";
            this.lblListeners.Size = new System.Drawing.Size(72, 20);
            this.lblListeners.TabIndex = 41;
            this.lblListeners.Text = "Listeners";
            this.lblListeners.Click += new System.EventHandler(this.Controls_MouseCLick);
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAlbum.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.lblAlbum.Location = new System.Drawing.Point(471, 22);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(52, 20);
            this.lblAlbum.TabIndex = 40;
            this.lblAlbum.Text = "Abum";
            this.lblAlbum.Click += new System.EventHandler(this.lblAlbumName_Click);
            this.lblAlbum.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lblAlbum.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // btnSongSettings
            // 
            this.btnSongSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSongSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSongSettings.Image = global::Spotify_PresentationLayer.Properties.Resources.icons8_ellipsis_60;
            this.btnSongSettings.ImageActive = null;
            this.btnSongSettings.Location = new System.Drawing.Point(977, 22);
            this.btnSongSettings.Name = "btnSongSettings";
            this.btnSongSettings.Size = new System.Drawing.Size(37, 28);
            this.btnSongSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSongSettings.TabIndex = 47;
            this.btnSongSettings.TabStop = false;
            this.btnSongSettings.Tag = "0";
            this.btnSongSettings.Zoom = 10;
            this.btnSongSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnSongSettings.MouseLeave += new System.EventHandler(this.btnControls_MouseEnter);
            // 
            // btnLikeSong
            // 
            this.btnLikeSong.BackColor = System.Drawing.Color.Transparent;
            this.btnLikeSong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLikeSong.Image = global::Spotify_PresentationLayer.Properties.Resources.icons8_heart_481;
            this.btnLikeSong.ImageActive = null;
            this.btnLikeSong.Location = new System.Drawing.Point(790, 22);
            this.btnLikeSong.Name = "btnLikeSong";
            this.btnLikeSong.Size = new System.Drawing.Size(31, 28);
            this.btnLikeSong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnLikeSong.TabIndex = 46;
            this.btnLikeSong.TabStop = false;
            this.btnLikeSong.Tag = "0";
            this.btnLikeSong.Zoom = 10;
            this.btnLikeSong.Click += new System.EventHandler(this.btnHeart_Click);
            this.btnLikeSong.MouseLeave += new System.EventHandler(this.btnControls_MouseEnter);
            // 
            // pbPlaySongEffect
            // 
            this.pbPlaySongEffect.Image = global::Spotify_PresentationLayer.Properties.Resources.R1;
            this.pbPlaySongEffect.Location = new System.Drawing.Point(10, 4);
            this.pbPlaySongEffect.Name = "pbPlaySongEffect";
            this.pbPlaySongEffect.Size = new System.Drawing.Size(59, 61);
            this.pbPlaySongEffect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPlaySongEffect.TabIndex = 45;
            this.pbPlaySongEffect.TabStop = false;
            this.pbPlaySongEffect.Visible = false;
            this.pbPlaySongEffect.Click += new System.EventHandler(this.Controls_MouseCLick);
            // 
            // pbSongPic
            // 
            this.pbSongPic.Location = new System.Drawing.Point(75, 4);
            this.pbSongPic.Name = "pbSongPic";
            this.pbSongPic.Size = new System.Drawing.Size(60, 60);
            this.pbSongPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSongPic.TabIndex = 43;
            this.pbSongPic.TabStop = false;
            this.pbSongPic.Click += new System.EventHandler(this.Controls_MouseCLick);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayPause.Image = global::Spotify_PresentationLayer.Properties.Resources.icons8_play_100;
            this.btnPlayPause.ImageActive = null;
            this.btnPlayPause.Location = new System.Drawing.Point(28, 20);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(28, 28);
            this.btnPlayPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPlayPause.TabIndex = 48;
            this.btnPlayPause.TabStop = false;
            this.btnPlayPause.Tag = "0";
            this.btnPlayPause.Zoom = 10;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            this.btnPlayPause.MouseLeave += new System.EventHandler(this.btnControls_MouseEnter);
            // 
            // ctrlSong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.btnSongSettings);
            this.Controls.Add(this.btnLikeSong);
            this.Controls.Add(this.pbPlaySongEffect);
            this.Controls.Add(this.pbSongPic);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.pnlSongName);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblListeners);
            this.Controls.Add(this.lblAlbum);
            this.Name = "ctrlSong";
            this.Size = new System.Drawing.Size(1029, 69);
            this.Click += new System.EventHandler(this.Controls_MouseCLick);
            this.MouseEnter += new System.EventHandler(this.ctrlSong_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ctrlSong_MouseLeave);
            this.pnlSongName.ResumeLayout(false);
            this.pnlSongName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSongSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLikeSong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlaySongEffect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSongPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlayPause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pbPlaySongEffect;
        private System.Windows.Forms.PictureBox pbSongPic;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.Panel pnlSongName;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblListeners;
        private System.Windows.Forms.Label lblAlbum;
        private Bunifu.Framework.UI.BunifuImageButton btnLikeSong;
        private Bunifu.Framework.UI.BunifuImageButton btnSongSettings;
        private Bunifu.Framework.UI.BunifuImageButton btnPlayPause;
    }
}
