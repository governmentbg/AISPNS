using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AISTN.ExternalAppAPI.Models.Authentication
{
    public class AuthnRequest
    {
        public string SAMLRequest { get; set; }
        public string RelayState { get; set; }
    }
}