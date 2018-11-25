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
getData "PhysicalMemoryArray", "MemoryArray"
getData "VideoController", "Video"
getData "OnBoardDevice", "Board"
getData "BaseBoard", "MoBo"
getData "OperatingSystem", "OS"
getData "Product", "Softwares"
getData "BaseService", "Services"

objFile.Close
MsgBox("End")

Sub getData(library, sufix)
	
	sWQL = "Select * From Win32_" & library
	Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
	Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL) 


	For Each oWMIObjEx In oWMIObjSet
	
		For Each oWMIProp In oWMIObjEx.Properties_
			If Not IsNull(oWMIProp.Value) Then
			
				If IsArray(oWMIProp.Value) Then
				
					For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
						line = oWMIProp.Name & "_" & sufix
						line = line & "," & oWMIProp.Value(n) 
						objFile.WriteLine line					
						
					Next
				Else
				
					line = oWMIProp.Name & "_" & sufix
					line = line & "," & oWMIProp.Value
					
					On Error Resume Next
					objFile.WriteLine line
				End If
			End If
		Next
	Next
End Sub
