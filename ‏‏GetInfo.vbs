Dim oWMISrvEx 'SWbemServicesEx
Dim oWMIObjSet 'SWbemServicesObjectSet
Dim oWMIObjEx 'SWbemObjectEx
Dim oWMIProp 'SWbemProperty
Dim sWQL 'WQL Statement
Dim n
Dim line

MsgBox "Wait to ""End"" Message"

strPath = Wscript.ScriptFullName
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.GetFile(strPath)
strFolder = objFSO.GetParentFolderName(objFile) 

Set wshShell = CreateObject( "WScript.Shell" )
filename = wshShell.ExpandEnvironmentStrings( "%COMPUTERNAME%" )

outFile = strFolder & "\" & filename & ".csv"

Set objFile = objFSO.OpenTextFile(outFile, 8, True)
objFile.WriteLine "name,value"

getData "LogicalDisk", "Disk"
getData "SCSIController", "SCSI"
getData "IDEController", "IDE"
getData "DiskDrive", "DiskDrive"
getData "Processor", "Processor"
getData "PhysicalMemory", "Memory"
getData "VideoController", "Video"
getData "OnBoardDevice", "Board"
getData "BaseBoard", "MoBo"
getData "OperatingSystem", "OS"
getData "Product", "Softwares"
getData "BaseService", "Services"

objFile.Close
MsgBox("End")

Sub getData(library, sufix)
	
	sWQL = "Select * From Win32_"&library 'LogicalDisk"
	Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
	Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL) 

	diskNum = 1
	counter = 1


	For Each oWMIObjEx In oWMIObjSet
	
		For Each oWMIProp In oWMIObjEx.Properties_
			If Not IsNull(oWMIProp.Value) Then
			
				If IsArray(oWMIProp.Value) Then
				
					For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
						If (oWMIProp.Name = "Size") Then
							line = oWMIProp.Name & CStr(diskNum) & "_"&sufix'Disk"
							diskNum = diskNum + 1
						ElseIf oWMIProp.Name = "Caption" Then
							line = oWMIProp.Name & CStr(counter) & "_"&sufix'Video"
							counter = counter + 1
						Else
							line = oWMIProp.Name & "_"&sufix'Disk"
						End If
						line = line & "," & oWMIProp.Value(n) 
						objFile.WriteLine line						
					Next
				Else
				
					If (oWMIProp.Name = "Size") Then
						line = CStr(oWMIProp.Name) & CStr(diskNum) & "_"&sufix'Disk"
						diskNum = diskNum + 1
					ElseIf oWMIProp.Name = "Caption" Then
						line = oWMIProp.Name & CStr(counter) & "_"&sufix'Video"
						counter = counter + 1
					Else
						line = oWMIProp.Name & "_"&sufix'Disk"
					End If
					line = line & "," & oWMIProp.Value
					On Error Resume Next
					objFile.WriteLine line
				End If
			End If
		Next
	Next
End Sub
