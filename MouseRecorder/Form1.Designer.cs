namespace MouseRecorder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            recordBtn = new Button();
            playBtn = new Button();
            isRecordingLabel = new Label();
            mouseCurrentCoord = new Label();
            savedOperations = new ListBox();
            isPlayingLabel = new Label();
            currentOperations = new ListBox();
            playbackCount = new TextBox();
            label1 = new Label();
            resetBtn = new Button();
            SuspendLayout();
            // 
            // recordBtn
            // 
            recordBtn.Location = new Point(263, 403);
            recordBtn.Name = "recordBtn";
            recordBtn.Size = new Size(95, 23);
            recordBtn.TabIndex = 0;
            recordBtn.Text = "Record";
            recordBtn.UseVisualStyleBackColor = true;
            recordBtn.Click += recordBtn_Click;
            // 
            // playBtn
            // 
            playBtn.Location = new Point(440, 403);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(75, 23);
            playBtn.TabIndex = 1;
            playBtn.Text = "Play";
            playBtn.UseVisualStyleBackColor = true;
            playBtn.Click += playBtn_Click;
            // 
            // isRecordingLabel
            // 
            isRecordingLabel.AutoSize = true;
            isRecordingLabel.Location = new Point(263, 426);
            isRecordingLabel.Name = "isRecordingLabel";
            isRecordingLabel.Size = new Size(81, 15);
            isRecordingLabel.TabIndex = 2;
            isRecordingLabel.Text = "Not recording";
            // 
            // mouseCurrentCoord
            // 
            mouseCurrentCoord.AutoSize = true;
            mouseCurrentCoord.Location = new Point(21, 407);
            mouseCurrentCoord.Name = "mouseCurrentCoord";
            mouseCurrentCoord.Size = new Size(148, 15);
            mouseCurrentCoord.TabIndex = 3;
            mouseCurrentCoord.Text = "Mouse Current Coordinate";
            // 
            // savedOperations
            // 
            savedOperations.FormattingEnabled = true;
            savedOperations.ItemHeight = 15;
            savedOperations.Location = new Point(21, 16);
            savedOperations.Name = "savedOperations";
            savedOperations.Size = new Size(236, 379);
            savedOperations.TabIndex = 4;
            // 
            // isPlayingLabel
            // 
            isPlayingLabel.AutoSize = true;
            isPlayingLabel.Location = new Point(440, 426);
            isPlayingLabel.Name = "isPlayingLabel";
            isPlayingLabel.Size = new Size(69, 15);
            isPlayingLabel.TabIndex = 5;
            isPlayingLabel.Text = "Not playing";
            // 
            // currentOperations
            // 
            currentOperations.FormattingEnabled = true;
            currentOperations.ItemHeight = 15;
            currentOperations.Location = new Point(521, 12);
            currentOperations.Name = "currentOperations";
            currentOperations.Size = new Size(267, 379);
            currentOperations.TabIndex = 6;
            // 
            // playbackCount
            // 
            playbackCount.Location = new Point(415, 352);
            playbackCount.Name = "playbackCount";
            playbackCount.Size = new Size(100, 23);
            playbackCount.TabIndex = 7;
            playbackCount.Text = "0";
            playbackCount.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(263, 355);
            label1.Name = "label1";
            label1.Size = new Size(143, 15);
            label1.TabIndex = 8;
            label1.Text = "Number of playbacks left:";
            // 
            // resetBtn
            // 
            resetBtn.Location = new Point(614, 403);
            resetBtn.Name = "resetBtn";
            resetBtn.Size = new Size(75, 23);
            resetBtn.TabIndex = 9;
            resetBtn.Text = "Reset Lists";
            resetBtn.UseVisualStyleBackColor = true;
            resetBtn.Click += resetBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resetBtn);
            Controls.Add(label1);
            Controls.Add(playbackCount);
            Controls.Add(currentOperations);
            Controls.Add(isPlayingLabel);
            Controls.Add(savedOperations);
            Controls.Add(mouseCurrentCoord);
            Controls.Add(isRecordingLabel);
            Controls.Add(playBtn);
            Controls.Add(recordBtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button recordBtn;
        private Button playBtn;
        private Label isRecordingLabel;
        private Label mouseCurrentCoord;
        private ListBox savedOperations;
        private Label isPlayingLabel;
        private ListBox currentOperations;
        private TextBox playbackCount;
        private Label label1;
        private Button resetBtn;
    }
}
