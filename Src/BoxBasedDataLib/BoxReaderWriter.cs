using System;
using System.Text;
using System.Buffers.Binary;
using System.IO;

namespace BoxBasedDataLib
{
    /// <summary>
    /// boxデータ読み込み
    /// </summary>
    public static class BoxReader
    {
        /// <summary>
        /// ファイルパスから読み込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Box Read(string filePath)
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                return Read(stream);
            }
            catch (Exception)
            {
                // TODO:適切な処理
                throw;
            }
        }

        /// <summary>
        /// streamから読み込み
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public static Box Read(Stream stream)
        {
            // Type
            var typeBuffer = new byte[Box.TypeSize];
            stream.ReadExactly(typeBuffer);

            var typeStr = Encoding.ASCII.GetString(typeBuffer).TrimEnd('\0');

            // Size
            Span<byte> buffer = stackalloc byte[8];

            stream.ReadExactly(buffer);
            var size = BinaryPrimitives.ReadInt64LittleEndian(buffer);

            // ChildCount
            stream.ReadExactly(buffer);
            var childCount = BinaryPrimitives.ReadInt64LittleEndian(buffer);

            if (size < Box.HeaderSize)
            {
                throw new InvalidDataException($"Invalid box size: {size}");
            }

            if (childCount < 0)
            {
                throw new InvalidDataException($"Invalid child count: {childCount}");
            }

            var contentSize = size - Box.HeaderSize;

            // 子Boxを持つ場合
            if (childCount > 0)
            {
                var box = BoxUtil.CreateInstance(typeStr);

                for (long i = 0; i < childCount; i++)
                {
                    var child = Read(stream);
                    box.AddChild(child);
                }

                return box;
            }

            // Dataを持つ場合
            if (contentSize > int.MaxValue)
            {
                throw new InvalidDataException("Data is too large.");
            }

            var data = new byte[contentSize];
            stream.ReadExactly(data);

            return new Box(typeStr, data);
        }
    }
    
    /// <summary>
    /// boxファイル書き込み
    /// </summary>
    public static class BoxWriter
    {
        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="box"></param>
        public static void Write(string filePath, Box box)
        {
            try
            {
                using var stream = File.Create(filePath);
                Write(stream, box);
            }
            catch (Exception)
            {
                // TODO:適切な処理
                throw;
            }
        }

        /// <summary>
        /// ストリーム指定して書き込み
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="box"></param>
        public static void Write(Stream stream, Box box)
        {
            // Type
            var typeBuffer = new byte[Box.TypeSize];

            Encoding.ASCII.GetBytes(box.TypeStr, typeBuffer);

            stream.Write(typeBuffer);

            // Size
            Span<byte> buffer = stackalloc byte[8];

            BinaryPrimitives.WriteInt64LittleEndian(buffer, box.Size);

            stream.Write(buffer);

            // ChildCount
            BinaryPrimitives.WriteInt64LittleEndian(buffer, box.ChildCount);

            stream.Write(buffer);

            // Content
            if (box.ChildCount > 0)
            {
                foreach (Box child in box.Children)
                {
                    Write(stream, child);
                }
            }
            else
            {
                stream.Write(box.Data);
            }
        }
    }

}
