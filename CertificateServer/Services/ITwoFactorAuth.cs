﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Services
{
    public interface ITwoFactorAuth
    {
        int Hash { get; set; }
        int GetTwoFactorHash();
        bool HashIsRigth(int inputHash);
    }
}
