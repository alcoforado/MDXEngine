#include "stdafx.h"
#include "LockBitmap.h"
#include <assert.h>
#include <algorithm>
using namespace MFreeType;

LockedBitmap::LockedBitmap(Bitmap^ bitmap)
{
	//For now only support for indexed 8bpp.
	//Consider to use Graphics if you have other format.
	assert(bitmap->PixelFormat == PixelFormat::Format8bppIndexed);
	_bitmap = bitmap;
	_bitmapData = _bitmap->LockBits(
		Rectangle(0, 0, _bitmap->Width, _bitmap->Height),
		ImageLockMode::ReadWrite,
		PixelFormat::Format8bppIndexed);
	_pData = reinterpret_cast<Byte*>(_bitmapData->Scan0.ToPointer());
}

LockedBitmap::~LockedBitmap()
{
	_bitmap->UnlockBits(_bitmapData);

}


Byte* LockedBitmap::GetPointer(int X, int Y)
{
	assert(X < _bitmap->Width);
	assert(Y < _bitmap->Height);
	return _pData + X + Y*_bitmapData->Stride;

}

void LockedBitmap::Copy(int X,int Y,Bitmap^ src){
	assert(src->PixelFormat == PixelFormat::Format8bppIndexed);
	assert(X < _bitmap->Width);
	assert(Y < _bitmap->Height);

	int width   = std::min(_bitmap->Width - X, src->Width);
	int height = std::min(_bitmap->Height - Y, src->Height);

	BitmapData^ srcData = src->LockBits(
		Rectangle(0, 0, src->Width, src->Height),
		ImageLockMode::ReadOnly,
		PixelFormat::Format8bppIndexed);
     Byte* pSrc = reinterpret_cast<Byte*>(srcData->Scan0.ToPointer()); 
	 int src_stride = srcData->Stride;
	 int dst_stride = _bitmapData->Stride;
	 
	 Byte* pDst = this->GetPointer(X, Y);
	 for (int y = 0; y < height; y++)
	 {
		 memcpy(pDst, pSrc, width);
		 pDst += dst_stride;
		 pSrc += src_stride;
	 }
	 
	 //unlock the source
	 src->UnlockBits(srcData);
	 

}