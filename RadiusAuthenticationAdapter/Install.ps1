#
# Install Script.

Write-Host "Creating required registry keys"
New-Item -Path "HKLM:\SOFTWARE\Dave Hope" -Force | Out-Null
New-Item -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Force  | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "Server" -Value "127.0.0.1" | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "AuthenticationPort" -Value "1812" | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "AccountingPort" -Value "1813" | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "TimeOut" -Value "3000" | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "SharedSecret" -Value "password" | Out-Null
New-ItemProperty -Path "HKLM:\SOFTWARE\Dave Hope\RadiusAuthenticationAdapter" -Name "IdentityClaims" -Value "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" | Out-Null

#
# Copy files to the right locations
Write-Host "Copying files to $env:programfiles"
New-Item -ItemType Directory "$env:programfiles\Dave Hope\RadiusAuthenticationAdapter" | Out-Null
Copy-Item RadiusAuthenticationAdapter.dll "$env:programfiles\Dave Hope\RadiusAuthenticationAdapter" | Out-Null
Copy-Item Radius.dll "$env:programfiles\Dave Hope\RadiusAuthenticationAdapter" | Out-Null
Set-location "$env:programfiles\Dave Hope\RadiusAuthenticationAdapter"

#
# Install assemblies into GAC.
Write-Host "Installing assemblies into the GAC"
Add-Type -AssemblyName System.EnterpriseServices
$publish = New-Object System.EnterpriseServices.Internal.Publish
$publish.GacInstall("$env:programfiles\Dave Hope\RadiusAuthenticationAdapter\Radius.dll")
$publish.GacInstall("$env:programfiles\Dave Hope\RadiusAuthenticationAdapter\RadiusAuthenticationAdapter.dll")

#
# Create EventLog source
Write-Host "Creating EventLog source"
if ([System.Diagnostics.EventLog]::SourceExists("RADIUS Authentication Adapter") -eq $false) {
	[System.Diagnostics.EventLog]::CreateEventSource("RADIUS Authentication Adapter", $null)
}

#
# Prompt before continuing
Write-Host "Review the above and Press Y continue and register the Authentication Adapter. CTRL-C to exit"
$response = read-host
if ( $response -ne "Y" )
{
	Write-Host "Aborted"
	exit	
}


#
# Register AD:FS Authentication Adapter
$typeName = ([system.reflection.assembly]::loadfile("$env:programfiles\Dave Hope\RadiusAuthenticationAdapter\RadiusAuthenticationAdapter.dll")).FullName
Register-AdfsAuthenticationProvider -TypeName "RadiusAuthenticationAdapter.AuthenticationAdapter, $typeName" -Name "RadiusAuthenticationAdapter" -Verbose
