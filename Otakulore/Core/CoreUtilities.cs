using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static T CastObject<T>(object @object)
        {
            var objectType = @object.GetType();
            var targetType = typeof(T);
            var instance = Activator.CreateInstance(targetType, false);
            var objectMembers = from source in objectType.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property select source;
            var targetMembers = from source in targetType.GetMembers().ToList() 
                where source.MemberType == MemberTypes.Property select source;
            var members = targetMembers.Where(targetMemberInfo => objectMembers.Select(objectMemberInfo => objectMemberInfo.Name).ToList().Contains(targetMemberInfo.Name)).ToList();
            foreach (var memberInfo in members)
            {
                var propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                var value = @object.GetType().GetProperty(memberInfo.Name).GetValue(@object,null);
                propertyInfo.SetValue(instance, value, null);
            } 
            return (T)instance;
        }

        public static string GetStringBetween(this string @string, string fromString, string toString)
        {
            var fromIndex = @string.IndexOf(fromString) + fromString.Length;
            @string = @string.Substring(fromIndex);
            return @string.Remove(@string.IndexOf(toString));
        }

    }

}