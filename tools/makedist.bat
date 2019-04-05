set buildtool=c:\windows\microsoft.net\framework\v3.5\msbuild.exe
if exist "%buildtool%" goto have_build_tool
echo MSBuild for .NET Framework 3.5 is not in its normal location
echo Edit the batch file to set the location of the MSBuild.exe.
echo If do not have Visual Studio then you can get this utility by installing
echo the tools from Windows SDK for Windows 7 from microsoft.com

:have_build_tool
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
jzip -a -r -p dist\WillowTree#Source.zip @tools\src_filelist.txt
cd bin\release
del ..\..\dist\willowtree#.zip
jzip -a -r -p ..\..\dist\WillowTree#.zip @..\..\tools\bin_filelist.txt
cd ..\..
del tools\src_filelist.txt
del tools\bin_filelist.txt
explorer dist
:end