using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using MDXEngine.SharpDXExtensions;
namespace MDXEngine.Textures
{
    public class GDITexture : Texture
    {

        public GDITexture(IDxContext dx, String text, TextWriteOptions options)
        {
            int width = 2;
            int height = 2;
            var brush = new SolidBrush(options.color.ToSystemColor());
         /*   StringAlignment align;
            switch (options.text_align)
            {
                case TextAlignment.Center:
                    align = StringAlignment.Center;
                    break;
                case TextAlignment.Left:
                    align = StringAlignment.Far;
                    break;
                case TextAlignment.Right:
                    align = StringAlignment.Near;
                    break;
            }*/
            var font = new Font(options.font_type, options.font_size);
            SizeF sizef;
            using (var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    sizef = graphics.MeasureString(text, font, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);
                }
            }

            //Calculate the width height of the bitmap with the padding_bottom
             width = (int) sizef.Width + options.padding_left + options.padding_right;
             height = (int)sizef.Height + options.padding_top + options.padding_bottom;
            using (var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    
                    var rect = new RectangleF(
                        (float)options.padding_left,
                        (float)options.padding_top,
                        sizef.Width,
                        sizef.Height);

                    graphics.DrawString(text, font, brush, rect);
                    graphics.Flush();

                    this.InitTexture(dx,new Texture2DDescription()
                        {
                            BindFlags = BindFlags.ShaderResource,
                            Width = width,
                            Height = height,
                            Usage = ResourceUsage.Default,
                            Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb,
                            ArraySize = 1,
                            OptionFlags = ResourceOptionFlags.None,
                            MipLevels = 1,
                            CpuAccessFlags = CpuAccessFlags.None,
                            SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0)
                        });

                    this.CopyFromBitmap(bitmap);
                }
            }
                 

        }

        public GDITexture(IDxContext dx, int width, int height)
        {
            this.InitTexture(dx, new Texture2DDescription()
            {
                BindFlags = BindFlags.ShaderResource,
                Width = width,
                Height = height,
                Usage = ResourceUsage.Default,
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb,
                ArraySize = 1,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                CpuAccessFlags = CpuAccessFlags.None,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0)
            });
        }

        public GDITexture(IDxContext dx, Bitmap bitmap)
        {
            this.InitTexture(dx, new Texture2DDescription()
            {
                BindFlags = BindFlags.ShaderResource,
                Width = bitmap.Width,
                Height = bitmap.Height,
                Usage = ResourceUsage.Default,
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb,
                ArraySize = 1,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                CpuAccessFlags = CpuAccessFlags.None,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0)
            });
            CopyFromBitmap(bitmap);

        }


        public void CopyFromBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
            {

                var height = Math.Min(_resource.Description.Height, bitmap.Height);
                var width = Math.Min(_resource.Description.Width, bitmap.Width);
                int pixelSize = 4;
                var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

                Nullable<ResourceRegion> region = new ResourceRegion
                {
                    Left = 0,
                    Top = 0,
                    Right = width,
                    Bottom = height,
                    Front = 0,
                    Back = 1
                };


                _dx.DeviceContext.UpdateSubresource(
                    (Resource)_resource,
                    0,
                    region,
                    bitmapData.Scan0,
                    bitmapData.Stride,
                    height * width * pixelSize);

                bitmap.UnlockBits(bitmapData);
            }
            else
                throw new Exception("Only Bitmap with PixelFormat Format32bppArgb is compatible for now");

        }


        public void RenderText(String text, TextWriteOptions options)
        {
            using (var bitmap = new Bitmap(_resource.Description.Width, _resource.Description.Height, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var brush = new SolidBrush(options.color.ToSystemColor());
                  /*  StringAlignment align;
                    switch (options.text_align)
                    {
                        case TextAlignment.Center:
                            align = StringAlignment.Center;
                            break;
                        case TextAlignment.Left:
                            align = StringAlignment.Far;
                            break;
                        case TextAlignment.Right:
                            align = StringAlignment.Near;
                            break;
                    }*/
                    var font = new Font(options.font_type, options.font_size);
                    var size = graphics.MeasureString(text, font, new PointF(0.0f, 0.0f), StringFormat.GenericDefault);
                    var rect = new RectangleF(
                        (float)options.padding_left,
                        (float)options.padding_top,
                        (float)(_resource.Description.Width - options.padding_left - options.padding_right),
                        (float)(_resource.Description.Height - options.padding_top - options.padding_bottom)
                        );
                    graphics.DrawString(text, font, brush, rect);
                    graphics.Flush();

                    this.CopyFromBitmap(bitmap);
                }
            }
        }
    }
}
