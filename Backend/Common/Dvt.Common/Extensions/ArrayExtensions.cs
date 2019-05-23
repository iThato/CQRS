using System;
using System.Linq;

namespace Dvt.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static byte[] Union(this byte[] firstArray, byte[] secondArray)
        {
            var resultArray = new byte[firstArray.Length + secondArray.Length];
            Array.Copy(firstArray, resultArray, firstArray.Length);
            Array.Copy(secondArray, 0, resultArray, firstArray.Length, secondArray.Length);
            return resultArray;
        }

        public static byte[] RemoveFirstBytes(this byte[] sourceArray, int firstNumberOfBytes)
        {
            var resultBytes = new byte[sourceArray.Length - firstNumberOfBytes];
            Array.Copy(sourceArray, firstNumberOfBytes, resultBytes, 0, sourceArray.Length - firstNumberOfBytes);
            return resultBytes;
        }

        public static byte[] CopyBytes(this byte[] sourceArray, int numberOfBytes)
        {
            var resultBytes = new byte[numberOfBytes];
            Array.Copy(sourceArray, resultBytes, numberOfBytes);
            return resultBytes;
        }

        public static bool IsEqualTo(this byte[] sourceArray, byte[] secondArray)
        {
            return sourceArray.SequenceEqual(secondArray);
        }

        public static bool NotEqualTo(this byte[] sourceArray, byte[] secondArray)
        {
            return !sourceArray.SequenceEqual(secondArray);
        }
    }
}
