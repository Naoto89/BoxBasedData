using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxBasedDataLib
{
    public class BoxUtil
    {
        /// <summary>
        /// 識別文字列に従って適切にBoxインスタンスを生成する
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        public static Box CreateInstance(string typeStr, byte[] data = null)
        {
            switch (typeStr)
            {
                case "":
                default: return new Box(typeStr, data);
            }
        }
    }
}
