#pragma once

#ifdef BOX2D_EXPORTS
#define BOX2D_API __declspec(dllexport)
#else
#define BOX2D_API __declspec(dllimport)
#endif
