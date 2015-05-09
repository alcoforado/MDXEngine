#pragma once
using namespace System::Drawing;


namespace MFreeType {
	public ref class MFontMapEntry
	{
		Bitmap^ _bitmap;
		int _advance;
		int _char_code;
		int _index;
	public:
		MFontMapEntry(Bitmap^ _bitmap, int advance,int char_code,int index);
		
		int GetCharCode(){ return _char_code; }
		int GetAdvance(){ return _advance; }
		Bitmap^ GetBitmap() { return _bitmap; }
		int GetIndex() { return _index; }
		bool HasBitmap() { return _bitmap != nullptr; }
	};

}