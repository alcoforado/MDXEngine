// This is the main DLL file.
#include <msclr\marshal_cppstd.h>
#include "stdafx.h"
#include "MFreeType.h"
#include <vcclr.h>
#include <cstdlib>
#include <string>


namespace MFreeType {
	


	MFreeType::MFreeType()
	{
		int error;
		pin_ptr<FT_Library> pinnedLibrary = &library;
		error = FT_Init_FreeType(pinnedLibrary);
		library = *pinnedLibrary;
		
		
		if (error)
			throw gcnew Exception(this->GetErrorMessage(error));
	}

	MFont^ MFreeType::GetFont(FileInfo^ file)
	{
		pin_ptr<FT_Library> pinnedLibrary = &library;

		if (!_font_cash.ContainsKey(file->FullName))
		{
			std::string str;

			const char *p = str.c_str();

			//Else load the font add it in cash and return
			msclr::interop::marshal_context context;
			std::string fileName = context.marshal_as<std::string>(file->FullName);
			FT_Face face;
			int error = FT_New_Face(library, fileName.c_str(), 0, &face);

			if (error)
			{
				throw gcnew Exception(this->GetErrorMessage(error));
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
		if (_error_map.Count == 0)
		{
				#undef FTERRORS_H                                                
				#define FT_ERRORDEF( e, v, s )  _error_map.Add(e,s);        
			    #define FT_ERROR_START_LIST  {                   
			    #define FT_ERROR_END_LIST    }          
			    #include <fterrors.h>
		}
		
		if (_error_map.ContainsKey(error))
			return _error_map[error];
		else
			return "Unknown Error has occured";
	}
}

