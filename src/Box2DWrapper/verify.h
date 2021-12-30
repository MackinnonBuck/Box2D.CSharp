#pragma once

#include <iostream>

#ifndef NDEBUG
#	define VERIFY_INSTANCE __verify_instance(obj);
#else
#   define VERIFY_INSTANCE do { } while (false)
#endif

inline void __verify_instance(void* obj)
{
    if (obj == nullptr)
    {
        std::cerr << __FUNCTION__ << "(): ERROR: Cannot access a managed object after it has been deleted." << std::endl;
        std::terminate();
    }
}

