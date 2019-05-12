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
            return new Random().Next();
        }

        //mock
        public bool HashIsRigth(int inputHash)
        {
            return true;
        }
    }
}
