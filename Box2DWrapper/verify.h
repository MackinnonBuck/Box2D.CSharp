#pragma once

#include <iostream>

#ifndef NDEBUG
#	define VERIFY_INSTANCE\
    do\
    {\
        if (obj == nullptr)\
        {\
            std::cerr << __FUNCTION__ << "(): ERROR: Cannot access a managed object after it has been deleted." << std::endl;\
            std::terminate();\
        }\
    } while (false)
#else
#   define VERIFY_INSTANCE do { } while (false)
#endif


