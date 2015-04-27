// MFreeType.h

#pragma once

#define generic _generic
#include <ft2build.h>
#include FT_FREETYPE_H
#undef generic

#include <map>
#include <string>
#include "MFont.h"
using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
namespace MFreeType {

	
	

	public ref class MFreeType
	{
		FT_Library library;
		Dictionary<String^, MFont^> _font_cash;
		static Dictionary<int,String^>^ _error_map=nullptr;
	private:
		String^ GetErrorMessage(int error);
	
	public:
		MFreeType();
		MFont^ GetFont(FileInfo^ fileInfo);
		void ClearCash();

	};
}
