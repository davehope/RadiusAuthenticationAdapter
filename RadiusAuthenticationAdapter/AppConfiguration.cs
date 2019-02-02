namespace RadiusAuthenticationAdapter
{
    /// <summary>
    /// This class acts as a basis for configuration, from which derived classes can implement different configuration backends.
    /// </summary>
    class AppConfiguration
    {
        internal string _Server;
        public string Server
        {
            get { return _Server; }
            set { _Server = value;  }
        }

        internal uint _AuthenticationPort;
        public uint AuthenticationPort
        {
            get { return _AuthenticationPort; }
            set { _AuthenticationPort = value; }
        }

        internal uint _AccountingPort;
        public uint AccountingPort
        {
            get { return _AccountingPort; }
            set { _AccountingPort = value; }
        }

        internal int _TimeOut;
        public int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }

        internal string _SharedSecret;
        public string SharedSecret
        {
            get { return _SharedSecret; }
            set { _SharedSecret = value; }
        }

        internal string _IdentityClaims;
        public string IdentityClaims
        {
            get { return _IdentityClaims; }
            set { _IdentityClaims = value; }
        }

        internal string _NasAddress;
        public string NasAddress
        {
            get { return _NasAddress; }
            set { _NasAddress = value; }
        }

        internal bool _Debug;
        public bool Debug
        {
            get { return _Debug; }
            set { _Debug = value;  }
        }
    }
}
