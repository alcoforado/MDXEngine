#pragma once
using namespace System::Collections::Generic;
using namespace System;
//using namespace System::Linq;

#include "MFontMapEntry.h"
#include "TextRenderingOptions.h"
namespace MFreeType {
	public ref class MFontMap
	{
		int max_height;
		int max_width;
		Dictionary<int, MFontMapEntry^>^ _dictionary;
		array<array<int>^>^ _kerning_table;
	public:
		MFontMap(List<MFontMapEntry^> ^entries, array<array<int>^>^ kerning_table);
		Bitmap^ RenderLineText(String^ str, TextRenderingOptions^ options);
		bool HasKerning() { return _kerning_table != nullptr; }
		String^ PrintKerningTable();
	};


}