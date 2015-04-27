#pragma once

using namespace System;
#include <string>

ref class InteropUtilities
{
public:
	static std::string ConvertToASCII(String^ string);
	static std::wstring ConvertToUnicode16(String^ string);
};

