# VideoInfo

VideoInfo is a test programme designed to exercise the NvAPI library, AMD ADL2/ADLX Libraries, and Windows CCD/GDI Libraries that were developed for DisplayMagician. This little programme helps me validate that the libraries I created are working properly, and that they will work when added to the main DisplayMagician code - but it can also be used by people to automate change display layouts from the commandline. 

VideoInfo records exactly how you setup your display settings, including NVIDIA Surround/Mosaic screens or AMD Eyefinity screens, display position, resolution, HDR settings, and even which screen is your main one, and then VideoInfo saves those settings to a file. You can set up your display settings exactly how you like them using NVIDIA Setup, AMD Adrenalin Setup and Windows Display Setup, and then use VideoInfo to save those settings to a file for later use. You can then quickly swap between those display profiles you've created.

Command line examples:

- Show what settings you currently are using: `VideoInfo print`
- Save the settings you currently are using to a file to use later: `VideoInfo save my-cool-settings.cfg`
- Load the settings you saved earlier and use them now: `VideoInfo load my-cool-settings.cfg`
- Load the settings you saved earlier but delay 500ms between steps: `VideoInfo load my-cool-settings.cfg --delay:500`
- Load the settings you saved earlier but use the old AMD Eyefinity library: `VideoInfo load my-cool-settings.cfg --oldEyefinity`
- Show whether the display config file can be used: `VideoInfo possible my-cool-settings.cfg`
- Test whether the display config file is equal to the current display layout: `VideoInfo equal my-cool-settings.cfg`
- Test whether the display config file is equal another display config file: `VideoInfo equal my-cool-settings.cfg my-other-settings.cfg`
- Create a Support ZIP File for uploading to Github (collects .cfg and .log files): `VideoInfo zip`


## To setup this software:

- Firstly, set up your display configuration using NVIDIA and AMD settings and the Windows Display settings exactly as you want to use them (e.g. one single NVIDIA Surround window using 3 screens)
- Next, save the settings you currently are using to a file to use later, using a command like `VideoInfo save triple-surround-on.cfg`
- Next, change your display configuration using NVIDIA settings, AMD Adrenaline Setup and the Windows Display settings to another display configuration you'd like to have (e.g. 3 single screens without using NVIDIA Surround)
- Next, save those settings to a different file to use later, using a command like `VideoInfo save triple-screen.cfg`

## To swap between different display setups:

Now that you've set up the different display configurations, you can swap between them using a command like this:

- To load the triple screen setup using NVIDIA surround: `VideoInfo load triple-surround-on.cfg`
- To load the triple screen without NVIDIA surround: `VideoInfo load triple-screen.cfg`

Feel free to use different config file names, and to set up what ever display configurations you like. Enjoy!

Note: This codebase is unlikely to be supported once DisplayMagician is working, but feel free to fork if you would like. Also feel free to send in suggestions for fixes to the C# NVIDIA or AMD library interfaces. Any help is appreciated!

### Adding a delay to cope with slow displays
Some displays are very slow, and can't cope with the display settings being changed too quickly. If you have a slow display, you can add a delay between the steps using the `--delay` option. For example, if you saved your triple screen NVIDIA surround setup previously into surround.cfg previously, you can load it with a 500ms delay between each step using this command: `VideoInfo load surround.cfg --delay:500`

### Using ADL2 instead of ADLX for AMD Eyefinity layouts
Many users do not like the newer way of setting up AMD Eyefinity using the later versions of AMD Adrenalin. They prefer to use the older way of setting it up, using the EyefinityPro.exe that resides in `C:\Program Files\AMD\CNext\CNext\EyefinityPro.exe`. You can use the EyefinityPro tool to configure Eyefinity, and then you can use VideoInfo to save those settings to a file. 

If you set up Eyefinity using the EyefinityPro tool, then I recommend that you also load your Eyefinity config files using the `--oldEyefinity` command line parameter. By default, VideoInfo uses the newer ADLX way of creating Eyefinity, which may not work with Eyefinity displays created using the EyefinityPro tool. We srtongly recommend that you add the `--oldEyefinity` parameter when you load any configs created with the EyefinityPro tool e.g.: `VideoInfo load oldeyefinity.cfg --oldEyefinity`

If you set up Eyefinity using the AMD Adrenalin tool, then you do not need to change anything. VideoInfo uses the newer ADLX way of interacting with Eyefinity displays by default. You can continue to use VideoInfo as you always have e.g.: `VideoInfo load neweyefinity.cfg`
