using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;

namespace secure_online_bookstore.Services
{
    public interface IEncryptService
    {
        byte[] EncryptPassword(string password);
        string DescryptPassword(byte[] cipheredtext, byte[] key, byte[] IV);
        string EncodePassword(string password);
        string DecodePassword(string encodedData);
    }
}