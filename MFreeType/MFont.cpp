#include "stdafx.h"
#include "MFont.h"
#include <assert.h>
using namespace System::Drawing::Imaging;
using namespace System::Drawing;

using namespace MFreeType;



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
		array<array<int>^> ^kerning_table = nullptr;
		List<MFontMapEntry^> ^entries = gcnew List<MFontMapEntry^>();
		for (int i = 0; i < str->Length; i++)
		{
			Bitmap^ bitmap = this->GetBitmap(str[i]);
			entries->Add(gcnew MFontMapEntry(
				bitmap,
				this->_face->glyph->advance.x/64,
				(int)str[i],
				i));

		}
		//assembly kerning
		if (FT_HAS_KERNING(_face))
		{
			kerning_table = gcnew array<array<int>^>(entries->Count);
			for (int i = 0; i < str->Length; i++)
			{
				int glyph_i_index = FT_Get_Char_Index(_face, str[i]);
				kerning_table[i] = gcnew array<int>(entries->Count);
				for (int k = 0; k < str->Length; k++)
				{
					int glyph_k_index = FT_Get_Char_Index(_face, str[k]);
					FT_Vector delta;
					FT_Get_Kerning(_face, glyph_i_index , glyph_k_index, FT_KERNING_DEFAULT, &delta);
					kerning_table[i][k] = delta.x >> 6; // this->ConvertFontPointsToPixelHorizontal(delta.x);
				}
			}
		}
		else
		{
			kerning_table = nullptr;
		}
		return gcnew MFontMap(entries,kerning_table);
	}

