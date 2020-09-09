using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApplication
{
    public static class Utility
    {

        public static IEnumerable<byte> ReverseIfBigEndian(IEnumerable<byte> byteEnumerable) => !BitConverter.IsLittleEndian ? byteEnumerable.Reverse() : byteEnumerable;

    }
}
