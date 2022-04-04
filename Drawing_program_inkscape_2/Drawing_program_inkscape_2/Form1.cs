using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Drawing_program_inkscape_2
{
    public partial class Form1 : Form
    {
        ToolName selectedTool = ToolName.MouseTool;
        List<int> selectedLines_sortedIndex = new List<int>();
        List<LineSegment> allLines = new List<LineSegment>();
        List<LineSegment> clipBoard = new List<LineSegment>();
        float zoom = 1;
        MouseState curMouseState = MouseState.None;
        bool mouseIsDown = false;
        PointF workpoint1 = new PointF();
        private PointF currentMousePos;
        int lineBeingChanged = -1;
        PointF drawingSize=new PointF(300, 300);
        float gridSpacing = 10;

        // The "size" of an object for mouse over purposes.
        private const float object_radius = 3;

        // We're over an object if the distance squared
        // between the mouse and the object is less than this.
        //private const float over_dist_squared = object_radius * object_radius;

        public Form1()
        {
            InitializeComponent();
            NewDrawingInit();
        }

        void NewDrawingInit()
        {
            this.MouseWheel += new MouseEventHandler(picImage_MouseWheel);
            //drawingSize = new PointF(300, 300);

            float spacefactor = 2;
            vScrollBar1.Maximum = Convert.ToInt32(spacefactor * drawingSize.Y / 2);
            vScrollBar1.Minimum = Convert.ToInt32(-spacefactor * drawingSize.Y / 2);
            hScrollBar1.Maximum = Convert.ToInt32(spacefactor * drawingSize.X / 2);
            hScrollBar1.Minimum = Convert.ToInt32(-spacefactor * drawingSize.X / 2);

            ChangeZoomFactor(1);
        }
        
        float over_dist_squared()
        {
            return object_radius* object_radius/ (zoom * zoom);
        }
        enum ToolName
        {  
            MouseTool,
            LineTool,
            EraseTool,
            HandTool
        }
        enum MouseState
        {
            None = 0,
            OverLine = 1,
            OverPoint = 2,
            IsDrawingLine = 3,
            IsMovingLine = 4,
            IsMovingEndpoint = 5,
            Selecting = 6
        }

        #region GUI functions and eventhandlers
        private void picbtn_mousetool_Click(object sender, EventArgs e)
        {
            picbtn_mousetool.BorderStyle = BorderStyle.Fixed3D;
            Control.ControlCollection Children = (sender as Control).Parent.Controls;
            int controlIndex = 0;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is PictureBox)
                {
                    (Children[i] as PictureBox).BorderStyle = BorderStyle.None;

                    if (Children[i].Equals(sender))
                    {
                        controlIndex = i;
                        (Children[i] as PictureBox).BorderStyle = BorderStyle.Fixed3D;
                    }
                }
            }
            lblShow.Text = "Tool " + controlIndex + " selected";

            panelOptMouse.Visible = false;
            panelOptErase.Visible = false;
            panelOptLine.Visible = false;
            switch (controlIndex)
            {
                case 0:
                    selectedTool = ToolName.MouseTool;
                    panelOptMouse.Visible = true;
                    break;
                case 1:
                    selectedTool = ToolName.LineTool;
                    panelOptLine.Visible = true;
                    break;
                case 2:
                    selectedTool = ToolName.EraseTool;
                    panelOptErase.Visible = true;
                    break;
                case 3:
                    selectedTool = ToolName.HandTool;
                    break;
            }
            curMouseState = MouseState.None;

        }

        private void picImage_MouseWheel(object sender, MouseEventArgs e)
        {
            float factor = 1 + Convert.ToSingle(e.Delta) / 1000;
            ChangeZoomFactor(factor);
        }

        void ChangeZoomFactor(float factor)
        {
            // remember scrollcenter
            int center_h = GetScrollCenter(hScrollBar1);
            int center_v = GetScrollCenter(vScrollBar1);

            PointF Mouse_PixelCoord = CoordToPixel(currentMousePos);

            // change the scroll factor
            zoom *= factor;

            // change size of the scroll bar indicator
            int verticalScrollRange = Convert.ToInt32(picCanvas.Height / zoom);
            vScrollBar1.LargeChange = verticalScrollRange;
            int horisontalScrollRange = Convert.ToInt32(picCanvas.Width / zoom);
            hScrollBar1.LargeChange = horisontalScrollRange;


            try
            {
                // Set the scrollbar to the correct position
                hScrollBar1.Value = center_h - hScrollBar1.LargeChange / 2;// - Convert.ToInt32(offset.X) * 2;
                vScrollBar1.Value = center_v - vScrollBar1.LargeChange / 2;// - Convert.ToInt32(offset.Y)*2;


                // Adjust the position to make the cursor stay in the same place relative to the drawing.
                PointF newmousePos = PixelToCoord(Mouse_PixelCoord);
                PointF offset = new PointF(newmousePos.X - currentMousePos.X, newmousePos.Y - currentMousePos.Y);
                hScrollBar1.Value -= Convert.ToInt32(offset.X);
                vScrollBar1.Value -= Convert.ToInt32(offset.Y);
                //PointF offset = new PointF((center_h - currentMousePos.X)*factor, (center_v - currentMousePos.Y)*factor);
                //hScrollBar1.Value += Convert.ToInt32(offset.X);
                //vScrollBar1.Value += Convert.ToInt32(offset.Y);

            }
            catch (Exception)
            {
                hScrollBar1.Value = 0 - hScrollBar1.LargeChange / 2;
                vScrollBar1.Value = 0 - vScrollBar1.LargeChange / 2;
            }


            


            // ------ Set grid spacing ----- //

            double adjustFactor = 1.5;
            double log10 = -Math.Log(zoom, 10) + adjustFactor; // 10^?
            double log10Mod = (log10 % 1); // Are we in between two multiples of 10?
                   gridSpacing = Convert.ToSingle(Math.Pow(10, Math.Floor(log10)));
            if (log10Mod < 0) log10Mod++;
            if (log10Mod > 0.699 ) // use a multiple of 5
            {
                gridSpacing *= 5;
            }
            else if (log10Mod > 0.301 ) // use a multiple of 2
            {
                gridSpacing *= 2;
            }
            //gridSpacing = Convert.ToSingle(Math.Pow(10, -log10));

            RedrawGridlines();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            // Drawing the backgroundcolor for the drawing area
            PointF cornerDR = CoordToPixel(drawingSize);
            PointF cornerUL = CoordToPixel(new PointF(0,0));
            Brush canvasBrush = new SolidBrush(Color.FromArgb(64, 64, 64, 64));
            e.Graphics.FillRectangle(canvasBrush, cornerUL.X, cornerUL.Y, cornerDR.X - cornerUL.X, cornerDR.Y - cornerUL.Y);


            // drawing the selection square
            if (curMouseState == MouseState.Selecting)
            {
                PointF startPoint = new PointF();
                startPoint.X = Math.Min(workpoint1.X, currentMousePos.X);
                startPoint.Y = Math.Min(workpoint1.Y, currentMousePos.Y);
                startPoint = CoordToPixel(startPoint);

                PointF endPoint = new PointF();
                endPoint.X = Math.Max(workpoint1.X, currentMousePos.X);
                endPoint.Y = Math.Max(workpoint1.Y, currentMousePos.Y);
                endPoint = CoordToPixel(endPoint);

                PointF rSize = new PointF();
                rSize.X = Math.Abs(endPoint.X - startPoint.X);
                rSize.Y = Math.Abs(endPoint.Y - startPoint.Y);

                //lblShow.Text = rSize.ToString();

                
                //rSize = CoordToPixel(rSize);
                Brush transparentBrush = new SolidBrush(Color.FromArgb(128, 186, 230, 255));
                RectangleF rect = new RectangleF(
                      startPoint.X, startPoint.Y, rSize.X, rSize.Y);
                e.Graphics.FillRectangle(transparentBrush, rect);
            }

            //if (allLines.Count < 1) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            PointF move = SnapToGrid(new PointF(currentMousePos.X - workpoint1.X, currentMousePos.Y - workpoint1.Y));
            SizeF mouseDistMoved = new SizeF(move.X,move.Y);
            //int nLinesToDraw = allLines.Count;
            

            int selectedLineCounter = 0;
            // Draw the segments.
            for (int i = 0; i < allLines.Count; i++)
            {
                bool lineIsSelected = false;
                if (selectedLineCounter < selectedLines_sortedIndex.Count)
                {
                    if (i == selectedLines_sortedIndex[selectedLineCounter]) // we found a selected line
                    {
                        selectedLineCounter++;
                        lineIsSelected = true;
                    }
                }
                if (lineIsSelected) // line is selected
                {

                    PointF lineStart = allLines[i].P1;
                    PointF lineEnd = allLines[i].P2;

                    if (curMouseState == MouseState.IsMovingLine) // If we are moving lines, display the lines with an added offset
                    {
                        lineStart = PointF.Add(allLines[i].P1, mouseDistMoved);
                        lineEnd = PointF.Add(allLines[i].P2, mouseDistMoved);
                    }
                    lineStart = CoordToPixel(lineStart);
                    lineEnd = CoordToPixel(lineEnd);

                    // Draw the end points.
                    RectangleF rect = new RectangleF(
                    lineStart.X - object_radius, lineStart.Y - object_radius,
                        2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);
                    // Draw the end points.
                    rect = new RectangleF(
                        lineEnd.X - object_radius, lineEnd.Y - object_radius,
                            2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);

                    // Draw the segment.
                    Pen thickPen = new Pen(allLines[i].LineColor, allLines[i].LineThickness * zoom);
                    //PointF End1 = CoordToPixel(allLines[i].P1);
                    //PointF End2 = CoordToPixel(allLines[i].P2);
                    e.Graphics.DrawLine(Pens.Blue, lineStart, lineEnd);
                    //e.Graphics.DrawLine(thickPen, End1, End2);
                }
                else // line not selected
                {
                    // Draw the segment.
                    Pen thickPen = new Pen(allLines[i].LineColor, allLines[i].LineThickness * zoom);
                    PointF End1 = CoordToPixel(allLines[i].P1);
                    PointF End2 = CoordToPixel(allLines[i].P2);
                    //e.Graphics.DrawLine(Pens.Blue, End1, End2);
                    e.Graphics.DrawLine(thickPen, End1, End2);
                }
                
                
            }

            
            // If there's a new segment under constructions, draw it red.
            
                if (curMouseState == MouseState.IsDrawingLine)
                {
                    PointF lineStart = CoordToPixel(workpoint1);
                    PointF lineEnd = CoordToPixel(SnapToGrid(currentMousePos));
                    e.Graphics.DrawLine(Pens.Red, lineStart, lineEnd);
                }
                
            
            

        }
        #endregion

        //        ctrl - merk av og på
        //mouse down:	
        //    Hvis over selected line: flytt alle markerte
        //        Hvis over endepunkt: Flytt endepunkt
        //    Hvis over umarkert linje: marker linjen
        //    Hvis over ingenting:	Start firkantmarkering
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            PointF mouseAbsPos = PixelToCoord(e.Location);


            // finding the line under the mousepointer
            bool keepLookingFlag = true;
            int clickedLine = -1;
            int selectedLineCounter = 0;
            for (int i = 0; i < allLines.Count && keepLookingFlag; i++)
            {
                bool lineIsSelected = false;
                if (selectedLineCounter < selectedLines_sortedIndex.Count)
                {
                    if (i == selectedLines_sortedIndex[selectedLineCounter]) // we found a selected line
                    {
                        selectedLineCounter++;
                        lineIsSelected = true;
                    }
                }
                PointF closest;
                bool lineIsOverMouse = allLines[i].FindDistanceToSegmentSquared(mouseAbsPos, out closest) < over_dist_squared();

                if (lineIsSelected && lineIsOverMouse) // line is selected (and close)
                {
                    keepLookingFlag = false;
                    clickedLine = i;
                }
                else if(!lineIsSelected && lineIsOverMouse)  // line not selected (but line is close)
                {
                    clickedLine = i;
                }
            }


            // ----------------

            if (e.Button == MouseButtons.Left) // -- Left click -- //
            {
                mouseIsDown = true;
                // --- Line Tool --- //

                if (selectedTool == ToolName.LineTool && curMouseState != MouseState.IsDrawingLine)
                {
                    PointF snappedPos = SnapToGrid(mouseAbsPos);
                    //allLines.Add(new LineSegment(snappedPos, snappedPos, Convert.ToSingle(numLineWidth.Value)));
                    workpoint1 = snappedPos;
                    curMouseState = MouseState.IsDrawingLine;
                }
                //else if (selectedTool == ToolName.LineTool && curMouseState == MouseState.IsDrawingLine)
                //{
                //    curMouseState = MouseState.None;
                //}


                // --- Mouse Tool --- //
                if (selectedTool == ToolName.MouseTool)
                {
                    
                    if (clickedLine != -1) // Clicking on a line
                    {
                        bool lineIsSelected = selectedLines_sortedIndex.Contains(clickedLine);

                        if (Form.ModifierKeys == Keys.Control) // Ctrl button is down
                        {
                            // Do Ctrl-Left Click Work
                            selectedLines_sortedIndex.Remove(clickedLine); // Deselect Line
                        }
                        

                        if (lineIsSelected) // clicking on a selected line
                        {
                            workpoint1 = mouseAbsPos;
                            bool overP1 = DistSquared(mouseAbsPos, allLines[clickedLine].P1) < over_dist_squared();
                            bool overP2 = DistSquared(mouseAbsPos, allLines[clickedLine].P2) < over_dist_squared();
                            if (overP1 || overP2) // moving the endpoint
                            {
                                curMouseState = MouseState.IsMovingEndpoint;
                                lineBeingChanged = clickedLine;

                                if (overP1) // Dette gjør at vi senere slipper å bry oss om hvilken ende vi endrer
                                {
                                    // swap the end points
                                    PointF temp = allLines[clickedLine].P1;
                                    allLines[clickedLine].P1 = allLines[clickedLine].P2;
                                    allLines[clickedLine].P2 = temp;
                                }

                            }
                            else // moving all selected lines
                            {
                                curMouseState = MouseState.IsMovingLine;
                            }
                            
                        }
                        else // selecting a new line
                        {
                            if (Form.ModifierKeys == Keys.Control) // Ctrl button is down
                            {
                                // Do Ctrl-Left Click Work
                                selectedLines_sortedIndex.Add(clickedLine);
                                selectedLines_sortedIndex.Sort();
                            }
                            else
                            {
                                selectedLines_sortedIndex = new List<int> { clickedLine };
                            }
                            
                            
                        }
                    }
                    else // --- Not clicking on a line - drawing selectingsquare --- //
                    {
                        if (curMouseState == MouseState.None)
                        {
                            curMouseState = MouseState.Selecting;
                            workpoint1 = mouseAbsPos;
                            lblShow.Text = "startSelecting";
                        }
                        
                    }
                    
                    

                    
                }
                else if (selectedTool == ToolName.EraseTool)
                {
                    if (clickedLine != -1)
                    {
                        allLines.RemoveAt(clickedLine);
                        selectedLines_sortedIndex.Clear();
                    }
                }
                else if (selectedTool == ToolName.HandTool)
                {
                    workpoint1 = currentMousePos;
                }


                picCanvas.Invalidate();
            }
            else // -- Right click -- //
            {
                if (selectedTool == ToolName.LineTool && curMouseState == MouseState.IsDrawingLine)
                {
                    curMouseState = MouseState.None;
                    //allLines.RemoveAt(allLines.Count - 1);
                }
            } // Right Click
            
        }

        

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            PointF mouseAbsPos = PixelToCoord(e.Location);
            currentMousePos = mouseAbsPos;
            if (selectedTool == ToolName.LineTool && curMouseState == MouseState.IsDrawingLine)
            {
                //allLines.Last().P2 = SnapToGrid( mouseAbsPos);
                PointF snappedPos = SnapToGrid(currentMousePos);
                numLineX1.Value = Convert.ToDecimal( workpoint1.X);
                numLineY1.Value = Convert.ToDecimal(workpoint1.Y);
                numLineX2.Value = Convert.ToDecimal(snappedPos.X);
                numLineY2.Value = Convert.ToDecimal(snappedPos.Y);
                numLineLength.Value = Convert.ToDecimal(Math.Sqrt(DistSquared(snappedPos,workpoint1)));

            }
            else if (selectedTool == ToolName.MouseTool && curMouseState == MouseState.IsMovingEndpoint)
            {
                allLines[lineBeingChanged].P2 = SnapToGrid(mouseAbsPos);
            }
            else if (selectedTool == ToolName.EraseTool && mouseIsDown)
            {
                int cover_Line = -1;
                
                for (int i = 0; i < allLines.Count ; i++)
                {
                    PointF closest;
                    if (allLines[i].FindDistanceToSegmentSquared(mouseAbsPos, out closest) < over_dist_squared())
                    {
                        allLines.RemoveAt(i);
                    }
                }
                selectedLines_sortedIndex.Clear();
                
            }
            else if (selectedTool == ToolName.HandTool && mouseIsDown)
            {
                int newVpos = vScrollBar1.Value + Convert.ToInt32(workpoint1.Y - currentMousePos.Y);
                vScrollBar1.Value = Math.Min(vScrollBar1.Maximum - 1, Math.Max(vScrollBar1.Minimum + 1, newVpos));

                int newHpos = hScrollBar1.Value + Convert.ToInt32(workpoint1.X - currentMousePos.X);
                hScrollBar1.Value = Math.Min(hScrollBar1.Maximum - 1, Math.Max(hScrollBar1.Minimum + 1, newHpos));
            }
            picCanvas.Invalidate();

            
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // -- Left click -- //
            {
                PointF mouseAbsPos = PixelToCoord(e.Location);
                mouseIsDown = false;
                if (selectedTool == ToolName.MouseTool)
                {
                    if (curMouseState == MouseState.Selecting)
                    {
                        curMouseState = MouseState.None;
                        PointF rectangleStart = workpoint1;
                        PointF rectangleEnd = mouseAbsPos;
                        selectedLines_sortedIndex.Clear();

                        for (int i = 0; i < allLines.Count; i++) // --- selecting the lines inside the rectangle --- // 
                        {
                            bool smallerThanMax_x = Math.Max(allLines[i].X1, allLines[i].X2) < Math.Max(rectangleStart.X, rectangleEnd.X);
                            bool biggerThanMin_x = Math.Min(allLines[i].X1, allLines[i].X2) > Math.Min(rectangleStart.X, rectangleEnd.X);
                            bool smallerThanMax_y = Math.Max(allLines[i].Y1, allLines[i].Y2) < Math.Max(rectangleStart.Y, rectangleEnd.Y);
                            bool biggerThanMin_y = Math.Min(allLines[i].Y1, allLines[i].Y2) > Math.Min(rectangleStart.Y, rectangleEnd.Y);


                            if (smallerThanMax_x && biggerThanMin_x && smallerThanMax_y && biggerThanMin_y) // the line is inside the rectangle
                            {
                                selectedLines_sortedIndex.Add(i);
                            }
                        }
                        selectedLines_sortedIndex.Sort();
                    }
                    else if (curMouseState == MouseState.IsMovingLine)
                    {
                        //PointF move = SnapToGrid(new PointF(currentMousePos.X - workpoint1.X, currentMousePos.Y - workpoint1.Y));
                        //SizeF mouseDistMoved = new SizeF(move.X, move.Y);

                        curMouseState = MouseState.None;
                        PointF mouseDistMoved = SnapToGrid(new PointF(currentMousePos.X - workpoint1.X, currentMousePos.Y - workpoint1.Y));
                        foreach (int linenum in selectedLines_sortedIndex)
                        {
                            allLines[linenum].X1 += mouseDistMoved.X;
                            allLines[linenum].X2 += mouseDistMoved.X;
                            allLines[linenum].Y1 += mouseDistMoved.Y;
                            allLines[linenum].Y2 += mouseDistMoved.Y;
                        }
                    }
                    else if (curMouseState == MouseState.IsMovingEndpoint)
                    {
                       
                        curMouseState = MouseState.None;
                    }
                }
                else if (selectedTool == ToolName.LineTool)
                {
                    if (curMouseState == MouseState.IsDrawingLine)
                    {
                        PointF snappedPos = SnapToGrid(currentMousePos);

                        if (Math.Abs(snappedPos.X - workpoint1.X) > 0.0001 || Math.Abs(snappedPos.Y - workpoint1.Y) > 0.0001) // Do not create extremely small lines. Only big ones.
                        {
                            allLines.Add(new LineSegment(workpoint1, SnapToGrid(currentMousePos)));
                        }
                        
                        curMouseState = MouseState.None;
                    }
                    
                }
                else if (selectedTool == ToolName.HandTool)
                {
                    RedrawGridlines();
                }
            }
        }

        private PointF SnapToGrid(PointF inputCoord)
        {
            double x = gridSpacing * (int)Math.Round(inputCoord.X / gridSpacing);
            double y = gridSpacing * (int)Math.Round(inputCoord.Y / gridSpacing);
            return new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
        }

        PointF CoordToPixel(PointF position)
        {
            int vSliderOffset = GetScrollCenter(vScrollBar1); 
            int hSliderOffset = GetScrollCenter(hScrollBar1);

            float pixelPosX = Convert.ToSingle((position.X - drawingSize.X/2 - hSliderOffset) * zoom);
            float pixelPosY = Convert.ToSingle((position.Y - drawingSize.Y/2 - vSliderOffset) * zoom);

            return new PointF(pixelPosX + picCanvas.Width/2, pixelPosY + picCanvas.Height / 2);
        }
        PointF PixelToCoord(PointF position)
        {
            int vSliderOffset = GetScrollCenter(vScrollBar1);
            int hSliderOffset = GetScrollCenter(hScrollBar1);

            float coordX = Convert.ToSingle((position.X - picCanvas.Width / 2) / zoom);
            float coordY = Convert.ToSingle((position.Y - picCanvas.Height / 2) / zoom);
            return new PointF(coordX + drawingSize.X / 2 + hSliderOffset, coordY + drawingSize.Y / 2 + vSliderOffset);
        }
        
        private float DistSquared(PointF pt1, PointF pt2)
        {
            float dx = pt1.X - pt2.X;
            float dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        int GetScrollCenter(ScrollBar inputBar)
        {
            //lblShow.Text = Convert.ToInt32(inputBar.Value + inputBar.LargeChange / 2).ToString();
            return Convert.ToInt32(inputBar.Value + inputBar.LargeChange / 2);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //lblShow.Text = vScrollBar1.Value.ToString();
            RedrawGridlines();
        }

        void RedrawGridlines()
        {
            Bitmap gridlines = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics gr = Graphics.FromImage(gridlines))
            {
                float realSpacing = gridSpacing * zoom;
                Pen gridLinePen = new Pen(Color.FromArgb(32,32,32,32), 0.01f);
                Pen gridLinePen_thick = new Pen(Color.FromArgb(64, 32, 32, 32), 1f);
                PointF origo = CoordToPixel(new PointF(0, 0));

                int i_x = -Convert.ToInt32(Math.Floor(origo.X / realSpacing)); // the minimum value
                int i_maxX = Convert.ToInt32(Math.Floor((picCanvas.Width - origo.X) / realSpacing)); // the maximum value
                while (i_x <= i_maxX)
                {
                    Pen curPen;
                    if (i_x % 5 == 0) curPen = gridLinePen_thick;
                    else  curPen = gridLinePen;
                    gr.DrawLine(curPen, origo.X + i_x*realSpacing, 0, origo.X + i_x *realSpacing, gridlines.Height);
                    i_x++;
                }

                int i_y = -Convert.ToInt32(Math.Floor(origo.Y / realSpacing)); // the minimum value
                int i_maxY = Convert.ToInt32(Math.Floor((picCanvas.Width - origo.Y) / realSpacing)); // the maYimum value
                while (i_y <= i_maxY)
                {
                    Pen curPen;
                    if (i_y % 5 == 0) curPen = gridLinePen_thick;
                    else curPen = gridLinePen;
                    gr.DrawLine(curPen, 0, origo.Y + i_y * realSpacing, gridlines.Width, origo.Y + i_y * realSpacing );
                    i_y++;
                }

            }

            picCanvas.BackgroundImage = gridlines;
            picCanvas.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "svg files (*.svg)|*.svg|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            //saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                string filecontent = SVGmethods.LinesToSvg(allLines, new PointF(drawingSize.X, drawingSize.Y));
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(filecontent);
                    
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.InitialDirectory = @"C: \Users\bvtv\Desktop\";
                openFileDialog.Filter = "svg files (*.svg)|*.svg|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    //filePath = @"C: \Users\bvtv\Desktop\testfil.svg";
                    //filePath = @"C: \Users\bvtv\Downloads\MinimalSVG_inkscape.svg";
                    using (StreamReader reader =  File.OpenText(filePath))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    List<LineSegment> importedLines;
                    
                    SVGmethods.GetSvgContent(fileContent, out importedLines,out drawingSize);

                    allLines = importedLines;
                    NewDrawingInit();

                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ----- Ctrl + C ----- //
            if (e.Control && e.KeyCode == Keys.C)
            {
                //MessageBox.Show("Ctrl + C");
                List<LineSegment> copiedLines = new List<LineSegment>();
                foreach (var item in selectedLines_sortedIndex)
                {
                    copiedLines.Add(allLines[item].GetCopy());
                }
                clipBoard = copiedLines;
            }
            // ----- Ctrl + V ----- //
            else if (e.Control && e.KeyCode == Keys.V)
            {
                int start = allLines.Count;
                foreach (LineSegment item in clipBoard)
                {
                    allLines.Add(item.GetCopy());
                }
                //allLines.AddRange(clipBoard);
                //allLines.CopyTo()
                int end = allLines.Count;
                selectedLines_sortedIndex.Clear();
                for (int i = start; i < end; i++)
                {
                    selectedLines_sortedIndex.Add(i);
                }
                workpoint1 = new PointF( clipBoard[0].X1, clipBoard[0].Y1);
                curMouseState = MouseState.IsMovingLine;
            }
            else if ( e.KeyCode == Keys.Delete)
            {
                for (int i = selectedLines_sortedIndex.Count-1; i >= 0; i--)
                {
                    allLines.RemoveAt(selectedLines_sortedIndex[i]);
                }
                selectedLines_sortedIndex.Clear();
                
            }
            else if (e.KeyCode == Keys.Escape)
            {
                curMouseState = MouseState.None;
            }


            picCanvas.Invalidate();
        }

        private void picBtnRotateRight_Click(object sender, EventArgs e)
        {
            rotateSelection(90);
        }

        private void picBtnRotateLeft_Click(object sender, EventArgs e)
        {
            rotateSelection(-90);
        }

        void rotateSelection(float degrees)
        {
            double radians = degrees / 180 * Math.PI;

            List<LineSegment> selectedLines = new List<LineSegment>();
            int selectedLineCounter = 0;
            // Draw the segments.
            for (int i = 0; i < allLines.Count; i++)
            {
                if (selectedLineCounter < selectedLines_sortedIndex.Count)
                {
                    if (i == selectedLines_sortedIndex[selectedLineCounter]) // we found a selected line
                    {
                        selectedLineCounter++;
                        selectedLines.Add(allLines[i]);
                    }
                }
            }
            RectangleF area = GetCoveredArea(selectedLines);
            PointF Centerpoint = new PointF((area.Width + area.X) / 2, (area.Height + area.Y) / 2);

            foreach (LineSegment item in selectedLines)
            {
                item.P1 = RotatePoint(item.P1, Centerpoint, radians);
                item.P2 = RotatePoint(item.P2, Centerpoint, radians);
            }


            picCanvas.Invalidate();
        }
        RectangleF GetCoveredArea(List<LineSegment> segments)
        {
            if (segments.Count <= 0)
            {
                return new RectangleF(0,0,0,0);
            }
            
            float xMin = segments[0].X1, xMax = segments[0].X1, yMin = segments[0].Y1, yMax = segments[0].Y1;
            foreach (LineSegment item in segments)
            {
                if (Math.Max(item.X1, item.X2) > xMax) xMax = Math.Max(item.X1, item.X2);
                if (Math.Min(item.X1, item.X2) < xMin) xMin = Math.Min(item.X1, item.X2);
                if (Math.Max(item.Y1, item.Y2) > yMax) yMax = Math.Max(item.Y1, item.Y2);
                if (Math.Min(item.Y1, item.Y2) < yMin) yMin = Math.Min(item.Y1, item.Y2);
            }
            return new RectangleF(xMin, yMin, xMax, yMax);
        }

        /// <summary>
        /// Rotates one point around another
        /// </summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The center point of rotation.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <returns>Rotated point</returns>
        static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, double radians)
        {
            
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);
            return new PointF
            {
                X =
                    (float)(cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (float)(sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        private void picBtnMirrorV_Click(object sender, EventArgs e)
        {
            List<LineSegment> selectedLines = new List<LineSegment>();
            int selectedLineCounter = 0;
            // Draw the segments.
            for (int i = 0; i < allLines.Count; i++)
            {
                if (selectedLineCounter < selectedLines_sortedIndex.Count)
                {
                    if (i == selectedLines_sortedIndex[selectedLineCounter]) // we found a selected line
                    {
                        selectedLineCounter++;
                        selectedLines.Add(allLines[i]);
                    }
                }
            }
            RectangleF area = GetCoveredArea(selectedLines);
            float Centerline = (area.Width + area.X) / 2;

            foreach (LineSegment item in selectedLines)
            {
                item.X1 += -(item.X1 - Centerline) * 2;
                item.X2 += -(item.X2 - Centerline) * 2;
                //item.P1 = RotatePoint(item.P1, Centerpoint, radians);
                //item.P2 = RotatePoint(item.P2, Centerpoint, radians);
            }


            picCanvas.Invalidate();
        }
    }

    public class LineSegment
    {
        private float x1, y1, x2, y2;
        private float lineThickness;
        private Color lineColor;
        public LineSegment(float x1, float y1, float x2, float y2, float thickness = 1)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.lineThickness = thickness;
            lineColor = Color.Blue;
        }
        public LineSegment(PointF p1, PointF p2, float thickness = 1)
        {
            this.x1 = p1.X;
            this.y1 = p1.Y;
            this.x2 = p2.X;
            this.y2 = p2.Y;
            this.lineThickness = thickness;
            this.lineColor = Color.Blue;
        }

        public LineSegment GetCopy()
        {
            LineSegment temp = new LineSegment(x1, y1, x2, y2, lineThickness);
            temp.lineColor = lineColor;
            return temp;
        }
        public float X1
        {
            get { return x1; }
            set { x1 = value; }
        }
        public float Y1
        {
            get { return y1; }
            set { y1 = value; }
        }
        public float X2
        {
            get { return x2; }
            set { x2 = value; }
        }
        public float Y2
        {
            get { return y2; }
            set { y2 = value; }
        }
        public float LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }

        }
        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public PointF P1
        {
            get { return new PointF(x1, y1); }
            set
            {
                x1 = value.X;
                y1 = value.Y;
            }
        }
        public PointF P2
        {
            get { return new PointF(x2, y2); }
            set
            {
                x2 = value.X;
                y2 = value.Y;
            }
        }

        public float Length
        {
            get
            {
                float distsquared = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
                return Convert.ToSingle(Math.Sqrt(distsquared));
            }
        }

        public float Slope
        {
            get
            {
                return (y2 - y1) / (x2 - x1);
            }
        }

        public float Angle
        {
            get
            {
                return Convert.ToSingle(Math.Atan2(y2 - y1, x2 - x1));
            }
        }
        public double FindDistanceToSegmentSquared(PointF pt, out PointF closest)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = new PointF(x1, y1);
                dx = pt.X - x1;
                dy = pt.Y - y1;
                return dx * dx + dy * dy;
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - x1) * dx + (pt.Y - y1) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(x1, y1);
                dx = pt.X - x1;
                dy = pt.Y - y1;
            }
            else if (t > 1)
            {
                closest = new PointF(x2, y2);
                dx = pt.X - x2;
                dy = pt.Y - y2;
            }
            else
            {
                closest = new PointF(x1 + t * dx, y1 + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }
    }

    public static class SVGmethods
    {
        public static string LinesToSvg(List<LineSegment> lines, PointF canvasSize)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";


            string lineSeg_svg = "";
            foreach (LineSegment item in lines)
            {
                Dictionary<string,string> _propertyPairs = new Dictionary<string, string> (){
                    { "x1", item.X1.ToString(nfi) },
                { "x2", item.X2.ToString(nfi) },
                { "y1", item.Y1.ToString(nfi) },
                { "y2", item.Y2.ToString(nfi) },
                { "stroke-width", item.LineThickness.ToString(nfi) },
                { "stroke", item.LineColor.Name }
                };

                lineSeg_svg += "<line " + CreateKeyValuePairs(_propertyPairs) + "/>\n";
            }

            Dictionary<string, string> propertyPairs = new Dictionary<string, string>(){
                    { "xmlns", "http://www.w3.org/2000/svg" },
                { "width", canvasSize.X.ToString(nfi) },
                { "height", canvasSize.Y.ToString(nfi) },
                };

            string svgTag = "<svg " + CreateKeyValuePairs(propertyPairs) + " >\n" + lineSeg_svg + "</svg>";

            return "<?xml version=\"1.0\" encoding=\"utf-8\"?>  \n<!DOCTYPE svg > \n" + svgTag;
        }

        private static string CreateKeyValuePairs(Dictionary<string, string> dict)
        {
            string result = "";
            for (int i = 0; i < dict.Count; i++)
            {
                result += dict.Keys.ElementAt(i) + " = \"" + dict.Values.ElementAt(i) + "\" ";
            }
            return result;
        }

        public static void GetSvgContent(string Svg, out List<LineSegment> imortedLines, out PointF size) // 
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            JSONobject test = new JSONobject(Svg, null);
            //List<JSONobject> nodes = JSONobject.FindChildren(Svg,null);
            List<string> resultLines = test.GetAllByType("line");
            List<string> resultContainers = test.GetAllByType("svg");

           imortedLines = new List<LineSegment>();
            foreach (string item in resultLines)
            {
                // The content in this for loop is inefficient
                float x1 = 0, x2 = 0, y1 = 0, y2 = 0, lineWidth = 0; ;
                for (int i = 0; i < item.Length; i++)
                {
                    string x1_maybe = GetFirstValueIfMatch("x1", item.Substring(i));
                    if (x1_maybe != "") x1 = Convert.ToSingle(x1_maybe, nfi);

                    string x2_maybe = GetFirstValueIfMatch("x2", item.Substring(i));
                    if (x2_maybe != "") x2 = Convert.ToSingle(x2_maybe, nfi);

                    string y1_maybe = GetFirstValueIfMatch("y1", item.Substring(i));
                    if (y1_maybe != "") y1 = Convert.ToSingle(y1_maybe, nfi);

                    string y2_maybe = GetFirstValueIfMatch("y2", item.Substring(i));
                    if (y2_maybe != "") y2 = Convert.ToSingle(y2_maybe, nfi);

                    string lineW_maybe = GetFirstValueIfMatch("stroke-width", item.Substring(i));
                    if (lineW_maybe != "") lineWidth = Convert.ToSingle(lineW_maybe, nfi);

                }
                imortedLines.Add(new LineSegment(x1, y1, x2, y2, lineWidth));
            }

            //resultContainers[0];

            float width = 0, height = 0;
            for (int i = 0; i < resultContainers[0].Length; i++)
            {
                string width_maybe = GetFirstValueIfMatch("width", resultContainers[0].Substring(i));
                if (width_maybe != "") width = Convert.ToSingle(width_maybe, nfi);

                string height_maybe = GetFirstValueIfMatch("height", resultContainers[0].Substring(i));
                if (height_maybe != "") height = Convert.ToSingle(height_maybe, nfi);

            }
            size = new PointF(width, height);
        }

        static string GetFirstValueIfMatch(string searchValue, string searchString)
        {
            bool matchFound = false;
            if (searchString.Length >= searchValue.Length)
            {
                if (searchString.Substring(0, searchValue.Length) == searchValue)
                {
                    matchFound = true;

                    int nAnforselstegn = 0;
                    int i = 0;
                    int startPos = 0;
                    int endPos = 0;
                    while (nAnforselstegn < 2 && i < searchString.Length)
                    {
                        if (searchString[i] == '"')
                        {
                            if (nAnforselstegn == 0)
                            {
                                startPos = i +1;
                            }
                            else
                            {
                                endPos = i;
                            }
                            nAnforselstegn++;
                        }
                        i++;
                    }
                    return searchString.Substring(startPos, endPos - startPos);
                }
                
            }
            return "";

        }
    }
    public class JSONobject
    {
        JSONobject parent;
        List<JSONobject> children;
        string content = "";
        public JSONobject(string data, JSONobject parent)
        {
            this.parent = parent;
            children = FindChildren(data,parent);
        }

        public List<string> GetAllByType(string tagName)
        {
            List<string> linesFound = new List<string>();
            string tagData = this.content.Trim();
            if (tagData.Length >= tagName.Length)
            {
                if (tagData.Substring(0,tagName.Length) == tagName )
                {
                    linesFound.Add(content);
                }
            }
            
            foreach (JSONobject item in children)
            {
                linesFound.AddRange( item.GetAllByType(tagName));
            }
            return linesFound;
        }
        private List<JSONobject> FindChildren(string data, JSONobject parent)
        {

            // Det eneste som mangler er håndtering av escape sekvenser

            char prevchar = ' ';
            int openingBracket = 0;
            int basicListLevel = 0;
            int DOM_level = 0;
            int startOfChild = 0;
            //string value = "";
            bool wrongObject = false;
            bool insideDOMend = false;
            bool domIsSeen = false;
            bool insideString = false;
            List<JSONobject> children = new List<JSONobject>();

            for (int i = 0; i < data.Length; i++)
            {
                if (!insideString)
                {
                    if ((data[i] == '!' || data[i] == '?') && prevchar == '<')
                    {
                        wrongObject = true;
                    }
                    else if (data[i] == '<')
                    {
                        basicListLevel++;
                        openingBracket = i;
                        if (DOM_level == 1)
                        {
                            startOfChild = i;
                        }
                    }
                    else if (data[i] == '>')
                    {
                        basicListLevel--;
                    
                        if (prevchar != '/' && !wrongObject && !insideDOMend) // End of headerblock, start of sublevel
                        {
                            if (DOM_level == 0)
                            {
                                content = data.Substring(openingBracket + 1, i - openingBracket-1);
                                //startOfChild = i+1;
                            }
                            DOM_level++;
                            domIsSeen = true;
                        }
                        else if (insideDOMend && DOM_level == 1) // end of one of the children
                        {
                            children.Add(new JSONobject(data.Substring(startOfChild, i - startOfChild), parent));
                        }
                        insideDOMend = false;
                        wrongObject = false;
                    }

                    if (prevchar == '<' && data[i] == '/') // Found the start of a container end
                    {
                        insideDOMend = true;
                        DOM_level--;
                    
                    }
                    else if (prevchar == '/' && data[i] == '>' && DOM_level == 1) // ending of a single line tag
                    {
                        children.Add(new JSONobject(data.Substring(openingBracket + 1, i - openingBracket - 2), parent));
                    }
                    else if (data[i] == '"')
                    {
                        insideString = true;
                    }

                    if (!String.IsNullOrWhiteSpace(data[i].ToString())) prevchar = data[i];
                }
                else // If inside string
                {
                    if (data[i] == '"')
                    {
                        insideString = false;
                    }
                }
                
            }
            if (!domIsSeen)
            {
                content = data;
            }


            return children;
        }
    }
}
