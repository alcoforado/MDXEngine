#pragma once
using namespace System::Collections::Generic;
using namespace System;
//using namespace System::Linq;

#include "MFontMapEntry.h"

namespace MFreeType {
	public ref class MFontMap
	{
		Dictionary<int, MFontMapEntry^>^ _dictionary;
	public:
		MFontMap(List<MFontMapEntry^> entries);
		Bitmap^ RenderText(String^ str);

	};

}