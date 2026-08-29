using System;
using System.Collections.Generic;
using System.Linq;

namespace BoxBasedDataLib
{
    /// <summary>
    /// ┌───────────────┐
    /// │ Type         32 bytes        │
    /// ├───────────────┤
    /// │ Size          8 bytes        │
    /// ├───────────────┤
    /// │ ChildCount    8 bytes        │
    /// ├───────────────┤
    /// │ Content                      │
    /// │                              │
    /// │ ChildCount == 0              │
    /// │     → Data                  │
    /// │                              │
    /// │ ChildCount > 0               │
    /// │     → ChildBox × ChildCount│
    /// └───────────────┘
    /// </summary>

    public class Box
    {
        public const int TypeSize = 32;
        public const int SizeSize = 8;
        public const int ChildCountSize = 8;
        public const int HeaderSize = TypeSize + SizeSize + ChildCountSize;

        /// <summary>
        /// Boxの識別子。ASCII 32バイト固定で保存する。
        /// </summary>
        public string TypeStr { get; set; }

        /// <summary>
        /// Boxが保持するデータ。
        /// ChildCountが0の場合に使用する。
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// 子Box。
        /// </summary>
        public List<Box> Children { get; } = new();

        /// <summary>
        /// 子Boxの数。
        /// バイナリにはInt64として保存する。
        /// </summary>
        public long ChildCount => Children.Count;

        /// <summary>
        /// Box全体のサイズ。
        /// Header + Content
        /// </summary>
        public long Size
        {
            get
            {
                if (Children.Count > 0)
                {
                    return HeaderSize + Children.Sum(x => x.Size);
                }

                return HeaderSize + Data.LongLength;
            }
        }

        /// <summary>
        /// コンストラクタ：タイプ指定
        /// </summary>
        /// <param name="typeStr"></param>
        public Box(string typeStr)
        {
            TypeStr = typeStr;
            Data = Array.Empty<byte>();
        }

        /// <summary>
        /// コンストラクタ：タイプとデータ配列指定
        /// </summary>
        /// <param name="typeStr"></param>
        /// <param name="data"></param>
        public Box(string typeStr, byte[] data)
        {
            TypeStr = typeStr;
            Data = data;
        }

        /// <summary>
        /// 子要素の追加
        /// </summary>
        /// <param name="child"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddChild(Box child)
        {
            if (Data.Length > 0)
            {
                throw new InvalidOperationException("Data box cannot have children.");
            }

            Children.Add(child);
        }

        /// <summary>
        /// 子要素の削除
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(Box child)
        {
            Children.Remove(child);
        }

        /// <summary>
        /// 子要素の存在確認
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        public bool HasChild(string typeStr)
        {
            return Children.Any(x => x.TypeStr == typeStr);
        }

        /// <summary>
        /// 最初にマッチする子要素の取得
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        public Box GetChild(string typeStr)
        {
            return Children.FirstOrDefault(x => x.TypeStr == typeStr);
        }

        /// <summary>
        /// マッチする子要素をすべて取得
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        public IEnumerable<Box> GetChildren(string typeStr)
        {
            return Children.Where(x => x.TypeStr == typeStr);
        }
    }
}
