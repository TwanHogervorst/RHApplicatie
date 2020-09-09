using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApplication
{
    public static class Utility
    {

        public static IEnumerable<byte> ReverseIfBigEndian(IEnumerable<byte> byteEnumerable) => !BitConverter.IsLittleEndian ? byteEnumerable.Reverse() : byteEnumerable;

        public static byte[] ReverseIfBigEndian(byte[] byteArray) => Utility.ReverseIfBigEndian((IEnumerable<byte>)byteArray).ToArray();

        public static List<byte> ReverseIfBigEndian(List<byte> byteList) => Utility.ReverseIfBigEndian((IEnumerable<byte>)byteList).ToList();
    }
}
