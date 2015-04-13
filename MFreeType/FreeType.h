#pragma once

#include <ft2build.h>
#include FT_FREETYPE_H

class FreeType
{
	FT_Library library;

public:
	FreeType();
	
	~FreeType();
};

