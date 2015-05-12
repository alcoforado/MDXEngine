#include "stdafx.h"
#include "MFontMap.h"
#include "TextRenderingOptions.h"
#include "LockBitmap.h"
#include <vector>
using namespace MFreeType;
using namespace System;
using namespace System::Drawing::Imaging;
using namespace System::Drawing;
using namespace System::Diagnostics;
using namespace System::Text;

String^ MFontMap::PrintKerningTable()
{
	if (!this->HasKerning())
		return gcnew String("");
	StringBuilder^  result = gcnew StringBuilder();
	for each (auto elem1 in _dictionary)
	{
		MFontMapEntry^ entry1 = elem1.Value;
		for each (auto elem2 in _dictionary)
		{
			MFontMapEntry^ entry2 = elem2.Value;
			result->Append(String::Format("{0}{1}:{2}  ",
				entry1->GetChar(),
				entry2->GetChar(),
				_kerning_table[entry1->GetIndex()][entry2->GetIndex()]
				));
		}
		result->AppendLine();
	}
	return result->ToString();

}


MFontMap::MFontMap(List<MFontMapEntry^> ^entries,array<array<int>^>^ kerning_table)
{
	max_height = 0;
	max_width = 0;
	_kerning_table = kerning_table;
	_dictionary = gcnew Dictionary<int, MFontMapEntry^>();
	for (int i = 0; i < entries->Count; i++)
	{
		auto elem = entries[i];
		if (_dictionary->ContainsKey(elem->GetCharCode()))
		{
			continue;
		}
		
		
		if (elem->GetAdvance() > max_width)
		{
			max_width = elem->GetAdvance();
		}
		if (elem->GetCharCode() != 32)//space doesn't have bitmap
		{
			if (elem->GetBitmap()->Height > max_height)
			{
				max_height = elem->GetBitmap()->Height;
			}
		}

		_dictionary->Add(elem->GetCharCode(),elem);

	}
}

struct CharPos{
	int X;
	int Y;
	int DX;

};

Bitmap^ MFontMap::RenderLineText(String^ str,TextRenderingOptions^ options)
{
	
	
	int line_height = options->padding_bottom + options->padding_top + max_height;
	std::vector<CharPos> v(str->Length);
	//Bitmap^ bitmap = gcnew Bitmap()
	//Decide the width of the bitmap
	
	int X  = options->padding_left; 
	int Y = options->padding_top;
	if (str->Length == 0)
		return nullptr;
	int last_char = str->Length - 1;
	
	MFontMapEntry ^nextEntry = nullptr;
	MFontMapEntry ^fontEntry = _dictionary[str[0]];
	for (int i = 0; i <= last_char; i++)
	{
		struct CharPos pos;
		pos.X = X;
		pos.Y = Y;

		if (!_dictionary->TryGetValue((int)str[i], fontEntry))
		{
			throw gcnew Exception("Charcode is not part of this fontmap");
		}
		pos.DX = fontEntry->GetAdvance();
		
		if (i != last_char && HasKerning())
		{
			nextEntry = _dictionary[str[i+1]];
			pos.DX += _kerning_table[fontEntry->GetIndex()][nextEntry->GetIndex()];
		}
		X += pos.DX;
		fontEntry=nextEntry;
		v[i]=pos;
	}
	X += options->padding_right;

	//Create Bitmap
	Bitmap^ bitmap=gcnew Bitmap(X, line_height, PixelFormat::Format8bppIndexed);
	
	LockedBitmap^ draw = gcnew LockedBitmap(bitmap);
	
	for (int i = 0; i < str->Length; i++)
	{
		MFontMapEntry^ fontEntry;

		if (!_dictionary->TryGetValue((int)str[i], fontEntry))
		{
			throw gcnew Exception("Charcode is not part of this fontmap");
		}
		if (fontEntry->HasBitmap())
			draw->Copy(v[i].X, v[i].Y, fontEntry->GetBitmap());
	}
	return bitmap;
}
