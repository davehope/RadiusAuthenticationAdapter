# Overview
AD FS Authentication Adapter for RADIUS. 

"Radius Authentication Adapter" is an Authentication Adapter for Microsoft AD FS, allowing RADIUS to be used as an authentication source for MFA.

The typical use case is likely to be sending a PIN/Token, provided by an end user, off to a RADIUS server.

![readme](https://cloud.githubusercontent.com/assets/5435716/22159558/69e37410-df39-11e6-8349-8584fd0bc0d5.png)

## Requirements
- .NET 4.5

## Installation Instructions
The below instructions assume the code has been built.

- Compile [Radius.Net](https://github.com/frontporch/Radius.NET) and sign it with a new SNK;
- Reference Radius.dll in this project;
- Build this project and sign it with a new SNK;
- Copy Radius.dll, RadiusAuthenticationAdapter.dll and install .ps1 to a local folder on each AD:FS server;
- Update install.ps1 , specifying the RADIUS server details (name, ports, psk etc)
- If building your own assembly, update the PublicKeyToken property in install.ps1
- Enable the Authentication Provider in AD:FS and configure as required

## License
Copyright (C) 2017 Dave Hope

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.

[LICENSE](https://github.com/davehope/RadiusAuthenticationAdapter/blob/master/LICENSE) contains a copy of the full GPLv3 licensing conditions.
