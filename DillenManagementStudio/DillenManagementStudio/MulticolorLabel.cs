using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DillenManagementStudio
{
    public class MulticolorLabel
    {
        protected float x = 0;
        protected float y = 0;
        protected string text = "";
        protected Color defaultTextColor = Color.Black;
        protected Color backgroundColor = Color.Transparent;
        protected Font defaultFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
        protected PictureBox picBx;
        

        /// CONSTRUCTORS
        //without PictureBox
        public MulticolorLabel(int width, int height, Form frm)
        {
            this.InicializeLabelWithoutPb(new Point(0,0), width, height, frm, this.defaultFont, this.defaultTextColor, Color.White);
        }
        
        public MulticolorLabel(Point location, int width, int height, Form frm)
        {
            this.InicializeLabelWithoutPb(location, width, height, frm, this.defaultFont, this.defaultTextColor, Color.White);
        }

        public MulticolorLabel(Point location, int width, int height, Form frm, Font defaultFont)
        {
            this.InicializeLabelWithoutPb(location, width, height, frm, defaultFont, this.defaultTextColor, Color.White);
        }

        public MulticolorLabel(Point location, int width, int height, Form frm,
            Font defaultFont, Color defaultTextColor)
        {
            this.InicializeLabelWithoutPb(location, width, height, frm, defaultFont, defaultTextColor, Color.White);
        }

        public MulticolorLabel(Point location, int width, int height, Form frm, Font defaultFont,
            Color defaultTextColor, Color backgroundColor)
        {
            this.InicializeLabelWithoutPb(location, width, height, frm, defaultFont, defaultTextColor, backgroundColor);
        }

        protected void InicializeLabelWithoutPb(Point location, int width, int height, Form frm, Font defaultFont,
            Color defaultTextColor, Color backgroundColor)
        {
            this.backgroundColor = backgroundColor;

            // PictureBox needs an image to draw on
            this.picBx = new PictureBox();
            this.picBx.Location = location;
            this.picBx.Width = width;
            this.picBx.Height = height;
            this.picBx.Image = new Bitmap(width, height);

            // create background all one color for drawing
            Graphics.FromImage(this.picBx.Image).FillRectangle(new SolidBrush(backgroundColor), 0, 0, 
                this.picBx.Image.Width, this.picBx.Image.Height);

            frm.Controls.Add(this.picBx);
            this.InicializeLabelFromPb(defaultFont, defaultTextColor);
        }


        //with PictureBox
        public MulticolorLabel(PictureBox pb)
        {
            this.picBx = pb;
            this.InicializeLabelFromPb(this.defaultFont, this.defaultTextColor);
        }

        public MulticolorLabel(PictureBox pb, Font defaultFont)
        {
            this.picBx = pb;
            this.InicializeLabelFromPb(defaultFont, this.defaultTextColor);
        }

        public MulticolorLabel(PictureBox pb, Font defaultFont, Color defaultTextColor)
        {
            this.picBx = pb;
            this.InicializeLabelFromPb(defaultFont, defaultTextColor);
        }

        protected void InicializeLabelFromPb(Font defaultFont, Color defaultTextColor)
        {
            this.defaultFont = defaultFont;
            this.defaultTextColor = defaultTextColor;
        }
        

        /// GETTERS AND SETTERS
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                //Clear
                this.Clear();
                
                this.Append(value);
            }

            //set: cancela tudo o que está escrito e escreve o value na defaultTextColor e defaultFont
        }

        public Color DefaultTextColor
        {
            get
            {
                return this.defaultTextColor;
            }

            set
            {
                if (value == null)
                    throw new Exception("Null color!");

                this.defaultTextColor = value;
            }
        }

        public Font DefaultFont
        {
            get
            {
                return (Font)this.defaultFont.Clone();
            }

            set
            {
                if (value == null)
                    throw new Exception("Null color!");

                this.defaultFont = value;
            }
        }

        public int X
        {
            get
            {
                return this.picBx.Location.X;
            }

            set
            {
                try
                {
                    this.picBx.Location = new Point(value, this.picBx.Location.Y);
                }
                catch (Exception e)
                {
                    throw new Exception("Invalid number for MulticolorLabel.X");
                }
            }
        }

        public int Y
        {
            get
            {
                return this.picBx.Location.Y;
            }

            set
            {
                try
                {
                    this.picBx.Location = new Point(this.picBx.Location.X, value);
                }catch(Exception e)
                {
                    throw new Exception("Invalid number for MulticolorLabel.Y");
                }
            }
        }
        
        
        ///ADD TEXT PROCEDURES 
        public void Append(string text)
        {
            this.Append(text, this.defaultTextColor);
        }

        public void Append(string text, Color color)
        {
            this.Append(text, color, this.defaultFont);
        }

        public void Append(string text, Color color, Font font)
        {
            List<string> newLineStrs = new List<string>();
            newLineStrs.Add("\\r\\n");
            newLineStrs.Add("\\n");

            int startIndex = 0;
            while(startIndex < text.Length)
            {
                int iArrayNumber = -1;
                int newIndex = text.IndexOf(newLineStrs, startIndex, ref iArrayNumber);

                if(newIndex < 0)
                {
                    this.AppendEachLine(text.Substring(startIndex), color, font);
                    break;
                }else
                {
                    this.AppendEachLine(text.Substring(startIndex, newIndex - startIndex), color, font);
                    this.NewLine(font);
                    if(iArrayNumber == 0)
                        startIndex = newIndex + 4;
                    else
                        startIndex = newIndex + 2;
                }
            }
        }

        /*public void Append(string text, Color color, Font font)
        {
            Graphics g = Graphics.FromImage(this.picBx.Image);

            float strWidth = (g.MeasureString(text, font)).Width;
            
            if (this.x + strWidth >= this.picBx.Width)
                this.NewLine(font);

            // draw text in whatever color
            g.DrawString(text, font, new SolidBrush(color), this.x, this.y);
            
            // measure text and advance x
            this.x += strWidth;

            this.text += text;
        }*/
        protected void AppendEachLine(string text, Color color, Font font)
        {
            Graphics g = Graphics.FromImage(this.picBx.Image);

            string[] textWords = text.Split();
            for (int i = 0; i < textWords.Length; i++)
            {
                string currWord = textWords[i] + (i== textWords.Length-1?"":" ");

                float strWidth = (g.MeasureString(currWord, font)).Width;

                // Word-Break
                if (this.x + strWidth >= this.picBx.Width)
                    this.NewLine(font);

                // draw text in whatever color
                g.DrawString(currWord, font, new SolidBrush(color), this.x, this.y);

                // measure text and advance x
                this.x += strWidth;
            }           

            this.text += text;
        }


        /// NEW LINE
        public void NewLine()
        {
            this.NewLine(this.defaultFont);
        }

        public void NewLine(Font font)
        {
            this.x = 0;
            this.y += font.Height;
        }


        /// CLEAR
        public void Clear()
        {
            this.x = 0;
            this.y = 0;

            this.picBx.Invalidate();
            Graphics.FromImage(this.picBx.Image).Clear(this.backgroundColor);
        }

    }
}