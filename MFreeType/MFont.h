#pragma once

#define generic _generic
#include <ft2build.h>
#include FT_FREETYPE_H
#undef generic

#include "Units.h"
#include "MFreeTypeException.h"
#include "InteropUtilities.h"
using namespace System::Drawing;
namespace MFreeType {
	public ref class MFont
	{
		FT_Face _face;
	
	private:
		int GetCharIndex(int charcode)
		{
			int result = FT_Get_Char_Index(_face, charcode);
			if (result == 0)
			{
				throw gcnew Exception("Char Index " + charcode + " Not Defined");
			}
			return result;
		}
	
		void LoadGlyph(int char_code)
		{
			int index = this->GetCharIndex(char_code);
			int error = FT_Load_Glyph(_face, index, FT_LOAD_DEFAULT);
			if (error)
			{
				throw gcnew MFreeTypeException(error);
			}
		}

	public:
		MFont(FT_Face face)
		{
			_face = face;
		}
		int NumFaces()
		{
			return _face->num_faces;
		}
		bool IsScalable()
		{
			return ((_face->face_flags)&FT_FACE_FLAG_SCALABLE) != 0;
		}
		void SetSizeInPixels(int width, int height)
		{
			int error = FT_Set_Pixel_Sizes(_face, width, height);
			if (error)
			{
				throw gcnew MFreeTypeException(error);

			}
		}
		
		Bitmap^ GetBitmap(int char_code);

		void SetSizeInPts(int pts, int horizontal_dpi, int vertical_dpi)
		{
			int error = FT_Set_Char_Size(_face, pts * 64, pts * 64, horizontal_dpi, vertical_dpi);
			if (error)
			{
				throw gcnew MFreeTypeException(error);
			}
		}


		int  TextSizeInPixels(String ^text)
		{
			std::wstring str = InteropUtilities::ConvertToUnicode16(text);
			int totalWidth = 0;
			for (unsigned i = 0; i < str.size(); i++)
			{
				this->LoadGlyph((int)str[i]);
				totalWidth += _face->glyph->metrics.horiAdvance;
			}
			totalWidth = Units::FontUnitToPixel(totalWidth);
			return totalWidth;
		}

		bool HasKerning()
		{
			return FT_HAS_KERNING(_face) != 0;
		}

		Bitmap^ Rasterize(String^ text);
		

	};

}