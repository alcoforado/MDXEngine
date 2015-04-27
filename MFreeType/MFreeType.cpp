// This is the main DLL file.
#include "stdafx.h"
#include "MFreeType.h"
#include <vcclr.h>
#include <cstdlib>
#include <string>
#include "InteropUtilities.h"

namespace MFreeType {
	


	MFreeType::MFreeType()
	{
		int error;
		pin_ptr<FT_Library> pinnedLibrary = &library;
		error = FT_Init_FreeType(pinnedLibrary);
		library = *pinnedLibrary;
		
		
		if (error)
			throw gcnew MFreeTypeException(error);
	}

	MFont^ MFreeType::GetFont(FileInfo^ file)
	{
		pin_ptr<FT_Library> pinnedLibrary = &library;

		if (!_font_cash.ContainsKey(file->FullName))
		{
			

			//Else load the font add it in cash and return
			std::string fileName = InteropUtilities::ConvertToASCII(file->FullName);
			FT_Face face;
			int error = FT_New_Face(library, fileName.c_str(), 0, &face);

			if (error)
			{
				throw gcnew MFreeTypeException(error);
			}
			
			MFont^ font = gcnew MFont(face);
			_font_cash.Add(file->FullName, font);
		}
		return _font_cash[file->FullName];
	}

	void MFreeType::ClearCash()
	{
		_font_cash.Clear();
	}

	String^  MFreeType::GetErrorMessage(int error)
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
}

