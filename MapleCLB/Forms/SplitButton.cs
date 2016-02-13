﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MapleCLB.Tools {
    public class SplitButton : Button {
        PushButtonState _state;

        const int SPLIT_SECTION_WIDTH = 18;

        static int borderSize = SystemInformation.Border3DSize.Width * 2;
        bool SkipNextOpen;
        Rectangle DropDownRectangle;
        bool showSplit;

        bool IsSplitMenuVisible;


        ContextMenuStrip MSplitMenuStrip;
        ContextMenu MSplitMenu;

        TextFormatFlags TextFormatFlags = TextFormatFlags.Default;

        public SplitButton() {
            AutoSize = true;
        }

        #region Properties

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip {
            get {
                return SplitMenuStrip;
            }
            set {
                SplitMenuStrip = value;
            }
        }

        [DefaultValue(null)]
        public ContextMenu SplitMenu {
            get { return MSplitMenu; }
            set {
                //remove the event handlers for the old SplitMenu
                if (MSplitMenu != null) {
                    MSplitMenu.Popup -= SplitMenu_Popup;
                }

                //add the event handlers for the new SplitMenu
                if (value != null) {
                    ShowSplit = true;
                    value.Popup += SplitMenu_Popup;
                } else
                    ShowSplit = false;

                MSplitMenu = value;
            }
        }

        [DefaultValue(null)]
        public ContextMenuStrip SplitMenuStrip {
            get {
                return MSplitMenuStrip;
            }
            set {
                //remove the event handlers for the old SplitMenuStrip
                if (MSplitMenuStrip != null) {
                    MSplitMenuStrip.Closing -= SplitMenuStrip_Closing;
                    MSplitMenuStrip.Opening -= SplitMenuStrip_Opening;
                }

                //add the event handlers for the new SplitMenuStrip
                if (value != null) {
                    ShowSplit = true;
                    value.Closing += SplitMenuStrip_Closing;
                    value.Opening += SplitMenuStrip_Opening;
                } else
                    ShowSplit = false;


                MSplitMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowSplit {
            set {
                if (value != showSplit) {
                    showSplit = value;
                    Invalidate();

                    if (Parent != null)
                        Parent.PerformLayout();
                }
            }
        }

        private PushButtonState State {
            get {
                return _state;
            }
            set {
                if (!_state.Equals(value)) {
                    _state = value;
                    Invalidate();
                }
            }
        }

        #endregion Properties

        protected override bool IsInputKey(Keys keyData) {
            if (keyData.Equals(Keys.Down) && showSplit)
                return true;

            return base.IsInputKey(keyData);
        }

        protected override void OnGotFocus(EventArgs e) {
            if (!showSplit) {
                base.OnGotFocus(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Default;
            }
        }

        protected override void OnKeyDown(KeyEventArgs kevent) {
            if (showSplit) {
                if (kevent.KeyCode.Equals(Keys.Down) && !IsSplitMenuVisible) {
                    ShowContextMenuStrip();
                } else if (kevent.KeyCode.Equals(Keys.Space) && kevent.Modifiers == Keys.None) {
                    State = PushButtonState.Pressed;
                }
            }

            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent) {
            if (kevent.KeyCode.Equals(Keys.Space)) {
                if (MouseButtons == MouseButtons.None) {
                    State = PushButtonState.Normal;
                }
            } else if (kevent.KeyCode.Equals(Keys.Apps)) {
                if (MouseButtons == MouseButtons.None && !IsSplitMenuVisible) {
                    ShowContextMenuStrip();
                }
            }

            base.OnKeyUp(kevent);
        }

        protected override void OnEnabledChanged(EventArgs e) {
            State = Enabled ? PushButtonState.Normal : PushButtonState.Disabled;

            base.OnEnabledChanged(e);
        }

        protected override void OnLostFocus(EventArgs e) {
            if (!showSplit) {
                base.OnLostFocus(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Normal;
            }
        }

        bool IsMouseEntered;

        protected override void OnMouseEnter(EventArgs e) {
            if (!showSplit) {
                base.OnMouseEnter(e);
                return;
            }

            IsMouseEntered = true;

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Hot;
            }

        }

        protected override void OnMouseLeave(EventArgs e) {
            if (!showSplit) {
                base.OnMouseLeave(e);
                return;
            }

            IsMouseEntered = false;

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = Focused ? PushButtonState.Default : PushButtonState.Normal;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (!showSplit) {
                base.OnMouseDown(e);
                return;
            }

            //handle ContextMenu re-clicking the drop-down region to close the menu
            if (MSplitMenu != null && e.Button == MouseButtons.Left && !IsMouseEntered)
                SkipNextOpen = true;

            if (DropDownRectangle.Contains(e.Location) && !IsSplitMenuVisible && e.Button == MouseButtons.Left) {
                ShowContextMenuStrip();
            } else {
                State = PushButtonState.Pressed;
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            if (!showSplit) {
                base.OnMouseUp(mevent);
                return;
            }

            // if the right button was released inside the button
            if (mevent.Button == MouseButtons.Right && ClientRectangle.Contains(mevent.Location) && !IsSplitMenuVisible) {
                ShowContextMenuStrip();
            } else if (MSplitMenuStrip == null && MSplitMenu == null || !IsSplitMenuVisible) {
                SetButtonDrawState();

                if (ClientRectangle.Contains(mevent.Location) && !DropDownRectangle.Contains(mevent.Location)) {
                    OnClick(new EventArgs());
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);

            if (!showSplit)
                return;

            Graphics g = pevent.Graphics;
            Rectangle bounds = ClientRectangle;

            // draw the button background as according to the current state.
            if (State != PushButtonState.Pressed && IsDefault && !Application.RenderWithVisualStyles) {
                Rectangle backgroundBounds = bounds;
                backgroundBounds.Inflate(-1, -1);
                ButtonRenderer.DrawButton(g, backgroundBounds, State);

                // button renderer doesnt draw the black frame when themes are off
                g.DrawRectangle(SystemPens.WindowFrame, 0, 0, bounds.Width - 1, bounds.Height - 1);
            } else {
                ButtonRenderer.DrawButton(g, bounds, State);
            }

            // calculate the current dropdown rectangle.
            DropDownRectangle = new Rectangle(bounds.Right - SPLIT_SECTION_WIDTH, 0, SPLIT_SECTION_WIDTH, bounds.Height);

            int internalBorder = borderSize;
            Rectangle focusRect =
                new Rectangle(internalBorder - 1,
                              internalBorder - 1,
                              bounds.Width - DropDownRectangle.Width - internalBorder,
                              bounds.Height - (internalBorder * 2) + 2);

            bool drawSplitLine = (State == PushButtonState.Hot || State == PushButtonState.Pressed || !Application.RenderWithVisualStyles);


            if (RightToLeft == RightToLeft.Yes) {
                DropDownRectangle.X = bounds.Left + 1;
                focusRect.X = DropDownRectangle.Right;

                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Left + SPLIT_SECTION_WIDTH, borderSize, bounds.Left + SPLIT_SECTION_WIDTH, bounds.Bottom - borderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Left + SPLIT_SECTION_WIDTH + 1, borderSize, bounds.Left + SPLIT_SECTION_WIDTH + 1, bounds.Bottom - borderSize);
                }
            } else {
                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Right - SPLIT_SECTION_WIDTH, borderSize, bounds.Right - SPLIT_SECTION_WIDTH, bounds.Bottom - borderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Right - SPLIT_SECTION_WIDTH - 1, borderSize, bounds.Right - SPLIT_SECTION_WIDTH - 1, bounds.Bottom - borderSize);
                }
            }

            // Draw an arrow in the correct location
            PaintArrow(g, DropDownRectangle);

            //paint the image and text in the "button" part of the splitButton
            PaintTextandImage(g, new Rectangle(0, 0, ClientRectangle.Width - SPLIT_SECTION_WIDTH, ClientRectangle.Height));

            // draw the focus rectangle.
            if (State != PushButtonState.Pressed && Focused && ShowFocusCues) {
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }
        }

        private void PaintTextandImage(Graphics g, Rectangle bounds) {
            // Figure out where our text and image should go
            Rectangle textRectangle;
            Rectangle imageRectangle;

            CalculateButtonTextAndImageLayout(ref bounds, out textRectangle, out imageRectangle);

            //draw the image
            if (Image != null) {
                if (Enabled)
                    g.DrawImage(Image, imageRectangle.X, imageRectangle.Y, Image.Width, Image.Height);
                else
                    ControlPaint.DrawImageDisabled(g, Image, imageRectangle.X, imageRectangle.Y, BackColor);
            }

            // If we dont' use mnemonic, set formatFlag to NoPrefix as this will show ampersand.
            if (!UseMnemonic)
                TextFormatFlags = TextFormatFlags | TextFormatFlags.NoPrefix;
            else if (!ShowKeyboardCues)
                TextFormatFlags = TextFormatFlags | TextFormatFlags.HidePrefix;

            //draw the text
            if (!string.IsNullOrEmpty(Text)) {
                if (Enabled)
                    TextRenderer.DrawText(g, Text, Font, textRectangle, ForeColor, TextFormatFlags);
                else
                    ControlPaint.DrawStringDisabled(g, Text, Font, BackColor, textRectangle, TextFormatFlags);
            }
        }

        private void PaintArrow(Graphics g, Rectangle dropDownRect) {
            Point middle = new Point(Convert.ToInt32(dropDownRect.Left + dropDownRect.Width / 2), Convert.ToInt32(dropDownRect.Top + dropDownRect.Height / 2));

            //if the width is odd - favor pushing it over one pixel right.
            middle.X += (dropDownRect.Width % 2);

            Point[] arrow = new[] { new Point(middle.X - 2, middle.Y - 1), new Point(middle.X + 3, middle.Y - 1), new Point(middle.X, middle.Y + 2) };

            if (Enabled)
                g.FillPolygon(SystemBrushes.ControlText, arrow);
            else
                g.FillPolygon(SystemBrushes.ButtonShadow, arrow);
        }

        public override Size GetPreferredSize(Size proposedSize) {
            Size preferredSize = base.GetPreferredSize(proposedSize);

            //autosize correctly for splitbuttons
            if (showSplit) {
                if (AutoSize)
                    return CalculateButtonAutoSize();

                if (!string.IsNullOrEmpty(Text) && TextRenderer.MeasureText(Text, Font).Width + SPLIT_SECTION_WIDTH > preferredSize.Width)
                    return preferredSize + new Size(SPLIT_SECTION_WIDTH + borderSize * 2, 0);
            }

            return preferredSize;
        }

        private Size CalculateButtonAutoSize() {
            Size retSize = Size.Empty;
            Size textSize = TextRenderer.MeasureText(Text, Font);
            Size imageSize = Image == null ? Size.Empty : Image.Size;

            // Pad the text size
            if (Text.Length != 0) {
                textSize.Height += 4;
                textSize.Width += 4;
            }

            switch (TextImageRelation) {
                case TextImageRelation.Overlay:
                    retSize.Height = Math.Max(Text.Length == 0 ? 0 : textSize.Height, imageSize.Height);
                    retSize.Width = Math.Max(textSize.Width, imageSize.Width);
                    break;
                case TextImageRelation.ImageAboveText:
                case TextImageRelation.TextAboveImage:
                    retSize.Height = textSize.Height + imageSize.Height;
                    retSize.Width = Math.Max(textSize.Width, imageSize.Width);
                    break;
                case TextImageRelation.ImageBeforeText:
                case TextImageRelation.TextBeforeImage:
                    retSize.Height = Math.Max(textSize.Height, imageSize.Height);
                    retSize.Width = textSize.Width + imageSize.Width;
                    break;
            }

            // Pad the result
            retSize.Height += (Padding.Vertical + 6);
            retSize.Width += (Padding.Horizontal + 6);

            //pad the splitButton arrow region
            if (showSplit)
                retSize.Width += SPLIT_SECTION_WIDTH;

            return retSize;
        }

        #region Button Layout Calculations

        //The following layout functions were taken from Mono's Windows.Forms 
        //implementation, specifically "ThemeWin32Classic.cs", 
        //then modified to fit the context of this splitButton

        private void CalculateButtonTextAndImageLayout(ref Rectangle contentRect, out Rectangle textRectangle, out Rectangle imageRectangle) {
            Size textSize = TextRenderer.MeasureText(Text, Font, contentRect.Size, TextFormatFlags);
            Size imageSize = Image == null ? Size.Empty : Image.Size;

            textRectangle = Rectangle.Empty;
            imageRectangle = Rectangle.Empty;

            switch (TextImageRelation) {
                case TextImageRelation.Overlay:
                    // Overlay is easy, text always goes here
                    textRectangle = OverlayObjectRect(ref contentRect, ref textSize, TextAlign); // Rectangle.Inflate(content_rect, -4, -4);

                    //Offset on Windows 98 style when button is pressed
                    if (_state == PushButtonState.Pressed && !Application.RenderWithVisualStyles)
                        textRectangle.Offset(1, 1);

                    // Image is dependent on ImageAlign
                    if (Image != null)
                        imageRectangle = OverlayObjectRect(ref contentRect, ref imageSize, ImageAlign);

                    break;
                case TextImageRelation.ImageAboveText:
                    contentRect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(contentRect, false, textSize, imageSize, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.TextAboveImage:
                    contentRect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(contentRect, true, textSize, imageSize, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.ImageBeforeText:
                    contentRect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(contentRect, false, textSize, imageSize, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.TextBeforeImage:
                    contentRect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(contentRect, true, textSize, imageSize, out textRectangle, out imageRectangle);
                    break;
            }
        }

        private static Rectangle OverlayObjectRect(ref Rectangle container, ref Size sizeOfObject, System.Drawing.ContentAlignment alignment) {
            int x, y;

            switch (alignment) {
                case System.Drawing.ContentAlignment.TopLeft:
                    x = 4;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.TopRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                    x = 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.BottomLeft:
                    x = 4;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case System.Drawing.ContentAlignment.BottomCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                default:
                    x = 4;
                    y = 4;
                    break;
            }

            return new Rectangle(x, y, sizeOfObject.Width, sizeOfObject.Height);
        }

        private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, out Rectangle textRect, out Rectangle imageRect) {
            int elementSpacing = 0;	// Spacing between the Text and the Image
            int totalWidth = textSize.Width + elementSpacing + imageSize.Width;

            if (!textFirst)
                elementSpacing += 2;

            // If the text is too big, chop it down to the size we have available to it
            if (totalWidth > totalArea.Width) {
                textSize.Width = totalArea.Width - elementSpacing - imageSize.Width;
                totalWidth = totalArea.Width;
            }

            int excessWidth = totalArea.Width - totalWidth;
            int offset = 0;

            Rectangle finalTextRect;
            Rectangle finalImageRect;

            HorizontalAlignment hText = GetHorizontalAlignment(TextAlign);
            HorizontalAlignment hImage = GetHorizontalAlignment(ImageAlign);

            if (hImage == HorizontalAlignment.Left)
                offset = 0;
            else if (hImage == HorizontalAlignment.Right && hText == HorizontalAlignment.Right)
                offset = excessWidth;
            else if (hImage == HorizontalAlignment.Center && (hText == HorizontalAlignment.Left || hText == HorizontalAlignment.Center))
                offset += excessWidth / 3;
            else
                offset += 2 * (excessWidth / 3);

            if (textFirst) {
                finalTextRect = new Rectangle(totalArea.Left + offset, AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
                finalImageRect = new Rectangle(finalTextRect.Right + elementSpacing, AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
            } else {
                finalImageRect = new Rectangle(totalArea.Left + offset, AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
                finalTextRect = new Rectangle(finalImageRect.Right + elementSpacing, AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
            }

            textRect = finalTextRect;
            imageRect = finalImageRect;
        }

        private void LayoutTextAboveOrBelowImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, out Rectangle textRect, out Rectangle imageRect) {
            int elementSpacing = 0;	// Spacing between the Text and the Image
            int totalHeight = textSize.Height + elementSpacing + imageSize.Height;

            if (textFirst)
                elementSpacing += 2;

            if (textSize.Width > totalArea.Width)
                textSize.Width = totalArea.Width;

            // If the there isn't enough room and we're text first, cut out the image
            if (totalHeight > totalArea.Height && textFirst) {
                imageSize = Size.Empty;
                totalHeight = totalArea.Height;
            }

            int excessHeight = totalArea.Height - totalHeight;
            int offset = 0;

            Rectangle finalTextRect;
            Rectangle finalImageRect;

            VerticalAlignment vText = GetVerticalAlignment(TextAlign);
            VerticalAlignment vImage = GetVerticalAlignment(ImageAlign);

            if (vImage == VerticalAlignment.Top)
                offset = 0;
            else if (vImage == VerticalAlignment.Bottom && vText == VerticalAlignment.Bottom)
                offset = excessHeight;
            else if (vImage == VerticalAlignment.Center && (vText == VerticalAlignment.Top || vText == VerticalAlignment.Center))
                offset += excessHeight / 3;
            else
                offset += 2 * (excessHeight / 3);

            if (textFirst) {
                finalTextRect = new Rectangle(AlignInRectangle(totalArea, textSize, TextAlign).Left, totalArea.Top + offset, textSize.Width, textSize.Height);
                finalImageRect = new Rectangle(AlignInRectangle(totalArea, imageSize, ImageAlign).Left, finalTextRect.Bottom + elementSpacing, imageSize.Width, imageSize.Height);
            } else {
                finalImageRect = new Rectangle(AlignInRectangle(totalArea, imageSize, ImageAlign).Left, totalArea.Top + offset, imageSize.Width, imageSize.Height);
                finalTextRect = new Rectangle(AlignInRectangle(totalArea, textSize, TextAlign).Left, finalImageRect.Bottom + elementSpacing, textSize.Width, textSize.Height);

                if (finalTextRect.Bottom > totalArea.Bottom)
                    finalTextRect.Y = totalArea.Top;
            }

            textRect = finalTextRect;
            imageRect = finalImageRect;
        }

        private static HorizontalAlignment GetHorizontalAlignment(System.Drawing.ContentAlignment align) {
            switch (align) {
                case System.Drawing.ContentAlignment.BottomLeft:
                case System.Drawing.ContentAlignment.MiddleLeft:
                case System.Drawing.ContentAlignment.TopLeft:
                    return HorizontalAlignment.Left;
                case System.Drawing.ContentAlignment.BottomCenter:
                case System.Drawing.ContentAlignment.MiddleCenter:
                case System.Drawing.ContentAlignment.TopCenter:
                    return HorizontalAlignment.Center;
                case System.Drawing.ContentAlignment.BottomRight:
                case System.Drawing.ContentAlignment.MiddleRight:
                case System.Drawing.ContentAlignment.TopRight:
                    return HorizontalAlignment.Right;
            }

            return HorizontalAlignment.Left;
        }

        private static VerticalAlignment GetVerticalAlignment(System.Drawing.ContentAlignment align) {
            switch (align) {
                case System.Drawing.ContentAlignment.TopLeft:
                case System.Drawing.ContentAlignment.TopCenter:
                case System.Drawing.ContentAlignment.TopRight:
                    return VerticalAlignment.Top;
                case System.Drawing.ContentAlignment.MiddleLeft:
                case System.Drawing.ContentAlignment.MiddleCenter:
                case System.Drawing.ContentAlignment.MiddleRight:
                    return VerticalAlignment.Center;
                case System.Drawing.ContentAlignment.BottomLeft:
                case System.Drawing.ContentAlignment.BottomCenter:
                case System.Drawing.ContentAlignment.BottomRight:
                    return VerticalAlignment.Bottom;
            }

            return VerticalAlignment.Top;
        }

        internal static Rectangle AlignInRectangle(Rectangle outer, Size inner, System.Drawing.ContentAlignment align) {
            int x = 0;
            int y = 0;

            if (align == System.Drawing.ContentAlignment.BottomLeft || align == System.Drawing.ContentAlignment.MiddleLeft || align == System.Drawing.ContentAlignment.TopLeft)
                x = outer.X;
            else if (align == System.Drawing.ContentAlignment.BottomCenter || align == System.Drawing.ContentAlignment.MiddleCenter || align == System.Drawing.ContentAlignment.TopCenter)
                x = Math.Max(outer.X + ((outer.Width - inner.Width) / 2), outer.Left);
            else if (align == System.Drawing.ContentAlignment.BottomRight || align == System.Drawing.ContentAlignment.MiddleRight || align == System.Drawing.ContentAlignment.TopRight)
                x = outer.Right - inner.Width;
            if (align == System.Drawing.ContentAlignment.TopCenter || align == System.Drawing.ContentAlignment.TopLeft || align == System.Drawing.ContentAlignment.TopRight)
                y = outer.Y;
            else if (align == System.Drawing.ContentAlignment.MiddleCenter || align == System.Drawing.ContentAlignment.MiddleLeft || align == System.Drawing.ContentAlignment.MiddleRight)
                y = outer.Y + (outer.Height - inner.Height) / 2;
            else if (align == System.Drawing.ContentAlignment.BottomCenter || align == System.Drawing.ContentAlignment.BottomRight || align == System.Drawing.ContentAlignment.BottomLeft)
                y = outer.Bottom - inner.Height;

            return new Rectangle(x, y, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
        }

        #endregion Button Layout Calculations


        private void ShowContextMenuStrip() {
            if (SkipNextOpen) {
                // we were called because we're closing the context menu strip
                // when clicking the dropdown button.
                SkipNextOpen = false;
                return;
            }

            State = PushButtonState.Pressed;

            if (MSplitMenu != null) {
                MSplitMenu.Show(this, new Point(0, Height));
            } else if (MSplitMenuStrip != null) {
                MSplitMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
        }

        void SplitMenuStrip_Opening(object sender, CancelEventArgs e) {
            IsSplitMenuVisible = true;
        }

        void SplitMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            IsSplitMenuVisible = false;

            SetButtonDrawState();

            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked) {
                SkipNextOpen = (DropDownRectangle.Contains(PointToClient(Cursor.Position))) && MouseButtons == MouseButtons.Left;
            }
        }


        void SplitMenu_Popup(object sender, EventArgs e) {
            IsSplitMenuVisible = true;
        }

        protected override void WndProc(ref Message m) {
            //0x0212 == WM_EXITMENULOOP
            if (m.Msg == 0x0212) {
                //this message is only sent when a ContextMenu is closed (not a ContextMenuStrip)
                IsSplitMenuVisible = false;
                SetButtonDrawState();
            }

            base.WndProc(ref m);
        }

        private void SetButtonDrawState() {
            if (Bounds.Contains(Parent.PointToClient(Cursor.Position))) {
                State = PushButtonState.Hot;
            } else if (Focused) {
                State = PushButtonState.Default;
            } else if (!Enabled) {
                State = PushButtonState.Disabled;
            } else {
                State = PushButtonState.Normal;
            }
        }
    }
}