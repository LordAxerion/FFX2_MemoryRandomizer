﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace FFX2MemoryReader
{
    /// <summary> 
    /// ProcessMemoryReader is a class that enables direct reading a process memory 
    /// </summary> 
    class ProcessMemoryReaderApi
    {
        // constants information can be found in <winnt.h> 
        [Flags]
        public enum ProcessAccessType
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400)
        }

        // function declarations are found in the MSDN and in <winbase.h> 

        //      HANDLE OpenProcess( 
        //         DWORD dwDesiredAccess,  // access flag 
        //         BOOL bInheritHandle,    // handle inheritance option 
        //         DWORD dwProcessId       // process identifier 
        //         ); 
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

        //      BOOL CloseHandle( 
        //         HANDLE hObject   // handle to object 
        //         ); 
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);

        //      BOOL ReadProcessMemory( 
        //         HANDLE hProcess,              // handle to the process 
        //         LPCVOID lpBaseAddress,        // base of memory area 
        //         LPVOID lpBuffer,              // data buffer 
        //         SIZE_T nSize,                 // number of bytes to read 
        //         SIZE_T * lpNumberOfBytesRead  // number of bytes read 
        //         ); 
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        //      BOOL WriteProcessMemory( 
        //         HANDLE hProcess,                // handle to process 
        //         LPVOID lpBaseAddress,           // base of memory area 
        //         LPCVOID lpBuffer,               // data buffer 
        //         SIZE_T nSize,                   // count of bytes to write 
        //         SIZE_T * lpNumberOfBytesWritten // count of bytes written 
        //         ); 
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);


    }
    public class ProcessMemoryReader
    {
        public Process ReadProcess { get; set; }
        private IntPtr handle;

        public ProcessMemoryReader(Process process)
        {
            ReadProcess = process;
        }

        public void OpenProcess()
        {
            try
            {
                ProcessMemoryReaderApi.ProcessAccessType access =
                    ProcessMemoryReaderApi.ProcessAccessType.PROCESS_QUERY_INFORMATION |
                    ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_READ |
                    ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_WRITE |
                    ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_OPERATION;
                handle = ProcessMemoryReaderApi.OpenProcess((uint)access, 1, (uint)ReadProcess.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Opening Process. Maybe ReadProcess wasn't defined.", ex);
                throw ex;
            }
        }

        public void CloseHandle()
        {
            if (ProcessMemoryReaderApi.CloseHandle(handle) != 0)
            {
                Console.WriteLine("Closing handle failed.");
                throw new Exception("Closing handle failed.");
            }
        }

        public byte[] ReadMemory(IntPtr memoryAdress, uint bytesToRead, out int bytesRead)
        {
            byte[] buffer = new byte[bytesToRead];
            IntPtr p_BytesRead = IntPtr.Zero;
            ProcessMemoryReaderApi.ReadProcessMemory(handle, memoryAdress, buffer, bytesToRead, out p_BytesRead);

            bytesRead = p_BytesRead.ToInt32();
            return buffer;
        }

        public int WriteMemory(IntPtr memoryAdress, byte[] buffer, out int bytesWritten)
        {
            try
            {
                IntPtr p_BytesWritten;

                ProcessMemoryReaderApi.WriteProcessMemory(handle, memoryAdress, buffer, buffer.Length, out p_BytesWritten);
                bytesWritten = p_BytesWritten.ToInt32();
            }
            catch
            {
                bytesWritten = 0;
            }
            return Marshal.GetLastWin32Error();
        }
    }
}
