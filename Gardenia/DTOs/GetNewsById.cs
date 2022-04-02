using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.DTOs
{
    public class GetNewsById
    {
        public NewsWithAllProperties News { get; set; }
        public List<NewsImagesDTO> NewsImages { get; set; }
        public List<ResponseCommentDTO> Comments { get; set; }
    }
}
