#if CSHARP_7_3_OR_NEWER

using System.IO;
using System.Linq;
using Gpm.Common.ThirdParty.SharpCompress.Common.Zip;

namespace Gpm.Common.ThirdParty.SharpCompress.Archives.Zip
{
    public class ZipArchiveEntry : ZipEntry, IArchiveEntry
    {
        internal ZipArchiveEntry(ZipArchive archive, SeekableZipFilePart part)
            : base(part)
        {
            Archive = archive;
        }

        public virtual Stream OpenEntryStream()
        {
            return Parts.Single().GetCompressedStream();
        }

#region IArchiveEntry Members

        public IArchive Archive { get; }

        public bool IsComplete => true;

#endregion

        public string Comment => (Parts.Single() as SeekableZipFilePart).Comment;
    }
}

#endif