#include "stdafx.h"
#include "MFont.h"
#include <assert.h>
using namespace System::Drawing::Imaging;
using namespace System::Drawing;

using namespace MFreeType;

Bitmap^ MFont::Rasterize(String^ text)
	{
		std::wstring str = InteropUtilities::ConvertToUnicode16(text);
		int advance = 0;
		int totalWidth = 0;
		int totalHeight = 0;
		for (unsigned i = 0; i < str.size(); i++)
		{
			this->LoadGlyph((int)str[i]);
			

			if (_face->glyph->format != FT_GLYPH_FORMAT_BITMAP)
			{
				FT_Render_Glyph(_face->glyph, FT_RENDER_MODE_NORMAL);
			}
			if (_face->glyph->bitmap.rows > totalHeight)
			{
				totalHeight = _face->glyph->bitmap.rows;
			}
			totalWidth += _face->glyph->bitmap.width;
			advance += _face->glyph->advance.x;
		}
		advance = Units::FontUnitToPixel(advance);


		int pen_x = 0;
		Bitmap^ result = gcnew Bitmap(totalWidth, totalHeight, PixelFormat::Format8bppIndexed);
		
		auto bitmapData = result->LockBits(
			Rectangle(0, 0, result->Width, result->Height),
			ImageLockMode::ReadWrite,
			PixelFormat::Format8bppIndexed);
		Byte* pdata = reinterpret_cast<Byte*>(bitmapData->Scan0.ToPointer());



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
	Bitmap^ MFont::GetBitmap(int char_code)
	{
		
		if (char_code == 32)
		{
			return nullptr;
		}
			this->LoadGlyph(char_code);


			if (_face->glyph->format != FT_GLYPH_FORMAT_BITMAP)
			{
				FT_Render_Glyph(_face->glyph, FT_RENDER_MODE_NORMAL);
			}
			
			int bitmap_height = _face->glyph->bitmap.rows;
			int bitmap_stride = _face->glyph->bitmap.pitch;
			int bitmap_width = _face->glyph->bitmap.width;

			int pen_x = 0;
			
			Bitmap^ result = gcnew Bitmap(bitmap_width,bitmap_height, PixelFormat::Format8bppIndexed);
			auto bitmapData = result->LockBits(
				Rectangle(0, 0, result->Width, result->Height),
				ImageLockMode::ReadWrite,
				PixelFormat::Format8bppIndexed);
			Byte* pdata = reinterpret_cast<Byte*>(bitmapData->Scan0.ToPointer());
			Byte* fbitmap = _face->glyph->bitmap.buffer;
			
			
			for (int y = 0; y < bitmap_height; y++)
			{
				memcpy(
				pdata,
				fbitmap,
				bitmap_width);

				//memset(pdata, 255, bitmap_width);
				pdata   += bitmapData->Stride;
				fbitmap += bitmap_stride;
			}

			
			result->UnlockBits(bitmapData);

			return result;
	}

	MFontMap^ MFont::GetFontMapForChars(String^ str)
	{
		List<MFontMapEntry^> ^entries = gcnew List<MFontMapEntry^>();
		for (int i = 0; i < str->Length; i++)
		{
			Bitmap^ bitmap = this->GetBitmap(str[i]);
			entries->Add(gcnew MFontMapEntry(
				bitmap,
				_face->glyph->advance.x,
				(int)str[i],
				i));
			
		}
		return gcnew MFontMap(entries);
	}

