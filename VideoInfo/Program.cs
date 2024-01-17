using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using DisplayMagicianShared;
using NLog.Config;
using DisplayMagicianShared.NVIDIA;
using DisplayMagicianShared.AMD;
using DisplayMagicianShared.Windows;
using System.Collections.Generic;
using System.Linq;

namespace VideoInfo
{
    class Program
    {

        public struct VIDEOINFO_DISPLAY_CONFIG
        {
            public NVIDIA_DISPLAY_CONFIG NVIDIAConfig;
            public AMD_DISPLAY_CONFIG AMDConfig;
            public WINDOWS_DISPLAY_CONFIG WindowsConfig;
        }

        static VIDEOINFO_DISPLAY_CONFIG myDisplayConfig = new VIDEOINFO_DISPLAY_CONFIG();

        static NVIDIALibrary nvidiaLibrary = null;
        static AMDLibrary amdLibrary = null;
        static WinLibrary winLibrary = null;


        static void Main(string[] args)
        {
            // Prepare NLog for logging
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            //string date = DateTime.Now.ToString("yyyyMMdd.HHmmss");
            string AppLogFilename = Path.Combine($"VideoInfo.log");

            // Rules for mapping loggers to targets          
            NLog.LogLevel logLevel = NLog.LogLevel.Trace;

            // Create the log file target
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = AppLogFilename,
                DeleteOldFileOnStartup = true
            };

            // Create a logging rule to use the log file target
            var loggingRule = new LoggingRule("LogToFile");
            loggingRule.EnableLoggingForLevels(logLevel, NLog.LogLevel.Fatal);
            loggingRule.Targets.Add(logfile);
            loggingRule.LoggerNamePattern = "*";
            config.LoggingRules.Add(loggingRule);

            // Apply config           
            NLog.LogManager.Configuration = config;

            // Start the Log file
            SharedLogger.logger.Info($"VideoInfo/Main: Starting VideoInfo v1.8.4");


            Console.WriteLine($"\nVideoInfo v1.8.4");
            Console.WriteLine($"=================");
            Console.WriteLine($"(c) Terry MacDonald 2023\n");

            // First check that we have an Video Card in this PC that we support
            List<string> videoCardVendors = WinLibrary.GetLibrary().GetAllPCIVideoCardVendors();
            if (!NVIDIALibrary.GetLibrary().PCIVendorIDs.All(value => videoCardVendors.Contains(value)))
            {
                SharedLogger.logger.Error($"VideoInfo/Main: There are no NVIDIA Video Cards enabled within this computer. VideoInfo requires at least one NVIDIA Video Card to work. Please use DisplayMagician instead.");
                Console.WriteLine($"ERROR - There are no NVIDIA Video Cards enabled within this computer. VideoInfo requires at least one NVIDIA Video Card to work.");
                Console.WriteLine($"        Please use DisplayMagician instead. See https://displaymagician.littlebitbig.com for more information.");
                Console.WriteLine();
                Environment.Exit(1);
            }

            // Update the configuration
            nvidiaLibrary = NVIDIALibrary.GetLibrary();
            amdLibrary = AMDLibrary.GetLibrary();
            winLibrary = WinLibrary.GetLibrary();

            if (args.Length > 0)
            {
                if (args[0] == "save")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: The save command was provided");
                    if (args.Length != 2)
                    {
                        Console.WriteLine($"ERROR - You need to provide a filename in which to save display settings");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - You need to provide a filename in which to save display settings");
                        Environment.Exit(1);
                    }
                    SharedLogger.logger.Debug($"VideoInfo/Main: Attempting to save the display settings to {args[1]} as save command was provided");
                    saveToFile(args[1]);
                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine($"ERROR - Couldn't save settings to the file {args[1]}");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't save settings to the file {args[1]}");
                        Environment.Exit(1);
                    }
                }
                else if (args[0] == "load")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: The load command was provided");
                    if (args.Length != 2)
                    {
                        Console.WriteLine($"ERROR - You need to provide a filename from which to load display settings");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - You need to provide a filename from which to load display settings");
                        Environment.Exit(1);
                    }
                    SharedLogger.logger.Debug($"VideoInfo/Main: Attempting to use the display settings in {args[1]} as load command was provided");
                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine($"ERROR - Couldn't find the file {args[1]} to load settings from it");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't find the file {args[1]} to load settings from it");
                        Environment.Exit(1);
                    }
                    loadFromFile(args[1]);
                }
                else if (args[0] == "possible")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: The possible command was provided");
                    if (args.Length != 2)
                    {
                        Console.WriteLine($"ERROR - You need to provide a filename from which we will check if the display settings are possible");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - You need to provide a filename from which we will check if the display settings are possible");
                        Environment.Exit(1);
                    }
                    SharedLogger.logger.Debug($"VideoInfo/Main: showing if the {args[1]} is a valid display cofig file as possible command was provided");
                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine($"ERROR - Couldn't find the file {args[1]} to check the settings from it");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't find the file {args[1]} to check the settings from it");
                        Environment.Exit(1);
                    }
                    possibleFromFile(args[1]);
                }
                else if (args[0] == "equal")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: The equal command was provided");
                    if (args.Length == 3)
                    {
                        if (!File.Exists(args[1]))
                        {
                            Console.WriteLine($"ERROR - Couldn't find the file {args[1]} to check the settings from it\n");
                            SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't find the file {args[1]} to check the settings from it");
                            Environment.Exit(1);
                        }
                        if (!File.Exists(args[2]))
                        {
                            Console.WriteLine($"ERROR - Couldn't find the file {args[2]} to check the settings from it\n");
                            SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't find the file {args[2]} to check the settings from it");
                            Environment.Exit(1);
                        }
                        equalFromFiles(args[1], args[2]);
                    }
                    else if (args.Length == 2)
                    {
                        if (!File.Exists(args[1]))
                        {
                            Console.WriteLine($"ERROR - Couldn't find the file {args[1]} to check the settings from it\n");
                            SharedLogger.logger.Error($"VideoInfo/Main: ERROR - Couldn't find the file {args[1]} to check the settings from it");
                            Environment.Exit(1);
                        }
                        equalFromFiles(args[1]);
                    }
                    else
                    {
                        Console.WriteLine($"ERROR - You need to provide two filenames in order for us to see if they are equal.");
                        Console.WriteLine($"        Equal means they are exactly the same.");
                        SharedLogger.logger.Error($"VideoInfo/Main: ERROR - You need to provide two filenames in order for us to see if they are equal.");
                        Environment.Exit(1);
                    }
                }
                else if (args[0] == "currentids")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: showing currently connected display ids as currentids command was provided");
                    Console.WriteLine("The current display identifiers are:");
                    SharedLogger.logger.Info($"VideoInfo/Main: The current display identifiers are:");
                    foreach (string displayId in nvidiaLibrary.CurrentDisplayIdentifiers)
                    {
                        Console.WriteLine(@displayId);
                        SharedLogger.logger.Info($@"{displayId}");
                    }
                }
                else if (args[0] == "allids")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: showing all display ids as allids command was provided");
                    Console.WriteLine("All connected display identifiers are:");
                    SharedLogger.logger.Info($"VideoInfo/Main: All connected display identifiers are:");
                    foreach (string displayId in nvidiaLibrary.GetAllConnectedDisplayIdentifiers())
                    {
                        Console.WriteLine(@displayId);
                        SharedLogger.logger.Info($@"{displayId}");
                    }
                }
                else if (args[0] == "print")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: printing display info as print command was provided");
                    Console.WriteLine(nvidiaLibrary.PrintActiveConfig());                    
                }

                else if (args[0] == "help" || args[0] == "--help" || args[0] == "-h" || args[0] == "/?" || args[0] == "-?")
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: Showing help as help command was provided");
                    showHelp();
                    Environment.Exit(1);
                }
                else
                {
                    SharedLogger.logger.Debug($"VideoInfo/Main: Showing help as an invalid command was provided");
                    showHelp();
                    Console.WriteLine("*** ERROR - Invalid command line parameter provided! ***\n");
                    Environment.Exit(1);
                }
            }
            else
            {
                SharedLogger.logger.Debug($"VideoInfo/Main: Showing help as help command was provided");
                showHelp();
                Environment.Exit(1);                
            }
            Console.WriteLine();
            Environment.Exit(0);
        }

        static void showHelp()
        {
            Console.WriteLine($"VideoInfo is a little program to help test setting display layout and HDR settings in Windows 10 64-bit and later.\n");
            Console.WriteLine($"You need to have the latest NVIDIA Driver installed and an NVIDIA video card in order to run this software.\n");
            Console.WriteLine($"You can run it without any command line parameters, and it will print all the information it can find from the \nNVIDIA driver and the Windows Display CCD and GDI interfaces.\n");
            Console.WriteLine($"You can also run it with 'VideoInfo save myfilename.cfg' and it will save the current display configuration into\nthe myfilename.cfg file.\n");
            Console.WriteLine($"This is most useful when you subsequently use the 'VideoInfo load myfilename.cfg' command, as it will load the\ndisplay configuration from the myfilename.cfg file and make it live. In this way, you can make yourself a library\nof different cfg files with different display layouts, then use the VideoInfo load command to swap between them.\n\n");
            Console.WriteLine($"Valid commands:\n");
            Console.WriteLine($"\t'VideoInfo print' will print information about your current display setting.");
            Console.WriteLine($"\t'VideoInfo save myfilename.cfg' will save your current display setting to the myfilename.cfg file.");
            Console.WriteLine($"\t'VideoInfo load myfilename.cfg' will load and apply the display setting in the myfilename.cfg file.");
            Console.WriteLine($"\t'VideoInfo possible myfilename.cfg' will test the display setting in the myfilename.cfg file to see\n\t\tif it is possible to use that display profile now.");
            Console.WriteLine($"\t'VideoInfo equal myfilename.cfg' will test if the display setting in the myfilename.cfg is equal to\n\t\tthe one in use.");
            Console.WriteLine($"\t'VideoInfo equal myfilename.cfg myother.cfg' will test if the display setting in the myfilename.cfg\n\t\tis equal to the one in myother.cfg.");
            Console.WriteLine($"\t'VideoInfo currentids' will display the display identifiers for all active displays.");
            Console.WriteLine($"\t'VideoInfo allids' will display the display identifiers for all displays that are active or can be \n\t\tmade active.");
            Console.WriteLine($"\nUse DisplayMagician to store display settings for each game you have. https://github.com/terrymacdonald/DisplayMagician\n");
        }

        static void saveToFile(string filename)
        {
            SharedLogger.logger.Trace($"VideoInfo/saveToFile: Attempting to save the current display configuration to the {filename}.");

            SharedLogger.logger.Trace($"VideoInfo/saveToFile: Getting the current Active Config");
            // Get the current configuration
            if (nvidiaLibrary.IsInstalled)
            {
                myDisplayConfig.NVIDIAConfig = nvidiaLibrary.ActiveDisplayConfig;
            }            
            if (amdLibrary.IsInstalled)
            {
                myDisplayConfig.AMDConfig = amdLibrary.ActiveDisplayConfig;
            }            
            myDisplayConfig.WindowsConfig = winLibrary.ActiveDisplayConfig;

            SharedLogger.logger.Trace($"VideoInfo/saveToFile: Attempting to convert the current Active Config objects to JSON format");
            // Save the object to file!
            try
            {
                SharedLogger.logger.Trace($"VideoInfo/saveToFile: Attempting to convert the current Active Config objects to JSON format");

                var json = JsonConvert.SerializeObject(myDisplayConfig, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Populate,
                    TypeNameHandling = TypeNameHandling.Auto

                });


                if (!string.IsNullOrWhiteSpace(json))
                {
                    SharedLogger.logger.Error($"VideoInfo/saveToFile: Saving the display settings to {filename}.");

                    File.WriteAllText(filename, json, Encoding.Unicode);

                    SharedLogger.logger.Error($"VideoInfo/saveToFile: Display settings successfully saved to {filename}.");
                    Console.WriteLine($"Display settings successfully saved to {filename}.");
                }
                else
                {
                    SharedLogger.logger.Error($"VideoInfo/saveToFile: The JSON string is empty after attempting to convert the current Active Config objects to JSON format");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/saveToFile: ERROR - Unable to save the profile repository to the {filename}.");
                SharedLogger.logger.Error(ex, $"VideoInfo/saveToFile: Saving the display settings to the {filename}.");
            }
        }

        static void loadFromFile(string filename)
        {
            string json = "";
            try
            {
                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Attempting to load the display configuration from {filename} to use it.");
                json = File.ReadAllText(filename, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/loadFromFile: ERROR - Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
                SharedLogger.logger.Error(ex, $"VideoInfo/loadFromFile: Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
            }

            if (!string.IsNullOrWhiteSpace(json))
            {
                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Contents exist within {filename} so trying to read them as JSON.");
                try
                {
                    myDisplayConfig = JsonConvert.DeserializeObject<VIDEOINFO_DISPLAY_CONFIG>(json, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Successfully parsed {filename} as JSON.");

                    // We have to patch the adapter IDs after we load a display config because Windows changes them after every reboot :(
                    winLibrary.PatchWindowsDisplayConfig(ref myDisplayConfig.WindowsConfig);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"VideoInfo/loadFromFile: ERROR - Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    SharedLogger.logger.Error(ex, $"VideoInfo/loadFromFile: Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    return;
                }

                // Check it's not in use already
                if (!profileAlreadyInUse(myDisplayConfig))
                {
                    bool applyNVIDIASettings = false;
                    bool applyAMDSettings = false;
                    bool itWorkedforNVIDIA = false;
                    bool itWorkedforAMD = false;
                    bool itWorkedforWindows = false;
                    bool itWorkedforNVIDIAOverride = false;
                    bool itWorkedforAMDOverride = false;
                    bool errorApplyingSomething = false;

                    if (nvidiaLibrary.IsInstalled)                        
                    {
                        if (myDisplayConfig.NVIDIAConfig.DisplayIdentifiers.Count > 0)
                        {
                            if (nvidiaLibrary.IsPossibleConfig(myDisplayConfig.NVIDIAConfig))
                            {
                                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings within {filename} are possible to use right now, so we'll use attempt to use them shortly.");
                                applyNVIDIASettings = true;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings within {filename} were NOT possible to be applied.");
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying NVIDIA display settings as no screens are connected to the NVIDIA video card.");
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying NVIDIA display settings as the NVIDIA library isn't installed.");
                    }

                    if (amdLibrary.IsInstalled)
                    {
                        if (myDisplayConfig.AMDConfig.DisplayIdentifiers.Count > 0)
                        {
                            if (amdLibrary.IsPossibleConfig(myDisplayConfig.AMDConfig))
                            {
                                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings within {filename} are possible to use right now, so we'll use attempt to use them.");
                                applyAMDSettings = true;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings within {filename} were NOT possible to be applied.");
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying AMD display settings as the AMD library isn't installed.");
                        }

                    }
                    else
                    {
                        SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying AMD display settings as the AMD library isn't installed.");
                    }

                    if (applyNVIDIASettings)
                    {
                        Console.WriteLine($"Attempting to apply NVIDIA display config from {filename}");
                        itWorkedforNVIDIA = nvidiaLibrary.SetActiveConfig(myDisplayConfig.NVIDIAConfig);
                        if (itWorkedforNVIDIA)
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings within {filename} were sucessfully applied.");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings within {filename} were NOT applied successfully.");
                            errorApplyingSomething = true;
                        }
                    }

                    if (applyAMDSettings)
                    {
                        Console.WriteLine($"Attempting to apply AMD display config from {filename}");
                        itWorkedforAMD = amdLibrary.SetActiveConfig(myDisplayConfig.AMDConfig);
                        if (itWorkedforAMD)
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings within {filename} were sucessfully applied.");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings within {filename} were NOT applied successfully.");
                            errorApplyingSomething = true;
                        }
                    }

                    // If either AMD or NVIDIA settings were applied, then we need to update our windows layout to make sure it
                    // matches current reality.
                    if ((amdLibrary.IsInstalled && itWorkedforAMD) || (nvidiaLibrary.IsInstalled && itWorkedforNVIDIA))
                    {
                        // if other changes were made, then ets update the screens so Windows knows whats happening
                        // NVIDIA and AMD make such large changes to the available screens in windows, we need to do this.
                        SharedLogger.logger.Trace($"VideoInfo/loadFromFile: NVIDIA and/or AMD display settings within {filename} were applied successfully, so updating Windows Active Config so it knows of the changes made."); 
                        winLibrary.UpdateActiveConfig();                        
                    }                                     

                    // Then let's try to also apply the windows changes
                    // Note: we are unable to check if the Windows CCD display config is possible, as it won't match if either the current display config is a Mosaic config,
                    // or if the display config we want to change to is a Mosaic config. So we just have to assume that it will work!
                    itWorkedforWindows = winLibrary.SetActiveConfig(myDisplayConfig.WindowsConfig);

                    if (itWorkedforWindows)
                    {
                        SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The Windows CCD display settings within {filename} were applied correctly, so now attempting to apply any overrides.");

                        if (applyNVIDIASettings)
                        {
                            if (itWorkedforNVIDIA)
                            {
                                itWorkedforNVIDIAOverride = nvidiaLibrary.SetActiveConfigOverride(myDisplayConfig.NVIDIAConfig);

                                if (itWorkedforNVIDIAOverride)
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings that override windows within {filename} were applied correctly.");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The NVIDIA display settings that override windows within {filename} were NOT applied correctly.");
                                    Console.WriteLine($"ERROR - VideoInfo NVIDIA Override settings were not applied correctly.");
                                    errorApplyingSomething = true;
                                }
                            }
                            else
                            {
                                if (nvidiaLibrary.IsInstalled)
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying NVIDIA display overrides as the NVIDIA display settings didn't apply correctly!");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying NVIDIA display overrides as the NVIDIA library isn't installed.");
                                }
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying NVIDIA display overrides as the NVIDIA video card doesn't have any displays in this profile.");
                        }

                        if (applyAMDSettings)
                        {
                            if (itWorkedforAMD)
                            {
                                itWorkedforAMDOverride = amdLibrary.SetActiveConfigOverride(myDisplayConfig.AMDConfig);

                                if (itWorkedforAMDOverride)
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings that override windows within {filename} were applied correctly.");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The AMD display settings that override windows within {filename} were NOT applied correctly.");
                                    Console.WriteLine($"ERROR - VideoInfo AMD Override settings were not applied correctly.");
                                    errorApplyingSomething = true;
                                }
                            }
                            else
                            {
                                if (amdLibrary.IsInstalled)
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying AMD display overrides as the AMD display settings didn't apply correctly!");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying AMD display overrides as the AMD library isn't installed.");
                                }
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"VideoInfo/loadFromFile: Skipping applying AMD display overrides as the AMD video card doesn't have any displays in this profile.");
                        }

                    }
                    else
                    {
                        SharedLogger.logger.Trace($"VideoInfo/loadFromFile: The Windows CCD display settings within {filename} were NOT applied correctly, so skipping setting the overrides.");
                        Console.WriteLine($"ERROR - VideoInfo Windows CCD settings were not applied correctly so skipping setting the overrides.");
                    }

                    // Give the final error if there are any
                    if (errorApplyingSomething)
                    {
                        SharedLogger.logger.Info($"VideoInfo/loadFromFile: VideoInfo was unable to successfully apply your display profile within {filename}.");
                        Console.WriteLine($"ERROR - VideoInfo was unable to successfully apply your display profile within {filename}.");
                    }
                    else 
                    {
                        SharedLogger.logger.Info($"VideoInfo/loadFromFile: VideoInfo successfully applied your display profile contained within {filename}.");
                        Console.WriteLine($"VideoInfo successfully applied your display profile contained within {filename}.");
                    }
                }
                else
                {
                    Console.WriteLine($"The display settings in {filename} are already installed. No need to install them again. Exiting.");
                    SharedLogger.logger.Info($"VideoInfo/loadFromFile: The display settings in {filename} are already installed. No need to install them again. Exiting.");
                }

            }
            else
            {
                Console.WriteLine($"ERROR - The {filename} profile JSON file exists but is empty! So we're going to treat it as if it didn't exist.");
                SharedLogger.logger.Error($"VideoInfo/loadFromFile: The {filename} profile JSON file exists but is empty! So we're going to treat it as if it didn't exist.");
            }
        }

        static bool profileAlreadyInUse(VIDEOINFO_DISPLAY_CONFIG myDisplayConfig)
        {
            bool winAlreadyInUse = winLibrary.IsActiveConfig(myDisplayConfig.WindowsConfig);
            bool nvidiaAlreadyInUse = true;
            bool amdAlreadyInUse = true;

            if ((amdLibrary.IsInstalled && !amdLibrary.IsActiveConfig(myDisplayConfig.AMDConfig)))
            {
                amdAlreadyInUse = false;
            }
            if ((nvidiaLibrary.IsInstalled && !nvidiaLibrary.IsActiveConfig(myDisplayConfig.NVIDIAConfig)))
            {
                nvidiaAlreadyInUse = false;
            }
            
            return winAlreadyInUse && amdAlreadyInUse && nvidiaAlreadyInUse;

        }

        static void possibleFromFile(string filename)
        {
            
            string json = "";
            try
            {
                SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: Attempting to load the display configuration from {filename} to see if it's possible.");
                json = File.ReadAllText(filename, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/possibleFromFile: ERROR - Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
                SharedLogger.logger.Error(ex, $"VideoInfo/possibleFromFile: Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
            }

            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: Contents exist within {filename} so trying to read them as JSON.");
                    myDisplayConfig = JsonConvert.DeserializeObject<VIDEOINFO_DISPLAY_CONFIG>(json, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: Successfully parsed {filename} as JSON.");

                    // We have to patch the adapter IDs after we load a display config because Windows changes them after every reboot :(
                    WinLibrary.GetLibrary().PatchWindowsDisplayConfig(ref myDisplayConfig.WindowsConfig);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"VideoInfo/possibleFromFile: ERROR - Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    SharedLogger.logger.Error(ex, $"VideoInfo/possibleFromFile: Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    return;
                }

                if (nvidiaLibrary.IsPossibleConfig(myDisplayConfig.NVIDIAConfig) && winLibrary.IsPossibleConfig(myDisplayConfig.WindowsConfig))
                {
                    SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: The NVIDIA & Windows CCD display settings in {filename} are compatible with this computer.");
                    Console.WriteLine($"The NVIDIA display settings in {filename} are compatible with this computer.");
                    Console.WriteLine($"You can apply them with the command 'VideoInfo load {filename}'");
                }
                else
                {
                    SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: The {filename} file contains a display setting that will NOT work on this computer right now.");
                    SharedLogger.logger.Trace($"VideoInfo/possibleFromFile: This may be because the required screens are turned off, or some other change has occurred on the PC.");
                    Console.WriteLine($"The {filename} file contains a display setting that will NOT work on this computer right now.");
                    Console.WriteLine($"This may be because the required screens are turned off, or some other change has occurred on the PC.");
                }

            }
            else
            {
                SharedLogger.logger.Error($"VideoInfo/possibleFromFile: The {filename} profile JSON file exists but is empty! So we're going to treat it as if it didn't exist.");
                Console.WriteLine($"VideoInfo/possibleFromFile: The {filename} profile JSON file exists but is empty! So we're going to treat it as if it didn't exist.");
            }
        }

        static void equalFromFiles(string filename, string otherFilename)
        {
            string json = ""; 
            string otherJson = "";
            VIDEOINFO_DISPLAY_CONFIG displayConfig = new VIDEOINFO_DISPLAY_CONFIG();
            VIDEOINFO_DISPLAY_CONFIG otherDisplayConfig = new VIDEOINFO_DISPLAY_CONFIG();
            SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Attempting to compare the display configuration from {filename} and {otherFilename} to see if they are equal.");
            try
            {
                json = File.ReadAllText(filename, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
                SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
            }

            try
            {
                otherJson = File.ReadAllText(otherFilename, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to read the JSON file {otherFilename} to memory but File.ReadAllTextthrew an exception.");
                SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to read the JSON file {otherFilename} to memory but File.ReadAllTextthrew an exception.");
            }

            if (!string.IsNullOrWhiteSpace(json)&&!string.IsNullOrWhiteSpace(otherJson))
            {
                try
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Contents exist within {filename} so trying to read them as JSON.");
                    displayConfig = JsonConvert.DeserializeObject<VIDEOINFO_DISPLAY_CONFIG>(json, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Successfully parsed {filename} as JSON.");

                    // We have to patch the adapter IDs after we load a display config because Windows changes them after every reboot :(
                    WinLibrary.GetLibrary().PatchWindowsDisplayConfig(ref displayConfig.WindowsConfig);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    return;
                }
                try
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Contents exist within {otherFilename} so trying to read them as JSON.");
                    otherDisplayConfig = JsonConvert.DeserializeObject<VIDEOINFO_DISPLAY_CONFIG>(otherJson, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Successfully parsed {filename} as JSON.");

                    // We have to patch the adapter IDs after we load a display config because Windows changes them after every reboot :(
                    WinLibrary.GetLibrary().PatchWindowsDisplayConfig(ref otherDisplayConfig.WindowsConfig);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    return;
                }

                if (displayConfig.WindowsConfig.Equals(otherDisplayConfig.WindowsConfig) && displayConfig.NVIDIAConfig.Equals(otherDisplayConfig.NVIDIAConfig))
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: The NVIDIA display settings in {filename} and {otherFilename} are equal.");
                    Console.WriteLine($"The NVIDIA display settings in {filename} and {otherFilename} are equal.");
                }
                else
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: The NVIDIA display settings in {filename} and {otherFilename} are NOT equal.");
                    Console.WriteLine($"The NVIDIA display settings in {filename} and {otherFilename} are NOT equal.");
                }

            }
            else
            {
                SharedLogger.logger.Error($"VideoInfo/equalFromFile: The {filename} or {otherFilename} JSON files exist but at least one of them is empty! Cannot continue.");
                Console.WriteLine($"VideoInfo/equalFromFile: The {filename} or {otherFilename} JSON files exist but at least one of them is empty! Cannot continue.");
            }
        }

        static void equalFromFiles(string filename)
        {
            string json = "";
            VIDEOINFO_DISPLAY_CONFIG displayConfig = new VIDEOINFO_DISPLAY_CONFIG();
            SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Attempting to compare the display configuration from {filename} and the currently active display configuration to see if they are equal.");
            try
            {
                json = File.ReadAllText(filename, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
                SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to read the JSON file {filename} to memory but File.ReadAllTextthrew an exception.");
            }

            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Contents exist within {filename} so trying to read them as JSON.");
                    displayConfig = JsonConvert.DeserializeObject<VIDEOINFO_DISPLAY_CONFIG>(json, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: Successfully parsed {filename} as JSON.");

                    // We have to patch the adapter IDs after we load a display config because Windows changes them after every reboot :(
                    WinLibrary.GetLibrary().PatchWindowsDisplayConfig(ref displayConfig.WindowsConfig);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"VideoInfo/equalFromFile: ERROR - Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    SharedLogger.logger.Error(ex, $"VideoInfo/equalFromFile: Tried to parse the JSON in the {filename} but the JsonConvert threw an exception.");
                    return;
                }
                if (displayConfig.WindowsConfig.Equals(WinLibrary.GetLibrary().GetActiveConfig()) && displayConfig.NVIDIAConfig.Equals(NVIDIALibrary.GetLibrary().GetActiveConfig()))
                //if (displayConfig.NVIDIAConfig.Equals(NVIDIALibrary.GetLibrary().GetActiveConfig()))
                //if (displayConfig.WindowsConfig.Equals(WinLibrary.GetLibrary().GetActiveConfig()))
                { 
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: The NVIDIA display settings in {filename} and the currently active display configuration are equal.");
                    Console.WriteLine($"The NVIDIA display settings in {filename} and the currently active display configuration are equal.");
                }
                else
                {
                    SharedLogger.logger.Trace($"VideoInfo/equalFromFile: The NVIDIA display settings in {filename} and the currently active display configuration are NOT equal.");
                    Console.WriteLine($"The NVIDIA display settings in {filename} and the currently active display configuration are NOT equal.");
                }

            }
            else
            {
                SharedLogger.logger.Error($"VideoInfo/equalFromFile: The {filename} JSON file exists but is empty! Cannot continue.");
                Console.WriteLine($"VideoInfo/equalFromFile: The {filename} JSON file exists but is empty! Cannot continue.");
            }
        }

    }
}
