

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
namespace MFreeType {
	ref class MFreeTypeException : public System::Exception 
	{
		static Dictionary<int, String^>^ _error_map = nullptr;
		int _error_code;
	public:
		static String^ GetErrorMessage(int error);
		MFreeTypeException(int error);
		~MFreeTypeException();


	};
}