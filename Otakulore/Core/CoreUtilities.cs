﻿using System;
using System.IO;
using System.Reflection;

namespace Otakulore.Core
{

    public static class CoreUtilities
    {

        public static string GetResourceFileAsString(string fileName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Otakulore.Resources." + fileName);
            var streamReader = new StreamReader(stream);
            var resourceString = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            return resourceString;
        }

        public static byte[] GetResourceFileAsByteArray(string fileName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Otakulore.Resources." + fileName);
            long originalPosition = 0;
            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }
            try
            {
                var readBuffer = new byte[4096];
                var totalBytesRead = 0;
                int bytesRead;
                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;
                    if (totalBytesRead != readBuffer.Length)
                        continue;
                    var nextByte = stream.ReadByte();
                    if (nextByte <= -1)
                        continue;
                    var temp = new byte[readBuffer.Length * 2];
                    Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                    Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                    readBuffer = temp;
                    totalBytesRead++;
                }
                var buffer = readBuffer;
                if (readBuffer.Length == totalBytesRead)
                    return buffer;
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                    stream.Position = originalPosition;
            }
        }

        public static string GetStringBetween(this string @string, string fromString, string toString)
        {
            var fromIndex = @string.IndexOf(fromString) + fromString.Length;
            @string = @string.Substring(fromIndex);
            return @string.Remove(@string.IndexOf(toString));
        }

    }

}