namespace Drawing_program_inkscape_2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInWallDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLay_Tools = new System.Windows.Forms.FlowLayoutPanel();
            this.picbtn_mousetool = new System.Windows.Forms.PictureBox();
            this.picbtn_linetool = new System.Windows.Forms.PictureBox();
            this.picbtn_erasetool = new System.Windows.Forms.PictureBox();
            this.picbtn_HandTool = new System.Windows.Forms.PictureBox();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.lblShow = new System.Windows.Forms.Label();
            this.panelRight2 = new System.Windows.Forms.Panel();
            this.flowTopPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelOptMouse = new System.Windows.Forms.Panel();
            this.picBtnMirrorV = new System.Windows.Forms.PictureBox();
            this.picBtnRotateLeft = new System.Windows.Forms.PictureBox();
            this.picBtnRotateRight = new System.Windows.Forms.PictureBox();
            this.panelOptLine = new System.Windows.Forms.Panel();
            this.numLineY2 = new System.Windows.Forms.NumericUpDown();
            this.numLineX2 = new System.Windows.Forms.NumericUpDown();
            this.numLineY1 = new System.Windows.Forms.NumericUpDown();
            this.numLineLength = new System.Windows.Forms.NumericUpDown();
            this.numLineX1 = new System.Windows.Forms.NumericUpDown();
            this.numLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelOptErase = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.flowLay_Tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_mousetool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_linetool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_erasetool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_HandTool)).BeginInit();
            this.panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.panel_bottom.SuspendLayout();
            this.panelRight2.SuspendLayout();
            this.flowTopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnMirrorV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRotateLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRotateRight)).BeginInit();
            this.panelOptLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineWidth)).BeginInit();
            this.panelOptErase.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1387, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openInWallDesignerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.newToolStripMenuItem.Text = "New*";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.openToolStripMenuItem.Text = "Open*";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.saveToolStripMenuItem.Text = "Save*";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openInWallDesignerToolStripMenuItem
            // 
            this.openInWallDesignerToolStripMenuItem.Name = "openInWallDesignerToolStripMenuItem";
            this.openInWallDesignerToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.openInWallDesignerToolStripMenuItem.Text = "Open In Wall Designer*";
            // 
            // flowLay_Tools
            // 
            this.flowLay_Tools.BackColor = System.Drawing.Color.White;
            this.flowLay_Tools.Controls.Add(this.picbtn_mousetool);
            this.flowLay_Tools.Controls.Add(this.picbtn_linetool);
            this.flowLay_Tools.Controls.Add(this.picbtn_erasetool);
            this.flowLay_Tools.Controls.Add(this.picbtn_HandTool);
            this.flowLay_Tools.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLay_Tools.Location = new System.Drawing.Point(0, 28);
            this.flowLay_Tools.Name = "flowLay_Tools";
            this.flowLay_Tools.Size = new System.Drawing.Size(37, 607);
            this.flowLay_Tools.TabIndex = 5;
            // 
            // picbtn_mousetool
            // 
            this.picbtn_mousetool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbtn_mousetool.Image = global::Drawing_program_inkscape_2.Properties.Resources.mousepointr;
            this.picbtn_mousetool.Location = new System.Drawing.Point(3, 3);
            this.picbtn_mousetool.Name = "picbtn_mousetool";
            this.picbtn_mousetool.Size = new System.Drawing.Size(31, 30);
            this.picbtn_mousetool.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbtn_mousetool.TabIndex = 2;
            this.picbtn_mousetool.TabStop = false;
            this.picbtn_mousetool.Click += new System.EventHandler(this.picbtn_mousetool_Click);
            // 
            // picbtn_linetool
            // 
            this.picbtn_linetool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbtn_linetool.Image = global::Drawing_program_inkscape_2.Properties.Resources.linetool;
            this.picbtn_linetool.Location = new System.Drawing.Point(3, 39);
            this.picbtn_linetool.Name = "picbtn_linetool";
            this.picbtn_linetool.Size = new System.Drawing.Size(31, 30);
            this.picbtn_linetool.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbtn_linetool.TabIndex = 3;
            this.picbtn_linetool.TabStop = false;
            this.picbtn_linetool.Click += new System.EventHandler(this.picbtn_mousetool_Click);
            // 
            // picbtn_erasetool
            // 
            this.picbtn_erasetool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbtn_erasetool.Image = global::Drawing_program_inkscape_2.Properties.Resources.erase;
            this.picbtn_erasetool.Location = new System.Drawing.Point(3, 75);
            this.picbtn_erasetool.Name = "picbtn_erasetool";
            this.picbtn_erasetool.Size = new System.Drawing.Size(31, 30);
            this.picbtn_erasetool.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbtn_erasetool.TabIndex = 3;
            this.picbtn_erasetool.TabStop = false;
            this.picbtn_erasetool.Click += new System.EventHandler(this.picbtn_mousetool_Click);
            // 
            // picbtn_HandTool
            // 
            this.picbtn_HandTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picbtn_HandTool.Image = global::Drawing_program_inkscape_2.Properties.Resources.HandTool;
            this.picbtn_HandTool.Location = new System.Drawing.Point(3, 111);
            this.picbtn_HandTool.Name = "picbtn_HandTool";
            this.picbtn_HandTool.Size = new System.Drawing.Size(31, 30);
            this.picbtn_HandTool.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbtn_HandTool.TabIndex = 4;
            this.picbtn_HandTool.TabStop = false;
            this.picbtn_HandTool.Click += new System.EventHandler(this.picbtn_mousetool_Click);
            // 
            // panel_Main
            // 
            this.panel_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Main.Controls.Add(this.picCanvas);
            this.panel_Main.Controls.Add(this.vScrollBar1);
            this.panel_Main.Controls.Add(this.hScrollBar1);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(37, 128);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(1234, 507);
            this.panel_Main.TabIndex = 8;
            // 
            // picCanvas
            // 
            this.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvas.Location = new System.Drawing.Point(0, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(1211, 484);
            this.picCanvas.TabIndex = 2;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseUp);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(1211, 0);
            this.vScrollBar1.Maximum = 10;
            this.vScrollBar1.Minimum = -10;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 484);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 484);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(1232, 21);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.lblShow);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 635);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(1387, 28);
            this.panel_bottom.TabIndex = 9;
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(3, 3);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(46, 17);
            this.lblShow.TabIndex = 0;
            this.lblShow.Text = "label1";
            // 
            // panelRight2
            // 
            this.panelRight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight2.Controls.Add(this.picBtnMirrorV);
            this.panelRight2.Controls.Add(this.picBtnRotateLeft);
            this.panelRight2.Controls.Add(this.picBtnRotateRight);
            this.panelRight2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight2.Location = new System.Drawing.Point(1271, 28);
            this.panelRight2.Name = "panelRight2";
            this.panelRight2.Size = new System.Drawing.Size(116, 607);
            this.panelRight2.TabIndex = 0;
            // 
            // flowTopPanel
            // 
            this.flowTopPanel.Controls.Add(this.panelOptMouse);
            this.flowTopPanel.Controls.Add(this.panelOptLine);
            this.flowTopPanel.Controls.Add(this.panelOptErase);
            this.flowTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowTopPanel.Location = new System.Drawing.Point(37, 28);
            this.flowTopPanel.Name = "flowTopPanel";
            this.flowTopPanel.Size = new System.Drawing.Size(1234, 100);
            this.flowTopPanel.TabIndex = 0;
            // 
            // panelOptMouse
            // 
            this.panelOptMouse.Location = new System.Drawing.Point(3, 3);
            this.panelOptMouse.Name = "panelOptMouse";
            this.panelOptMouse.Size = new System.Drawing.Size(417, 83);
            this.panelOptMouse.TabIndex = 0;
            // 
            // picBtnMirrorV
            // 
            this.picBtnMirrorV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnMirrorV.Image = global::Drawing_program_inkscape_2.Properties.Resources.Mirror_V;
            this.picBtnMirrorV.Location = new System.Drawing.Point(14, 48);
            this.picBtnMirrorV.Name = "picBtnMirrorV";
            this.picBtnMirrorV.Size = new System.Drawing.Size(31, 30);
            this.picBtnMirrorV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnMirrorV.TabIndex = 12;
            this.picBtnMirrorV.TabStop = false;
            this.picBtnMirrorV.Click += new System.EventHandler(this.picBtnMirrorV_Click);
            // 
            // picBtnRotateLeft
            // 
            this.picBtnRotateLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnRotateLeft.Image = global::Drawing_program_inkscape_2.Properties.Resources.RotateLeft;
            this.picBtnRotateLeft.Location = new System.Drawing.Point(14, 12);
            this.picBtnRotateLeft.Name = "picBtnRotateLeft";
            this.picBtnRotateLeft.Size = new System.Drawing.Size(31, 30);
            this.picBtnRotateLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnRotateLeft.TabIndex = 12;
            this.picBtnRotateLeft.TabStop = false;
            this.picBtnRotateLeft.Click += new System.EventHandler(this.picBtnRotateLeft_Click);
            // 
            // picBtnRotateRight
            // 
            this.picBtnRotateRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnRotateRight.Image = global::Drawing_program_inkscape_2.Properties.Resources.RotateRight;
            this.picBtnRotateRight.Location = new System.Drawing.Point(51, 12);
            this.picBtnRotateRight.Name = "picBtnRotateRight";
            this.picBtnRotateRight.Size = new System.Drawing.Size(31, 30);
            this.picBtnRotateRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnRotateRight.TabIndex = 12;
            this.picBtnRotateRight.TabStop = false;
            this.picBtnRotateRight.Click += new System.EventHandler(this.picBtnRotateRight_Click);
            // 
            // panelOptLine
            // 
            this.panelOptLine.Controls.Add(this.numLineY2);
            this.panelOptLine.Controls.Add(this.numLineX2);
            this.panelOptLine.Controls.Add(this.numLineY1);
            this.panelOptLine.Controls.Add(this.numLineLength);
            this.panelOptLine.Controls.Add(this.numLineX1);
            this.panelOptLine.Controls.Add(this.numLineWidth);
            this.panelOptLine.Controls.Add(this.label4);
            this.panelOptLine.Controls.Add(this.label6);
            this.panelOptLine.Controls.Add(this.label5);
            this.panelOptLine.Controls.Add(this.label2);
            this.panelOptLine.Location = new System.Drawing.Point(426, 3);
            this.panelOptLine.Name = "panelOptLine";
            this.panelOptLine.Size = new System.Drawing.Size(516, 83);
            this.panelOptLine.TabIndex = 1;
            // 
            // numLineY2
            // 
            this.numLineY2.DecimalPlaces = 2;
            this.numLineY2.Location = new System.Drawing.Point(404, 58);
            this.numLineY2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineY2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLineY2.Name = "numLineY2";
            this.numLineY2.Size = new System.Drawing.Size(83, 22);
            this.numLineY2.TabIndex = 4;
            // 
            // numLineX2
            // 
            this.numLineX2.DecimalPlaces = 2;
            this.numLineX2.Location = new System.Drawing.Point(315, 58);
            this.numLineX2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineX2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLineX2.Name = "numLineX2";
            this.numLineX2.Size = new System.Drawing.Size(83, 22);
            this.numLineX2.TabIndex = 4;
            // 
            // numLineY1
            // 
            this.numLineY1.DecimalPlaces = 2;
            this.numLineY1.Location = new System.Drawing.Point(404, 30);
            this.numLineY1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineY1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLineY1.Name = "numLineY1";
            this.numLineY1.Size = new System.Drawing.Size(83, 22);
            this.numLineY1.TabIndex = 3;
            // 
            // numLineLength
            // 
            this.numLineLength.DecimalPlaces = 2;
            this.numLineLength.Location = new System.Drawing.Point(101, 58);
            this.numLineLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineLength.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLineLength.Name = "numLineLength";
            this.numLineLength.Size = new System.Drawing.Size(83, 22);
            this.numLineLength.TabIndex = 3;
            // 
            // numLineX1
            // 
            this.numLineX1.DecimalPlaces = 2;
            this.numLineX1.Location = new System.Drawing.Point(315, 30);
            this.numLineX1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineX1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLineX1.Name = "numLineX1";
            this.numLineX1.Size = new System.Drawing.Size(83, 22);
            this.numLineX1.TabIndex = 3;
            // 
            // numLineWidth
            // 
            this.numLineWidth.DecimalPlaces = 2;
            this.numLineWidth.Location = new System.Drawing.Point(101, 31);
            this.numLineWidth.Name = "numLineWidth";
            this.numLineWidth.Size = new System.Drawing.Size(83, 22);
            this.numLineWidth.TabIndex = 2;
            this.numLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Line Width:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(428, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Line length:";
            // 
            // panelOptErase
            // 
            this.panelOptErase.Controls.Add(this.label3);
            this.panelOptErase.Location = new System.Drawing.Point(948, 3);
            this.panelOptErase.Name = "panelOptErase";
            this.panelOptErase.Size = new System.Drawing.Size(227, 83);
            this.panelOptErase.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Erase options";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 663);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.flowTopPanel);
            this.Controls.Add(this.panelRight2);
            this.Controls.Add(this.flowLay_Tools);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_bottom);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLay_Tools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_mousetool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_linetool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_erasetool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbtn_HandTool)).EndInit();
            this.panel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.panelRight2.ResumeLayout(false);
            this.flowTopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnMirrorV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRotateLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRotateRight)).EndInit();
            this.panelOptLine.ResumeLayout(false);
            this.panelOptLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineWidth)).EndInit();
            this.panelOptErase.ResumeLayout(false);
            this.panelOptErase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picbtn_mousetool;
        private System.Windows.Forms.PictureBox picbtn_linetool;
        private System.Windows.Forms.PictureBox picbtn_erasetool;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInWallDesignerToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLay_Tools;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Panel panelRight2;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.FlowLayoutPanel flowTopPanel;
        private System.Windows.Forms.Panel panelOptMouse;
        private System.Windows.Forms.Panel panelOptLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelOptErase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numLineWidth;
        private System.Windows.Forms.NumericUpDown numLineX1;
        private System.Windows.Forms.NumericUpDown numLineY2;
        private System.Windows.Forms.NumericUpDown numLineX2;
        private System.Windows.Forms.NumericUpDown numLineY1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numLineLength;
        private System.Windows.Forms.PictureBox picBtnRotateRight;
        private System.Windows.Forms.PictureBox picBtnRotateLeft;
        private System.Windows.Forms.PictureBox picBtnMirrorV;
        private System.Windows.Forms.PictureBox picbtn_HandTool;
    }
}

