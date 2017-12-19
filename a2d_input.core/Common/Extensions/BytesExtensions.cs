using System;

namespace a2d_input.core.Extensions
{
    public static class BytesExtensions
    {
        public static short[] ToInt16(this byte[] bytes)
        {
            const int size = sizeof(short);

            var lenght = bytes.Length/size;
            if (lenght%size != 0) throw new ArgumentException(nameof(bytes));

            var array = new short[lenght];

            for (var i = 0; i < lenght; i++)
            {
                array[i] = BitConverter.ToInt16(bytes, i*size);
            }

            return array;
        }
    }
}