using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace MapleCLB.Forms {
    public class SplitButton : Button {
        private const int SPLIT_SECTION_WIDTH = 18;
        private static readonly int borderSize = SystemInformation.Border3DSize.Width * 2;

        private Rectangle dropDownRectangle;

        private bool isMouseEntered;
        private bool isSplitMenuVisible;
        private ContextMenu mSplitMenu;
        private ContextMenuStrip mSplitMenuStrip;
        private bool showSplit;
        private bool skipNextOpen;

        private PushButtonState state;
        private TextFormatFlags textFormatFlags = TextFormatFlags.Default;

        public SplitButton() {
            AutoSize = true;
        }

        public sealed override bool AutoSize {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        protected override bool IsInputKey(Keys keyData) {
            if (keyData.Equals(Keys.Down) && showSplit) {
                return true;
            }

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
                if (kevent.KeyCode.Equals(Keys.Down) && !isSplitMenuVisible) {
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
                if (MouseButtons == MouseButtons.None && !isSplitMenuVisible) {
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

        protected override void OnMouseEnter(EventArgs e) {
            if (!showSplit) {
                base.OnMouseEnter(e);
                return;
            }

            isMouseEntered = true;

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Hot;
            }
        }

        protected override void OnMouseLeave(EventArgs e) {
            if (!showSplit) {
                base.OnMouseLeave(e);
                return;
            }

            isMouseEntered = false;

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
            if (mSplitMenu != null && e.Button == MouseButtons.Left && !isMouseEntered) {
                skipNextOpen = true;
            }

            if (dropDownRectangle.Contains(e.Location) && !isSplitMenuVisible && e.Button == MouseButtons.Left) {
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
            if (mevent.Button == MouseButtons.Right && ClientRectangle.Contains(mevent.Location) && !isSplitMenuVisible) {
                ShowContextMenuStrip();
            } else if (mSplitMenuStrip == null && mSplitMenu == null || !isSplitMenuVisible) {
                SetButtonDrawState();

                if (ClientRectangle.Contains(mevent.Location) && !dropDownRectangle.Contains(mevent.Location)) {
                    OnClick(new EventArgs());
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);

            if (!showSplit) {
                return;
            }

            var g = pevent.Graphics;
            var bounds = ClientRectangle;

            // draw the button background as according to the current state.
            if (State != PushButtonState.Pressed && IsDefault && !Application.RenderWithVisualStyles) {
                var backgroundBounds = bounds;
                backgroundBounds.Inflate(-1, -1);
                ButtonRenderer.DrawButton(g, backgroundBounds, State);

                // button renderer doesnt draw the black frame when themes are off
                g.DrawRectangle(SystemPens.WindowFrame, 0, 0, bounds.Width - 1, bounds.Height - 1);
            } else {
                ButtonRenderer.DrawButton(g, bounds, State);
            }

            // calculate the current dropdown rectangle.
            dropDownRectangle = new Rectangle(bounds.Right - SPLIT_SECTION_WIDTH, 0, SPLIT_SECTION_WIDTH, bounds.Height);

            int internalBorder = borderSize;
            var focusRect =
                new Rectangle(internalBorder - 1,
                    internalBorder - 1,
                    bounds.Width - dropDownRectangle.Width - internalBorder,
                    bounds.Height - internalBorder * 2 + 2);

            bool drawSplitLine = State == PushButtonState.Hot || State == PushButtonState.Pressed ||
                                 !Application.RenderWithVisualStyles;

            if (RightToLeft == RightToLeft.Yes) {
                dropDownRectangle.X = bounds.Left + 1;
                focusRect.X = dropDownRectangle.Right;

                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Left + SPLIT_SECTION_WIDTH, borderSize,
                        bounds.Left + SPLIT_SECTION_WIDTH, bounds.Bottom - borderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Left + SPLIT_SECTION_WIDTH + 1, borderSize,
                        bounds.Left + SPLIT_SECTION_WIDTH + 1, bounds.Bottom - borderSize);
                }
            } else {
                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Right - SPLIT_SECTION_WIDTH, borderSize,
                        bounds.Right - SPLIT_SECTION_WIDTH, bounds.Bottom - borderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Right - SPLIT_SECTION_WIDTH - 1, borderSize,
                        bounds.Right - SPLIT_SECTION_WIDTH - 1, bounds.Bottom - borderSize);
                }
            }

            // Draw an arrow in the correct location
            PaintArrow(g, dropDownRectangle);

            //paint the image and text in the "button" part of the splitButton
            PaintTextandImage(g,
                new Rectangle(0, 0, ClientRectangle.Width - SPLIT_SECTION_WIDTH, ClientRectangle.Height));

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
                if (Enabled) {
                    g.DrawImage(Image, imageRectangle.X, imageRectangle.Y, Image.Width, Image.Height);
                } else {
                    ControlPaint.DrawImageDisabled(g, Image, imageRectangle.X, imageRectangle.Y, BackColor);
                }
            }

            // If we dont' use mnemonic, set formatFlag to NoPrefix as this will show ampersand.
            if (!UseMnemonic) {
                textFormatFlags = textFormatFlags | TextFormatFlags.NoPrefix;
            } else if (!ShowKeyboardCues) {
                textFormatFlags = textFormatFlags | TextFormatFlags.HidePrefix;
            }

            //draw the text
            if (!string.IsNullOrEmpty(Text)) {
                if (Enabled) {
                    TextRenderer.DrawText(g, Text, Font, textRectangle, ForeColor, textFormatFlags);
                } else {
                    ControlPaint.DrawStringDisabled(g, Text, Font, BackColor, textRectangle, textFormatFlags);
                }
            }
        }

        private void PaintArrow(Graphics g, Rectangle dropDownRect) {
            var middle = new Point(Convert.ToInt32(dropDownRect.Left + dropDownRect.Width / 2),
                Convert.ToInt32(dropDownRect.Top + dropDownRect.Height / 2));

            //if the width is odd - favor pushing it over one pixel right.
            middle.X += dropDownRect.Width % 2;

            Point[] arrow = {
                new Point(middle.X - 2, middle.Y - 1), new Point(middle.X + 3, middle.Y - 1),
                new Point(middle.X, middle.Y + 2)
            };

            g.FillPolygon(Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow, arrow);
        }

        public override Size GetPreferredSize(Size proposedSize) {
            var preferredSize = base.GetPreferredSize(proposedSize);

            //autosize correctly for splitbuttons
            if (!showSplit) {
                return preferredSize;
            }

            if (AutoSize) {
                return CalculateButtonAutoSize();
            }

            if (!string.IsNullOrEmpty(Text) &&
                TextRenderer.MeasureText(Text, Font).Width + SPLIT_SECTION_WIDTH > preferredSize.Width) {
                return preferredSize + new Size(SPLIT_SECTION_WIDTH + borderSize * 2, 0);
            }

            return preferredSize;
        }

        private Size CalculateButtonAutoSize() {
            var retSize = Size.Empty;
            var textSize = TextRenderer.MeasureText(Text, Font);
            var imageSize = Image == null ? Size.Empty : Image.Size;

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
            retSize.Height += Padding.Vertical + 6;
            retSize.Width += Padding.Horizontal + 6;

            //pad the splitButton arrow region
            if (showSplit) {
                retSize.Width += SPLIT_SECTION_WIDTH;
            }

            return retSize;
        }

        private void ShowContextMenuStrip() {
            if (skipNextOpen) {
                // we were called because we're closing the context menu strip
                // when clicking the dropdown button.
                skipNextOpen = false;
                return;
            }

            State = PushButtonState.Pressed;

            if (mSplitMenu != null) {
                mSplitMenu.Show(this, new Point(0, Height));
            } else {
                mSplitMenuStrip?.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
        }

        private void SplitMenuStrip_Opening(object sender, CancelEventArgs e) {
            isSplitMenuVisible = true;
        }

        private void SplitMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            isSplitMenuVisible = false;

            SetButtonDrawState();

            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked) {
                skipNextOpen = dropDownRectangle.Contains(PointToClient(Cursor.Position)) &&
                               MouseButtons == MouseButtons.Left;
            }
        }

        private void SplitMenu_Popup(object sender, EventArgs e) {
            isSplitMenuVisible = true;
        }

        protected override void WndProc(ref Message m) {
            //0x0212 == WM_EXITMENULOOP
            if (m.Msg == 0x0212) {
                //this message is only sent when a ContextMenu is closed (not a ContextMenuStrip)
                isSplitMenuVisible = false;
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

        #region Properties
        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip {
            get { return SplitMenuStrip; }
            set { SplitMenuStrip = value; }
        }

        [DefaultValue(null)]
        public ContextMenu SplitMenu {
            get { return mSplitMenu; }
            set {
                //remove the event handlers for the old SplitMenu
                if (mSplitMenu != null) {
                    mSplitMenu.Popup -= SplitMenu_Popup;
                }

                //add the event handlers for the new SplitMenu
                if (value != null) {
                    ShowSplit = true;
                    value.Popup += SplitMenu_Popup;
                } else {
                    ShowSplit = false;
                }

                mSplitMenu = value;
            }
        }

        [DefaultValue(null)]
        public ContextMenuStrip SplitMenuStrip {
            get { return mSplitMenuStrip; }
            set {
                //remove the event handlers for the old SplitMenuStrip
                if (mSplitMenuStrip != null) {
                    mSplitMenuStrip.Closing -= SplitMenuStrip_Closing;
                    mSplitMenuStrip.Opening -= SplitMenuStrip_Opening;
                }

                //add the event handlers for the new SplitMenuStrip
                if (value != null) {
                    ShowSplit = true;
                    value.Closing += SplitMenuStrip_Closing;
                    value.Opening += SplitMenuStrip_Opening;
                } else {
                    ShowSplit = false;
                }

                mSplitMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowSplit {
            set {
                if (value == showSplit) {
                    return;
                }

                showSplit = value;
                Invalidate();

                Parent?.PerformLayout();
            }
        }

        private PushButtonState State {
            get { return state; }
            set {
                if (!state.Equals(value)) {
                    state = value;
                    Invalidate();
                }
            }
        }
        #endregion Properties

        #region Button Layout Calculations

        //The following layout functions were taken from Mono's Windows.Forms 
        //implementation, specifically "ThemeWin32Classic.cs", 
        //then modified to fit the context of this splitButton

        private void CalculateButtonTextAndImageLayout(ref Rectangle contentRect, out Rectangle textRectangle,
                                                       out Rectangle imageRectangle) {
            var textSize = TextRenderer.MeasureText(Text, Font, contentRect.Size, textFormatFlags);
            var imageSize = Image == null ? Size.Empty : Image.Size;

            textRectangle = Rectangle.Empty;
            imageRectangle = Rectangle.Empty;

            switch (TextImageRelation) {
                case TextImageRelation.Overlay:
                    // Overlay is easy, text always goes here
                    textRectangle = OverlayObjectRect(ref contentRect, ref textSize, TextAlign);
                    // Rectangle.Inflate(content_rect, -4, -4);

                    //Offset on Windows 98 style when button is pressed
                    if (state == PushButtonState.Pressed && !Application.RenderWithVisualStyles) {
                        textRectangle.Offset(1, 1);
                    }

                    // Image is dependent on ImageAlign
                    if (Image != null) {
                        imageRectangle = OverlayObjectRect(ref contentRect, ref imageSize, ImageAlign);
                    }

                    break;
                case TextImageRelation.ImageAboveText:
                    contentRect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(contentRect, false, textSize, imageSize, out textRectangle,
                        out imageRectangle);
                    break;
                case TextImageRelation.TextAboveImage:
                    contentRect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(contentRect, true, textSize, imageSize, out textRectangle,
                        out imageRectangle);
                    break;
                case TextImageRelation.ImageBeforeText:
                    contentRect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(contentRect, false, textSize, imageSize, out textRectangle,
                        out imageRectangle);
                    break;
                case TextImageRelation.TextBeforeImage:
                    contentRect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(contentRect, true, textSize, imageSize, out textRectangle,
                        out imageRectangle);
                    break;
            }
        }

        private static Rectangle OverlayObjectRect(ref Rectangle container, ref Size sizeOfObject,
                                                   ContentAlignment alignment) {
            int x, y;

            switch (alignment) {
                case ContentAlignment.TopLeft:
                    x = 4;
                    y = 4;
                    break;
                case ContentAlignment.TopCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = 4;
                    break;
                case ContentAlignment.TopRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = 4;
                    break;
                case ContentAlignment.MiddleLeft:
                    x = 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    x = 4;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case ContentAlignment.BottomCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case ContentAlignment.BottomRight:
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

        private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize,
                                                  out Rectangle textRect, out Rectangle imageRect) {
            int elementSpacing = 0; // Spacing between the Text and the Image
            int totalWidth = textSize.Width + elementSpacing + imageSize.Width;

            if (!textFirst) {
                elementSpacing += 2;
            }

            // If the text is too big, chop it down to the size we have available to it
            if (totalWidth > totalArea.Width) {
                textSize.Width = totalArea.Width - elementSpacing - imageSize.Width;
                totalWidth = totalArea.Width;
            }

            int excessWidth = totalArea.Width - totalWidth;
            int offset = 0;

            Rectangle finalTextRect;
            Rectangle finalImageRect;

            var hText = GetHorizontalAlignment(TextAlign);
            var hImage = GetHorizontalAlignment(ImageAlign);

            if (hImage == HorizontalAlignment.Left) {
                offset = 0;
            } else if (hImage == HorizontalAlignment.Right && hText == HorizontalAlignment.Right) {
                offset = excessWidth;
            } else if (hImage == HorizontalAlignment.Center &&
                       (hText == HorizontalAlignment.Left || hText == HorizontalAlignment.Center)) {
                offset += excessWidth / 3;
            } else {
                offset += 2 * (excessWidth / 3);
            }

            if (textFirst) {
                finalTextRect = new Rectangle(totalArea.Left + offset,
                    AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
                finalImageRect = new Rectangle(finalTextRect.Right + elementSpacing,
                    AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
            } else {
                finalImageRect = new Rectangle(totalArea.Left + offset,
                    AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
                finalTextRect = new Rectangle(finalImageRect.Right + elementSpacing,
                    AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
            }

            textRect = finalTextRect;
            imageRect = finalImageRect;
        }

        private void LayoutTextAboveOrBelowImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize,
                                                 out Rectangle textRect, out Rectangle imageRect) {
            int elementSpacing = 0; // Spacing between the Text and the Image
            int totalHeight = textSize.Height + elementSpacing + imageSize.Height;

            if (textFirst) {
                elementSpacing += 2;
            }

            if (textSize.Width > totalArea.Width) {
                textSize.Width = totalArea.Width;
            }

            // If the there isn't enough room and we're text first, cut out the image
            if (totalHeight > totalArea.Height && textFirst) {
                imageSize = Size.Empty;
                totalHeight = totalArea.Height;
            }

            int excessHeight = totalArea.Height - totalHeight;
            int offset = 0;

            Rectangle finalTextRect;
            Rectangle finalImageRect;

            var vText = GetVerticalAlignment(TextAlign);
            var vImage = GetVerticalAlignment(ImageAlign);

            if (vImage == VerticalAlignment.Top) {
                offset = 0;
            } else if (vImage == VerticalAlignment.Bottom && vText == VerticalAlignment.Bottom) {
                offset = excessHeight;
            } else if (vImage == VerticalAlignment.Center &&
                       (vText == VerticalAlignment.Top || vText == VerticalAlignment.Center)) {
                offset += excessHeight / 3;
            } else {
                offset += 2 * (excessHeight / 3);
            }

            if (textFirst) {
                finalTextRect = new Rectangle(AlignInRectangle(totalArea, textSize, TextAlign).Left,
                    totalArea.Top + offset, textSize.Width, textSize.Height);
                finalImageRect = new Rectangle(AlignInRectangle(totalArea, imageSize, ImageAlign).Left,
                    finalTextRect.Bottom + elementSpacing, imageSize.Width, imageSize.Height);
            } else {
                finalImageRect = new Rectangle(AlignInRectangle(totalArea, imageSize, ImageAlign).Left,
                    totalArea.Top + offset, imageSize.Width, imageSize.Height);
                finalTextRect = new Rectangle(AlignInRectangle(totalArea, textSize, TextAlign).Left,
                    finalImageRect.Bottom + elementSpacing, textSize.Width, textSize.Height);

                if (finalTextRect.Bottom > totalArea.Bottom) {
                    finalTextRect.Y = totalArea.Top;
                }
            }

            textRect = finalTextRect;
            imageRect = finalImageRect;
        }

        private static HorizontalAlignment GetHorizontalAlignment(ContentAlignment align) {
            switch (align) {
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    return HorizontalAlignment.Left;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    return HorizontalAlignment.Center;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    return HorizontalAlignment.Right;
            }

            return HorizontalAlignment.Left;
        }

        private static VerticalAlignment GetVerticalAlignment(ContentAlignment align) {
            switch (align) {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    return VerticalAlignment.Top;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    return VerticalAlignment.Center;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return VerticalAlignment.Bottom;
            }

            return VerticalAlignment.Top;
        }

        internal static Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align) {
            int x = 0;
            int y = 0;

            switch (align) {
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    x = outer.X;
                    break;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    x = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    x = outer.Right - inner.Width;
                    break;
            }
            switch (align) {
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    y = outer.Y;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    y = outer.Y + (outer.Height - inner.Height) / 2;
                    break;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                case ContentAlignment.BottomLeft:
                    y = outer.Bottom - inner.Height;
                    break;
            }

            return new Rectangle(x, y, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
        }
        #endregion Button Layout Calculations
    }
}
