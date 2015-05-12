#include "stdafx.h"
#include "MFontMapEntry.h"

using namespace MFreeType;

MFontMapEntry::MFontMapEntry(Bitmap^ bitmap, int advance, int char_code,int index)
{
	_bitmap = bitmap;
	_advance = advance;
	_char_code = char_code;
	_index = index;
}
