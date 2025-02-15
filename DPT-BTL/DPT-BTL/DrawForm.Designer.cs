namespace DPT_BTL
{
    partial class DrawForm
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
            draw_panel = new Panel();
            flowLayoutPanel_img = new FlowLayoutPanel();
            btn_search = new Button();
            pictureBox1 = new PictureBox();
            btn_ChooseFolder = new Button();
            label1 = new Label();
            label2 = new Label();
            btn_OK = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // draw_panel
            // 
            draw_panel.BackColor = SystemColors.ButtonHighlight;
            draw_panel.Location = new Point(48, 56);
            draw_panel.Name = "draw_panel";
            draw_panel.Size = new Size(322, 139);
            draw_panel.TabIndex = 0;
            // 
            // flowLayoutPanel_img
            // 
            flowLayoutPanel_img.BackColor = SystemColors.ButtonHighlight;
            flowLayoutPanel_img.Location = new Point(48, 317);
            flowLayoutPanel_img.Name = "flowLayoutPanel_img";
            flowLayoutPanel_img.Size = new Size(688, 247);
            flowLayoutPanel_img.TabIndex = 1;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(48, 211);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(94, 29);
            btn_search.TabIndex = 2;
            btn_search.Text = "Tìm kiếm";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_searchInDB_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonHighlight;
            pictureBox1.Location = new Point(424, 56);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(312, 139);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // btn_ChooseFolder
            // 
            btn_ChooseFolder.Location = new Point(48, 259);
            btn_ChooseFolder.Name = "btn_ChooseFolder";
            btn_ChooseFolder.Size = new Size(244, 29);
            btn_ChooseFolder.TabIndex = 4;
            btn_ChooseFolder.Text = "Chọn thư mục ảnh";
            btn_ChooseFolder.UseVisualStyleBackColor = true;
            btn_ChooseFolder.Click += btn_ChooseFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 19);
            label1.Name = "label1";
            label1.Size = new Size(207, 20);
            label1.TabIndex = 5;
            label1.Text = "Vẽ phác họa vào ô để tìm ảnh";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(424, 19);
            label2.Name = "label2";
            label2.Size = new Size(109, 20);
            label2.TabIndex = 6;
            label2.Text = "Ảnh được chọn";
            // 
            // btn_OK
            // 
            btn_OK.Location = new Point(642, 15);
            btn_OK.Name = "btn_OK";
            btn_OK.Size = new Size(94, 29);
            btn_OK.TabIndex = 7;
            btn_OK.Text = "OK";
            btn_OK.UseVisualStyleBackColor = true;
            btn_OK.Click += btn_OK_Click;
            // 
            // DrawForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(782, 607);
            Controls.Add(btn_OK);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btn_ChooseFolder);
            Controls.Add(pictureBox1);
            Controls.Add(btn_search);
            Controls.Add(flowLayoutPanel_img);
            Controls.Add(draw_panel);
            Name = "DrawForm";
            Text = "DrawForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel draw_panel;
        private FlowLayoutPanel flowLayoutPanel_img;
        private Button btn_search;
        private PictureBox pictureBox1;
        private Button btn_ChooseFolder;
        private Label label1;
        private Label label2;
        private Button btn_OK;
    }
}