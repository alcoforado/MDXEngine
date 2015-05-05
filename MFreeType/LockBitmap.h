#pragma once
using namespace System::Drawing;
using namespace System::Drawing::Imaging;
using namespace System;
namespace MFreeType {
	ref struct LockedBitmap
	{
		Bitmap^ _bitmap;
		BitmapData^ _bitmapData;
		Byte* _pData;
	public:
		LockedBitmap(Bitmap^ bitmap);
		//void Release();
		~LockedBitmap();

		void Copy(int X, int Y, Bitmap^ bitmap);
		Byte* GetPointer(int X, int Y);

	};

}