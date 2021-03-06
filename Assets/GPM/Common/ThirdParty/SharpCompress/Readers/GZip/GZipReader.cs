#if CSHARP_7_3_OR_NEWER

using System.Collections.Generic;
using System.IO;
using Gpm.Common.ThirdParty.SharpCompress.Common;
using Gpm.Common.ThirdParty.SharpCompress.Common.GZip;

namespace Gpm.Common.ThirdParty.SharpCompress.Readers.GZip
{
    public class GZipReader : AbstractReader<GZipEntry, GZipVolume>
    {
        internal GZipReader(Stream stream, ReaderOptions options)
            : base(options, ArchiveType.GZip)
        {
            Volume = new GZipVolume(stream, options);
        }

        public override GZipVolume Volume { get; }

        #region Open

        /// <summary>
        /// Opens a GZipReader for Non-seeking usage with a single volume
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static GZipReader Open(Stream stream, ReaderOptions options = null)
        {
            stream.CheckNotNull("stream");
            return new GZipReader(stream, options ?? new ReaderOptions());
        }

        #endregion Open

        protected override IEnumerable<GZipEntry> GetEntries(Stream stream)
        {
            return GZipEntry.GetEntries(stream, Options);
        }
    }
}

#endif