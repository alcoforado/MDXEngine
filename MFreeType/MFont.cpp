#include "stdafx.h"
#include "MFont.h"
#include <assert.h>
using namespace System::Drawing::Imaging;
using namespace System::Drawing;

using namespace MFreeType;

Bitmap^ MFont::Rasterize(String^ text)
{
	{
		std::wstring str = InteropUtilities::ConvertToUnicode16(text);
		int totalWidth = 0;
		int totalHeight = 0;
		for (unsigned i = 0; i < str.size(); i++)
		{
			this->LoadGlyph((int)str[i]);
			totalWidth += _face->glyph->metrics.horiAdvance;

			if (_face->glyph->format != FT_GLYPH_FORMAT_BITMAP)
			{
				FT_Render_Glyph(_face->glyph, FT_RENDER_MODE_NORMAL);
			}
			if (_face->glyph->bitmap_top > totalHeight)
			{
				totalHeight = _face->glyph->bitmap_top;
			}

		}
		totalWidth = Units::FontUnitToPixel(totalWidth);


		int pen_x = 0;
		Bitmap^ result = gcnew Bitmap(totalWidth, totalHeight, PixelFormat::Format8bppIndexed);
		Byte* pdata = reinterpret_cast<Byte*>(
			result->LockBits(
			Rectangle(0, 0, result->Width, result->Height),
			ImageLockMode::ReadWrite,
			PixelFormat::Format8bppIndexed)->Scan0.ToPointer());



		for (unsigned i = 0; i < str.size(); i++)
		{
			this->LoadGlyph((int)str[i]);
			//if it is not bitmap, create one
			if (_face->glyph->format != FT_GLYPH_FORMAT_BITMAP)
			{
				FT_Render_Glyph(_face->glyph, FT_RENDER_MODE_NORMAL);
			}
			int bitmap_rows = _face->glyph->bitmap.rows;
			int bitmap_stride = _face->glyph->bitmap.pitch;
			int bitmap_width = _face->glyph->bitmap.width;
			for (int y = 0; y < bitmap_rows; y++)
			{
				assert(totalWidth*y + pen_x < totalWidth*totalHeight);
				memcpy(
					pdata + totalWidth*y + pen_x,
					_face->glyph->bitmap.buffer + bitmap_stride*y,
					bitmap_width);
			}
		}
		return result;


	}
}

