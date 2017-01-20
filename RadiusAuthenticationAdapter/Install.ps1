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
[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") | Out-Null
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
$typeName = "RadiusAuthenticationAdapter.AuthenticationAdapter, RadiusAuthenticationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7be220675eadd64f"
Register-AdfsAuthenticationProvider -TypeName $typeName -Name "RadiusAuthenticationAdapter" -Verbose