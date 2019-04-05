Option Explicit

Sub LoadExclusions(filename)
	dim strData, lines, file, line, folder
	
	FolderExcludeCount = 0
	FileExcludeCount = 0
	
	Set file = fso.OpenTextFile(filename, 1)
	strData = file.ReadAll
	file.close

	lines = Split(strData,vbCrLf)

	for each line in lines
		if not line = "" then
		if Right(line,1) = "\" then
			if FolderExcludeCount mod 50 = 0 then
				Redim FolderExclude(FolderExcludeCount + 50)
			end if

			FolderExclude(FolderExcludeCount) = Left(line, Len(line)  - 1)
			FolderExcludeCount = FolderExcludeCount + 1
		else
			if FileExcludeCount mod 50 = 0 then
				Redim FileExclude(FileExcludeCount + 50)
			end if

			FileExclude(FileExcludeCount) = line
			FileExcludeCount = FileExcludeCount + 1
		end if
		end if
	next
rem 	redim preserve FolderExclude(FolderExcludeCount)
rem 	redim preserve FileExclude(FileExcludeCount)
End Sub	
	      

Sub ListFolder(OutFile, rootpath, strPath)
	Dim folder, files, folders, basepath, fullname, file, subfolder, exclude, i

	if strPath = "" then
		basepath = ""
	else
		basepath = strPath + "\"
	end if

	if basepath = "" then
		Set folder = fso.GetFolder(rootpath + "\.")
	else
		Set folder = fso.GetFolder(rootpath + "\" + basepath)
	end if

	Set files = folder.Files
	Set folders = folder.SubFolders

	for each file in files
	do
		fullname = basepath + file.Name
		if file.attributes and 2 then
			exit do
		end if

		if Left(file.Name, 1) = "." then
			exit do
		end if
		
		for i = 0 to FileExcludeCount - 1
			if fullname = FileExclude(i) then
				exit do
			end if
		next
		
		OutFile.WriteLine(fullname)
	loop while false
	Next
	
	For each subfolder in folders
	do
		fullname = basepath + subfolder.Name
		if subfolder.attributes and 2 then
			exit do
		end if

		if Left(subfolder.Name, 1) = "." then
			exit do
		end if

		for i = 0 to FolderExcludeCount - 1
			if fullname = FolderExclude(i) then
				exit do
			end if
		next
rem		OutFile.WriteLine(fullname)
		ListFolder OutFile, rootpath, basepath + subfolder.Name
	loop while false
	Next
End Sub


dim FileExclude, FolderExclude, FolderExcludeCount, FileExcludeCount, fso, NewFile, inpath, outfile, exclusionfile,objstdout

Set fso = CreateObject("Scripting.FileSystemObject")

if wscript.Arguments.count = 3 then
	inpath = wscript.Arguments.Item(0)
	exclusionfile = wscript.Arguments.Item(1)
	outfile = wscript.Arguments.Item(2)
	
	Set NewFile = fso.CreateTextFile(outfile, True)
	LoadExclusions exclusionfile
	ListFolder NewFile, inpath, ""
	NewFile.close
elseif wscript.Arguments.count = 2 then
	inpath = wscript.arguments.item(0)
	exclusionfile = wscript.arguments.item(1)

	Set NewFile = wscript.StdOut
	LoadExclusions exclusionfile
	ListFolder NewFile, inpath , ""
else
	wscript.echo _
		"USAGE listfiles <folder path> <exclusion file> [<output file>]" & vbCrLf & vbCrLf & _
		"Lists all files in a folder and its subfolders, excluding any folders or files listed in the exclusion file"
end if
rem To use this in a batch file the line looks like:
rem cscript listfiles.vbs <folder path> <exclusion file> [<output file>]
rem If you don't specify output file then it goes to standard output
