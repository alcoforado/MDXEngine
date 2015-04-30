#pragma once
using namespace System::Drawing;


namespace MFreeType {
	ref class MFontMapEntry
	{
		Bitmap^ _bitmap;
		int _advance;
		int _char_code;
	public:
		MFontMapEntry(Bitmap^ _bitmap, int advance,int char_code);
		
		int GetCharCode(){ return _char_code; }
	};

}