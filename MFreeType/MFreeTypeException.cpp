#define generic _generic
#include <ft2build.h>
#include FT_FREETYPE_H
#undef generic

#include "stdafx.h"
#include "MFreeTypeException.h"

namespace MFreeType {
	MFreeTypeException::MFreeTypeException(int error)
		:Exception(GetErrorMessage(error))
	{
		this->_error_code = error;
	}

	String^ MFreeTypeException::GetErrorMessage(int error)
	{
		if (_error_map == nullptr)
		{
			_error_map = gcnew  Dictionary<int, String^>();

#undef __FTERRORS_H__                                                
#define FT_ERRORDEF( e, v, s )  _error_map->Add(e,s);        
#define FT_ERROR_START_LIST  {                   
#define FT_ERROR_END_LIST    }    
#include <fterrors.h>
		}

		if (_error_map->ContainsKey(error))
			return _error_map[error];
		else
			return "Unknown Error has occured";
	}

	MFreeTypeException::~MFreeTypeException()
	{
	}
}