using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression;


namespace Codingriver
{
	public static class ZipHelper
	{


        //压缩字节
        //1.创建压缩的数据流 
        //2.设定compressStream为存放被压缩的文件流,并设定为压缩模式
        //3.将需要压缩的字节写到被压缩的文件流
        public static byte[] GZipCompress(byte[] bytes)
        {
            using (MemoryStream compressStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Compress))
                    zipStream.Write(bytes, 0, bytes.Length);
                byte[] data= compressStream.ToArray();
                compressStream.Dispose();
                return data;
            }
        }

        //解压缩字节
        //1.创建被压缩的数据流
        //2.创建zipStream对象，并传入解压的文件流
        //3.创建目标流
        //4.zipStream拷贝到目标流
        //5.返回目标流输出字节
        public static byte[] GZipDecompress(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        UnityEngine.Debug.Log(resultStream.Capacity);
                        return resultStream.ToArray();
                    }
                }
            }
        }


        /// <summary>
        /// Zip
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] content)
		{
            
            //return content;
            Deflater compressor = new Deflater();
			compressor.SetLevel(Deflater.BEST_COMPRESSION);

			compressor.SetInput(content);
			compressor.Finish();

			using (MemoryStream bos = new MemoryStream(content.Length))
			{
				var buf = new byte[1024];
				while (!compressor.IsFinished)
				{
					int n = compressor.Deflate(buf);
					bos.Write(buf, 0, n);
				}
				return bos.ToArray();
			}
		}

        /// <summary>
        /// Unzip
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
		public static byte[] Decompress(byte[] content)
		{
			return Decompress(content, 0, content.Length);
		}

		public static byte[] Decompress(byte[] content, int offset, int count)
		{
			//return content;
			Inflater decompressor = new Inflater();
			decompressor.SetInput(content, offset, count);

			using (MemoryStream bos = new MemoryStream(content.Length))
			{
				var buf = new byte[1024];
				while (!decompressor.IsFinished)
				{
					int n = decompressor.Inflate(buf);
					bos.Write(buf, 0, n);
				}
				return bos.ToArray();
			}
		}
	}
}
