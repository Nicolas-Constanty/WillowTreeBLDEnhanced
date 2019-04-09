set buildtool=c:\windows\microsoft.net\framework\v3.5\msbuild.exe
set ziptool=c:\Program Files\7-Zip\7z.exe
set zipargs=a -r
if exist "%buildtool%" goto have_build_tool
echo MSBuild for .NET Framework 3.5 is not in its normal location
echo Edit the batch file to set the location of the MSBuild.exe.
echo If do not have Visual Studio then you can get this utility by installing
echo the tools from Windows SDK for Windows 7 from microsoft.com
goto end
:have_build_tool
if exist "%ziptool%" goto have_zip_tool
echo Couldn't find zip tool.  Edit the batch file to update its path and
arguments.
:have_zip_tool
cd ..
mkdir dist
rmdir /s /q bin\release
"%buildtool%" willowtreesharp.sln /t:rebuild /p:configuration=Release
del willowtreesharp.sln.cache
cd tools
cscript listfiles.vbs .. src_exclude.txt src_filelist.txt
cscript listfiles.vbs ..\bin\release bin_exclude.txt bin_filelist.txt
cd ..
del dist\willowtree#source.zip
"%ziptool%" %zipargs% dist\WillowTree#Source.zip @tools\src_filelist.txt
cd bin\release
del ..\..\dist\willowtree#.zip
"%ziptool%" %zipargs% ..\..\dist\WillowTree#.zip @..\..\tools\bin_filelist.txt
cd ..\..
del tools\src_filelist.txt
del tools\bin_filelist.txt
explorer dist
:end