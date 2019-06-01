namespace The_Game_Of_Life
{
    partial class Form1
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
            this.New_game = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // New_game
            // 
            this.New_game.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.New_game.Location = new System.Drawing.Point(12, 12);
            this.New_game.Name = "New_game";
            this.New_game.Size = new System.Drawing.Size(135, 35);
            this.New_game.TabIndex = 0;
            this.New_game.Text = "New game";
            this.New_game.UseVisualStyleBackColor = true;
            this.New_game.Click += new System.EventHandler(this.New_game_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1066, 821);
            this.Controls.Add(this.New_game);
            this.Name = "Form1";
            this.Text = "The Game of Life";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button New_game;
    }
}

