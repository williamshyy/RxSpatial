// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.


using System;
using System.Runtime.InteropServices;

namespace LoadTestApplication
{

public static class HighResolutionDateTime
{
    public static bool IsAvailable { get; private set; }

    [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
    private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

    public static DateTime UtcNow
    {
        get
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException(
                    "High resolution clock isn't available.");
            }

            long filetime;
            GetSystemTimePreciseAsFileTime(out filetime);

            return DateTime.FromFileTimeUtc(filetime);
        }
    }

    static HighResolutionDateTime()
    {
        try
        {
            long filetime;
            GetSystemTimePreciseAsFileTime(out filetime);
            IsAvailable = true;
        }
        catch (EntryPointNotFoundException)
        {
            // Not running Windows 8 or higher.
            IsAvailable = false;
        }
    }
}
}
