using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Services
{
    public class TwoFactorAuth : ITwoFactorAuth
    {
        public int Hash { get; set; }

        public int GetTwoFactorHash()
        {
            //TODO Юзаем Албертину Апишку
            throw new NotImplementedException();
        }

        public bool HashIsRigth(int inputHash)
        {
            return inputHash == Hash;
        }
    }
}
