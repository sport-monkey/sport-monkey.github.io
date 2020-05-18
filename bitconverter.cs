using System;

T BitConverterPlus<T>(bool isBigEndian, byte[] bytes, int startIndex = 0)
{
    var len = bytes.Length;
    var bytesCopy = new byte[len];
    Array.Copy(bytes, bytesCopy, len);

    if (BitConverter.IsLittleEndian ? isBigEndian : !isBigEndian)
    {
        Array.Reverse(bytesCopy);
        var typeSize = System.Runtime.InteropServices.Marshal.SizeOf(default(T));
        startIndex = (len - typeSize) - startIndex;
    }
    var paramAray = new object[] { bytesCopy, startIndex };

    var typeName = typeof(T).Name;
    var methodInfo = (typeof(BitConverter)).GetMethod("To" + typeName);
    var obj = (T)methodInfo.Invoke(null, paramAray);
    return obj;
}