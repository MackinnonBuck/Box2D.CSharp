#pragma once

#include <iostream>

#ifndef NDEBUG
#	define VERIFY_INSTANCE __verify_instance(obj, __FUNCTION__);
#else
#   define VERIFY_INSTANCE do { } while (false)
#endif

inline void __verify_instance(void* obj, const char* func)
{
    if (obj == nullptr)
    {
        std::cerr << func << "(): ERROR: Cannot access a managed object after it has been deleted." << std::endl;
        std::terminate();
    }
}

