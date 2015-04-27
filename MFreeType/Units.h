#pragma once
ref class Units
{
public:
	Units();
	static int  FontUnitToPixel(int fts)
	{
		return fts >> 6;
	}
};

