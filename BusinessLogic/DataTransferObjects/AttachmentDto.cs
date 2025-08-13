using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }

        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }

        public int TaskItemId { get; set; }
        public string FileSizeFormatted
        { 
            get
            { string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB" };
                double size = FileSize;
                int suffixIndex = 0;
                while (size >= 1024 && suffixIndex < sizeSuffixes.Length - 1)
                {
                    size /= 1024;
                    suffixIndex++;
                }
                return $"{size:0.##} {sizeSuffixes[suffixIndex]}";

            }
        }
        public string DownloadUrl
        {
            get
            {
                return $"/api/attachments/{Id}";
            }
        }


    }
}