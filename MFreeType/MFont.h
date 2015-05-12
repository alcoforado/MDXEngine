#pragma once

#define generic _generic
#include <ft2build.h>
#include FT_FREETYPE_H
#undef generic

#include "Units.h"
#include "MFreeTypeException.h"
#include "InteropUtilities.h"
#include "MFontMap.h"
using namespace System::Drawing;
namespace MFreeType {
	public ref class MFont
	{
		FT_Face _face;
		int _horizontal_dpi;
		int _vertical_dpi;
	
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
		int ConvertPoints_64ToPixelHorizontal(int pts_64)
		{
			return pts_64*_horizontal_dpi / (64 * 72);
		}
		
		int ConvertFontPointsToPixelHorizontal(int pts)
		{
			return pts * _horizontal_dpi/72;
		}

		int ConvertPoints_64ToPixelVertical(int pts_64)
		{
			return pts_64*_vertical_dpi / (64 * 72);
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
		void SetSizeInPixels(int width, int height,int horizontal_dpi,int vertical_dpi)
		{
			//One point is 1/64
			//np = pts/(64*72)*dpi
			//pts = np*64*72/dpi
			_horizontal_dpi = horizontal_dpi;
			_vertical_dpi = vertical_dpi;
			int error = FT_Set_Char_Size(_face, width*64*72/horizontal_dpi, height*64*72/vertical_dpi, horizontal_dpi, vertical_dpi); 
			if (error)
			{
				throw gcnew MFreeTypeException(error);
			}
		}
		
		void SetSizeInPixels(int width, int height)
		{
			//One point is 1/64
			//np = pts/(64*72)*dpi
			//pts = np*64*72/dpi
			_horizontal_dpi = 72;
			_vertical_dpi = 72;
			int error = FT_Set_Char_Size(_face, width * 64 * 72 / _horizontal_dpi, height * 64 * 72 / _vertical_dpi, _horizontal_dpi, _vertical_dpi);
			if (error)
			{
				throw gcnew MFreeTypeException(error);
			}
		}

		Bitmap^ GetBitmap(int char_code);

		
		void SetSizeInMM(int mm, int horizontal_dpi, int vertical_dpi)
		{
			//One inch has 25.4 mm.
			//1/64 points is 1/(72*64) inches == 1/(72*64)*25.4 == 0.00551215277mm
			//1 mm has 181.417322835 (1/64)points
			_horizontal_dpi = horizontal_dpi;
			_vertical_dpi = vertical_dpi;
			int error = FT_Set_Char_Size(_face, 181.417322835 * mm, 181.417322835 * mm, horizontal_dpi, vertical_dpi);
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
			totalWidth = this->ConvertPoints_64ToPixelHorizontal(totalWidth);
			return totalWidth;
		}

		bool HasKerning()
		{
			return FT_HAS_KERNING(_face) != 0;
		}

		MFontMap^ GetFontMapForChars(String^ str);
		

		
		

	};

}