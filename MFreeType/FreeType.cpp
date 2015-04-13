#include "stdafx.h"
#include "FreeType.h"


FreeType::FreeType()
{
		int error;
		error = FT_Init_FreeType(&library);


}


FreeType::~FreeType()
{
}
