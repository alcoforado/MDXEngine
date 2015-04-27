#include <msclr\marshal_cppstd.h>
#include "stdafx.h"
#include "InteropUtilities.h"
//#include <vcclr.h>
std::string InteropUtilities::ConvertToASCII(String^ str)
{
	msclr::interop::marshal_context context;
	std::string std_string = context.marshal_as<std::string>(str);
	return std_string;
}


std::wstring InteropUtilities::ConvertToUnicode16(String^ str)
{
	msclr::interop::marshal_context context;
	std::wstring std_string = context.marshal_as<std::wstring>(str);
	return std_string;
}

