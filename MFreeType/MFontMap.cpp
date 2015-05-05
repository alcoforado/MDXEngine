#include "stdafx.h"
#include "MFontMap.h"
#include "TextRenderingOptions.h"
#include "LockBitmap.h"
using namespace MFreeType;
using namespace System;
using namespace System::Drawing::Imaging;
using namespace System::Drawing;
MFontMap::MFontMap(List<MFontMapEntry^> ^entries)
{
	max_height = 0;
	max_width = 0;
	_dictionary = gcnew Dictionary<int, MFontMapEntry^>();
	for (int i = 0; i < entries->Count; i++)
	{
		auto elem = entries[i];
		if (elem->GetAdvance() > max_width)
		{
			max_width = elem->GetAdvance();
		}
		if (elem->GetCharCode != 32)//space doesn't have bitmap
		{
			if (elem->GetBitmap()->Height > max_height)
			{
				max_height = elem->GetBitmap()->Height;
			}
		}

		_dictionary->Add(elem->GetCharCode(),elem);

	}
}

Bitmap^ MFontMap::RenderLineText(String^ str,TextRenderingOptions options)
{
	int line_height = options.padding_bottom + options.padding_top + max_height;

	//Bitmap^ bitmap = gcnew Bitmap()
	//Decide the width of the bitmap
	
	int line_width = 0;
	for (int i = 0; i < str->Length; i++)
	{
		MFontMapEntry^ fontEntry;
		
		if (!_dictionary->TryGetValue((int)str[i], fontEntry))
		{
			throw gcnew Exception("Charcode is not part of this fontmap");
		}
		line_width += fontEntry->GetAdvance();
	
	}
	line_width += options.padding_left + options.padding_right;

	//Create Bitmap
	Bitmap^ bitmap=gcnew Bitmap(line_width, line_height, PixelFormat::Format8bppIndexed);
	
	LockedBitmap^ draw = gcnew LockedBitmap(bitmap);
	int Y = options.padding_top;
	int X = options.padding_left;
	for (int i = 0; i < str->Length; i++)
	{
		MFontMapEntry^ fontEntry;

		if (!_dictionary->TryGetValue((int)str[i], fontEntry))
		{
			throw gcnew Exception("Charcode is not part of this fontmap");
		}
		
		draw->Copy(X, Y, fontEntry->GetBitmap());
	}




}
