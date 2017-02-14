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


sWQL = "Select * From Win32_LogicalDisk"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL) 

diskNum = 1


For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					If (oWMIProp.Name = "Size") Then
						line = oWMIProp.Name & CStr(diskNum) & "_Disk"
						diskNum = diskNum + 1
					Else
						line = oWMIProp.Name & "_Disk"
					End If
					line = line & "," & oWMIProp.Value(n) 
					objFile.WriteLine line						
				Next
			Else
				
				If (oWMIProp.Name = "Size") Then
					line = CStr(oWMIProp.Name) & CStr(diskNum) & "_Disk"
					diskNum = diskNum + 1
				Else
					line = oWMIProp.Name & "_Disk"
				End If
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next

sWQL = "Select * From Win32_SCSIController"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

diskNum = 1

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					If (oWMIProp.Name = "Size") Then
						line = oWMIProp.Name & CStr(diskNum) & "_SCSI"
						diskNum = diskNum + 1
					Else
						line = oWMIProp.Name & "_SCSI"
					End If
					
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				
				If (oWMIProp.Name = "Size") Then
					line = oWMIProp.Name & CStr(diskNum) & "_SCSI"
					diskNum = diskNum + 1
				Else
					line = oWMIProp.Name & "_SCSI"
				End If
				
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next


sWQL = "Select * From Win32_IDEController"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

diskNum = 1

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					If (oWMIProp.Name = "Size") Then
						line = oWMIProp.Name & CStr(diskNum) & "_IDE"
						diskNum = diskNum + 1
					Else
						line = oWMIProp.Name & "_IDE"
					End If
					
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				
				If (oWMIProp.Name = "Size") Then
					line = oWMIProp.Name & CStr(diskNum) & "_IDE"
					diskNum = diskNum + 1
				Else
					line = oWMIProp.Name & "_IDE"
				End If
				
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next

sWQL = "Select * From Win32_DiskDrive"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

diskNum = 1

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					If (oWMIProp.Name = "Size") Then
						line = oWMIProp.Name & CStr(diskNum) & "_DiskDrive"
						diskNum = diskNum + 1
					Else
						line = oWMIProp.Name & "_DiskDrive"
					End If
					
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				
				If (oWMIProp.Name = "Size") Then
					line = oWMIProp.Name & CStr(diskNum) & "_DiskDrive"
					diskNum = diskNum + 1
				Else
					line = oWMIProp.Name & "_DiskDrive"
				End If
				
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next

sWQL = "Select * From Win32_Processor"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					line = oWMIProp.Name & "_Processor"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				line = oWMIProp.Name & "_Processor"
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next

sWQL = "Select * From Win32_PhysicalMemory"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					line = oWMIProp.Name & "_Memory"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				line = oWMIProp.Name & "_Memory"
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next



sWQL = "Select * From Win32_VideoController"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)
counter = 1

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					If oWMIProp.Name = "Caption" Then
						line = oWMIProp.Name & CStr(counter) & "_Video"
						counter = counter + 1
					Else
						line = oWMIProp.Name & "_Video"
					End If
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				If oWMIProp.Name = "Caption" Then
					line = oWMIProp.Name & CStr(counter) & "_Video"
					counter = counter + 1
				Else
					line = oWMIProp.Name & "_Video"
				End If
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next



sWQL = "Select * From Win32_OnBoardDevice"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					line = oWMIProp.Name & "_Board"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				line = oWMIProp.Name & "_Board"
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next




sWQL = "Select * From Win32_BaseBoard"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					line = oWMIProp.Name & "_MoBo"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				line = oWMIProp.Name & "_MoBo"
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
				
			End If
		End If
	Next
'End If
Next




sWQL = "Select * From Win32_OperatingSystem"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					line = oWMIProp.Name & "_OS"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else                            
				line = oWMIProp.Name & "_OS"
				line = line & "," & oWMIProp.Value
				On Error Resume Next
				objFile.WriteLine line
			End If
		End If
	Next
'End If
Next





sWQL = "Select * From Win32_Product"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					
					line = oWMIProp.Name & "_Softwares"
					
					line = line & "," & oWMIProp.Value(n)
					
					objFile.WriteLine line
					
				Next
			Else
				
				line = oWMIProp.Name & "_Softwares"
				
				line = line & "," & oWMIProp.Value
				
				On Error Resume Next
				objFile.WriteLine line
				
			End If
		End If
	Next
'End If
Next



sWQL = "Select * From Win32_BaseService"
Set oWMISrvEx = GetObject("winmgmts:root/CIMV2")
Set oWMIObjSet = oWMISrvEx.ExecQuery(sWQL)

For Each oWMIObjEx In oWMIObjSet
	
	For Each oWMIProp In oWMIObjEx.Properties_
		If Not IsNull(oWMIProp.Value) Then
			
			If IsArray(oWMIProp.Value) Then
				
				For n = LBound(oWMIProp.Value) To UBound(oWMIProp.Value)
					
					line = oWMIProp.Name & "_Services"
					line = line & "," & oWMIProp.Value(n)
					objFile.WriteLine line
				Next
			Else
				line = oWMIProp.Name & "_Services"
				line = line & "," & oWMIProp.Value
				
				On Error Resume Next
				objFile.WriteLine line
				
			End If
		End If
	Next
'End If
Next


objFile.Close
MsgBox("End")
