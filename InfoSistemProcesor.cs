using System;
using System.Management;
using Microsoft.Win32;

namespace Miroslav_project
{
    public class SystemInfo
    {
                public void getOperatingSystemInfo()
                {
                    Console.WriteLine("Displaying operating system info...\n");

                    ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                    foreach (ManagementObject managementObject in mos.Get())
                    {
                        if (managementObject["Caption"] != null)
                        {
                            Console.WriteLine("Operating System Name  :  " + managementObject["Caption"].ToString());
                        }
                        if (managementObject["OSArchitecture"] != null)
                        {
                            Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   
                        }
                        if (managementObject["CSDVersion"] != null)
                        {
                            Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());    
                        }
                    }
                }
                public void getProcessorInfo()
                {
                    Console.WriteLine("\n\nDisplaying Processor Name...");
                    RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);  
                    if (processor_name != null)
                    {
                        if (processor_name.GetValue("ProcessorNameString") != null)
                        {
                            Console.WriteLine(processor_name.GetValue("ProcessorNameString"));  
                        }
                    }
                    DateTime now = DateTime.Now;
                    Console.WriteLine("\nDisplaying ... \n");
                    Console.Write("The current time and date : ");
                    Console.WriteLine(now);
                }
            }
}


