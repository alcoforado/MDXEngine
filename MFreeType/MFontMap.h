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
	public:
		MFontMap(List<MFontMapEntry^> ^entries);
		Bitmap^ RenderLineText(String^ str, TextRenderingOptions^ options);

	};


}