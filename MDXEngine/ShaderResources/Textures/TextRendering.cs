﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Interfaces;
using MDXEngine.ShaderResources.Textures.BinPack;

namespace MDXEngine.Textures
{
    
  
    
    public class TextRendering : IDisposable

    {
        Bitmap _bitmap;
        Graphics _graphics;

        public TextRendering(int initial_width=600,int initial_height=200)
        {
            this.ResizeBuffer(initial_width, initial_height);

        }
        void ResizeBuffer(int width, int height)
        {
            if (_graphics != null)
                _graphics.Dispose();
            if (_bitmap != null)
                _bitmap.Dispose();
            _bitmap   = new Bitmap(width,height, PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.PageUnit = GraphicsUnit.Pixel;
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;    
        }


        public IBitmap RenderText(String text, TextWriteOptions options = null)
        {
            var font = new Font(options.font_type,options.font_size);
            var sizef = _graphics.MeasureString(text, font, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);

             //Calculate the width height of the bitmap with the padding_bottom
            var  width = (int) sizef.Width + options.padding_left + options.padding_right;
            var  height = (int)sizef.Height + options.padding_top + options.padding_bottom;
                    
                 
            if (height  > _bitmap.Height || width  > _bitmap.Width)
            {
                this.ResizeBuffer(
                    Math.Max((int)height, _bitmap.Width),
                    Math.Max((int)width,  _bitmap.Height));
            }
            var rect = new RectangleF(
                       (float)options.padding_left,
                       (float)options.padding_top,
                       sizef.Width,
                       sizef.Height);
            var brush = new SolidBrush(Color.Red);
            _graphics.DrawString(text, font, brush, rect);

            var result = _bitmap.Clone(new Rectangle(new Point(0, 0), new Size((int) width, (int) height)),_bitmap.PixelFormat);
            return new GDIBitmap(result);
        }


        public void Dispose()
        {
            _graphics.Dispose();
            _bitmap.Dispose();
            _graphics = null;
            _bitmap = null;
        }
    }
}