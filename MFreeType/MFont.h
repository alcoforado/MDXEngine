#pragma once

#define generic _generic
#include <ft2build.h>
#include FT_FREETYPE_H
#undef generic

public ref class MFont
{
	FT_Face _face;
public:
	MFont(FT_Face face)
	{
		_face = face;
	}

};

