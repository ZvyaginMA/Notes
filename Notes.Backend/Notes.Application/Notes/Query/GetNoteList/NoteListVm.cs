using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Query.GetNoteList
{
    internal class NoteListVm
    {
        public IList<NoteLookupDto> Notes { get; set; }
    }
}
