# CommandLineTester

Test how C#/.NET command line splitting compares to various splitters here: https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp

From what we can see, from "Atif Aziz" CommandLineToArgvW having 0 fails, C#/.NET on Windows seem to use shell32.dll's CommandLineToArgvW to split the command line. On Linux, I don't know.

The next best thing is "CS. (467313)" and "Mikescher (No caret handling)". "Mikescher (No caret handling)" is the winner here IMO, since it can also easily support cmd.exe caret handling (as it originally does in stackoverflow).

You could say that CommandLineToArgvW is the winner, BUT it is not C# code and not portable. Also CommandLineToArgvW has some special/weird logic for the first argument (the program itself).

<pre>
Runs: 21709
User: Atif Aziz (749653) fails: 0
User: Jeffrey L Whitledge (298968) fails: 2966
User: Daniel Earwicker (298990) fails: 214
User: Anton (299795) fails: 212
User: CS. (467313) fails: 29
User: Vapour in the Alley (2132004) fails: 8411
User: Monoman (7774211) fails: 214
User: Thomas Petersson (19091999) fails: 214
User: Fabio Iotti (19725880) fails: 13688
User: ygoe (23961658) fails: 345
User: Kevin Thach (24829691) fails: 31
User: Lucas De Jesus (31621370) fails: 21709 exceptions: 6
User: HarryP (48008872) fails: 784
User: TylerY86 (53290784) fails: 2966
User: Louis Somers (55903304) fails: 4720
User: user2126375 (58233585) fails: 239
User: DilipNannaware (59131568) fails: 2966
User: Mikescher (this) fails: 8100
User: Mikescher (No caret handling) fails: 29
User: System.CommandLine.Parser fails: 212
</pre>
