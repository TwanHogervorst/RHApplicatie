﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RHApplicationLib.Core
{
    public static class Utility
    {
        
        public static IEnumerable<byte> ReverseIfBigEndian(IEnumerable<byte> byteEnumerable) => !BitConverter.IsLittleEndian ? byteEnumerable.Reverse() : byteEnumerable;

        public static byte[] ReverseIfBigEndian(byte[] byteArray) => Utility.ReverseIfBigEndian((IEnumerable<byte>)byteArray).ToArray();

        public static List<byte> ReverseIfBigEndian(List<byte> byteList) => Utility.ReverseIfBigEndian((IEnumerable<byte>)byteList).ToList();

        public static void EnableAllChildControls(GroupBox groupBox)
        {
            foreach (Control ctrl in groupBox.Controls) ctrl.Enabled = true;
        }

        public static void DisableAllChildControls(GroupBox groupBox)
        {
            foreach (Control ctrl in groupBox.Controls) ctrl.Enabled = false;
        }

        /// <summary>
        /// Makes sure <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>.
        /// If <paramref name="value"/> is smaller than <paramref name="min"/>, then the result will be <paramref name="min"/>.
        /// If <paramref name="value"/> is larger than <paramref name="max"/>, then the result will be <paramref name="max"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static decimal Bound(decimal value, decimal min, decimal max) => Math.Min(Math.Max(min, value), max);
    }
}
